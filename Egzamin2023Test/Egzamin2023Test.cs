using System.Net;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Ezgamin2023Test;

public class Egzamin2023Test : IClassFixture<Egzamin2023TestFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private static int _points;
    private IConfiguration _configuration = Configuration.Default;
    private IBrowsingContext _context;
    private IHtmlParser? _parser;

    private Egzamin2023TestFactory<Program> _app;

    // Reference date from IDateProvider is 2024.01.01:00:000:00 
    private readonly ITestOutputHelper output;

    public Egzamin2023Test(Egzamin2023TestFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        this.output = output;
        _client = _factory.CreateClient();
        _context = BrowsingContext.New(_configuration);
        _parser = _context.GetService<IHtmlParser>();
    }

    [Theory]
    [InlineData("Content", "Treść", null, "textarea")]
    [InlineData("Deadline", "Data ważności", "datetime-local", "input")]
    [InlineData("Title", "Tytuł", "text", "input")]
    public async void Test01(string name, string label, string type, string element)
    {
        //Arrange

        var result = await _client.GetAsync($"/Exam/Create");

        //Act

        IDocument document = _parser.ParseDocument(await result.Content.ReadAsStringAsync());

        //Assert

        IHtmlCollection<IElement> forms = document.QuerySelectorAll("form");
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(1, forms.Length);

        IElement? inputElement = document.QuerySelector($"{element}[name='{name}']");
        Assert.NotNull(inputElement);
        Assert.Equal(name, inputElement.GetAttribute("name"));
        Assert.Equal(type, inputElement.GetAttribute("type"));

        IElement? labelElement = document.QuerySelector($"label[for='{name}']");
        Assert.NotNull(labelElement);
        Assert.Equal(label.ToLower(), labelElement.TextContent.ToLower());
    }

    [Fact]
    public async void Test02A()
    {
        //Invalid note
        //Arrange

        var result = await SendNote("as", "abcde", new DateTime(2024, 1, 1, 0, 59, 0));

        //Act

        string content = await result.Content.ReadAsStringAsync();
        IDocument document = _parser.ParseDocument(content);

        //Assert

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        IElement? form = document.QuerySelector("form");
        Assert.NotNull(form);
        IHtmlCollection<IElement> spans = document.QuerySelectorAll("span.field-validation-error");
        Assert.Equal(3, spans.Length);
        IElement? deadlineError = document.QuerySelector("span[data-valmsg-for='Deadline']");
        Assert.NotNull(deadlineError);
        Assert.NotNull(deadlineError.TextContent);
    }

    [Fact]
    public async void Test02B()
    {
        //Valid note
        //Arrange

        var result = await SendNote("ABCDEFGH", "ABCDEFGHJIKLMNOPRSDW", new DateTime(2024, 1, 2));

        //Act

        string content = await result.Content.ReadAsStringAsync();
        IDocument document = _parser.ParseDocument(content);

        //Assert

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        IElement? h1 = document.QuerySelector("h1");
        Assert.NotNull(h1);
        IElement? deadlineError = document.QuerySelector("span[data-valmsg-for='Deadline']");
        Assert.Null(deadlineError);
    }

    [Fact]
    public async void Test03()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            // Type? noteServiceType = Type.GetType("NoteService"); //Original version
            Type? noteServiceType = Type.GetType("Egzamin2023.Services.NoteService,Egzamin2023");
            Assert.NotNull(noteServiceType);

            Object? noteService = _factory.Services.GetService(noteServiceType);
            Assert.NotNull(noteService);
            Type clazz = noteService.GetType();
            var methods = clazz.GetMethods();
            Assert.Contains(methods, c => c.Name == "Add" && c.ReturnType.Name == "Void");
            string s = methods[2].ReturnType.Name;
            Assert.Contains(methods,
                c => c.Name == "GetAll" && c.ReturnType.IsClass && c.ReturnType.Name.Contains("List"));
            Assert.Contains(methods, c => c.Name == "GetById" && c.ReturnType.IsClass && c.ReturnType.Name == "Note");
            var methodGetAll = clazz.GetMethod("GetAll");
            Assert.NotNull(methodGetAll);
            object? notes = methodGetAll.Invoke(noteService, new object[] { });
            Assert.NotNull(notes);
        }
    }

    [Fact]
    public async void Test04()
    {
        //Arrange
        
        await SendNote("TEST1", "line 10\nline 20\nline 20", new DateTime(2024, 2, 2));
        await SendNote("TEST2", "line 11\nline 21\nline 21", new DateTime(2024, 4, 2));
        await SendNote("TEST3", "line 12\nline 22\nline 22", new DateTime(2024, 7, 2));
        await SendNote("TEST4", "line 13\nline 23\nline 23", new DateTime(2024, 1, 1, 0, 30, 0));
        
        //Act
        
        var response = await _client.GetAsync("/Exam/Index");
        string content = await response.Content.ReadAsStringAsync();
        IDocument document = _parser.ParseDocument(content);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        IElement? ul = document.QuerySelector("h1 + ul");
        Assert.NotNull(ul);
        Assert.Equal(4, ul.Children.Length);
        Assert.Contains(ul.Children, i => i.Children.Length == 1);
        var links = ul.Children.Select(c => c.Children[0]);
        Assert.Contains(links, c => c.TextContent == "TEST1");
        Assert.Contains(links, c => c.TextContent == "TEST2");
        Assert.Contains(links, c => c.TextContent == "TEST3");
        Assert.Contains(links, c => c.GetAttribute("href") == "/Exam/Details/TEST1");
        Assert.Contains(links, c => c.GetAttribute("href") == "/Exam/Details/TEST2");
        Assert.Contains(links, c => c.GetAttribute("href") == "/Exam/Details/TEST3");
        Assert.DoesNotContain(links, c => c.GetAttribute("href") == "/Exam/Details/TEST4");
    }

    [Fact]
    public async void Test05A()
    {
        //Arrange
        
        await SendNote("TEST1", "line 10\nline 20\nline 30", new DateTime(2024, 2, 2));
        await SendNote("TEST2", "line 11\nline 21\nline 31", new DateTime(2024, 4, 2));
        await SendNote("TEST3", "line 12\nline 22\nline 32", new DateTime(2024, 7, 2));
        await SendNote("TEST4", "line 13\nline 23\nline 33", new DateTime(2024, 1, 1));
        var response = await _client.GetAsync("/Exam/Details/TEST1");
        string content = await response.Content.ReadAsStringAsync();
        
        //Act
        
        IDocument document = _parser.ParseDocument(content);
        
        //Assert
        
        var h1 = document.QuerySelector("h1");
        Assert.NotNull(h1);
        Assert.Equal("TEST1", h1.TextContent);
        var div = document.QuerySelector("h1 + div");
        Assert.NotNull(div);
        Assert.Equal(3, div.Children.Length);
        Assert.Contains(div.Children, p => p.TextContent == "line 10");
        Assert.Contains(div.Children, p => p.TextContent == "line 20");
        Assert.Contains(div.Children, p => p.TextContent == "line 30");
    }

    [Fact]
    public async void Test05B()
    {
        //Arrange
        
        await SendNote("TEST1", "line 10\nline 20\nline 30", new DateTime(2024, 2, 2));
        await SendNote("TEST2", "line 11\nline 21\nline 31", new DateTime(2024, 4, 2));
        await SendNote("TEST3", "line 12\nline 22\nline 32", new DateTime(2024, 7, 2));
        await SendNote("TEST4", "line 13\nline 23\nline 33", new DateTime(2024, 1, 1));
        
        //Act
        
        var response = await _client.GetAsync("/Exam/Details/TEST4");
        string content = await response.Content.ReadAsStringAsync();
        
        //Assert
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<HttpResponseMessage> SendNote(string title, string content, DateTime deadline)
    {
        Dictionary<string, string> c = new Dictionary<string, string>()
        {
            { "Title", title },
            { "Content", content },
            { "Deadline", deadline.ToString() }
        };
        return await _client.PostAsync("/Exam/Create", new FormUrlEncodedContent(c));
    }
}