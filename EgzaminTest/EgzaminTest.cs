using System.Net;
using System.Reflection;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Egzamin;
using Egzamin.Controllers;
using Egzamin.Models;
using Egzamin.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EgzaminTest;

[TestCaseOrderer(
    "EgzaminTest.TestOrderer", 
    "EgzaminTest")]
public class EgzaminTest : IClassFixture<EgzaminTestFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private IConfiguration _configuration = Configuration.Default;
    private IBrowsingContext _context;
    private IHtmlParser? _parser;

    private EgzaminTestFactory<Program> _app;
    private static int _points = 0;

    // Reference date from IDateProvider is 2024.01.01:00:000:00 
    private readonly ITestOutputHelper output;

    public EgzaminTest(EgzaminTestFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        this.output = output;
        _client = _factory.CreateClient();
        _context = BrowsingContext.New(_configuration);
        _parser = _context.GetService<IHtmlParser>();
    }

    [Theory]
    [InlineData("AlbumId", "Int32")]
    [InlineData("Title", "String")]
    [InlineData("ArtistId", "Int32")]
    [InlineData("Artist", "Artist")]
    public void Task01Test01(string propName, string propType)
    {
        Type clazz = typeof(Album);
        PropertyInfo[]? props = clazz.GetProperties();
        Assert.NotNull(props);
        Assert.Contains<PropertyInfo>(props, c => c.Name == propName && c.PropertyType.Name == propType);
        CountPoints();
    }

    [Theory]
    [InlineData("ArtistId", "Int32")]
    [InlineData("Name", "String")]
    public void Task01Test02(string propName, string propType)
    {
        Type clazz = typeof(Artist);
        PropertyInfo[]? props = clazz.GetProperties();
        Assert.NotNull(props);
        Assert.Contains<PropertyInfo>(props, c => c.Name == propName && c.PropertyType.Name == propType);
        CountPoints();
    }

    [Fact, ]
    public void Task02Test01()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            AppDbContext? context = scope.ServiceProvider.GetService<AppDbContext>();
            Assert.NotNull(context);
            Type clazz = context.GetType();
            var props = clazz.GetProperties();
            Assert.NotNull(props);
            CountPoints();
            Assert.Contains(props, p => p.Name == "Artists" && p.PropertyType.Name == "DbSet`1");
            CountPoints();
            Assert.Contains(props, p => p.Name == "Albums" && p.PropertyType.Name == "DbSet`1");
            CountPoints();
            var albums = clazz.GetProperty("Albums");
            var result = albums.GetMethod.Invoke(context, Array.Empty<object?>());
            Assert.NotNull(result);
            CountPoints();
        }
    }

    [Fact]
    public void Task03Test01()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            MusicService? service = scope.ServiceProvider.GetService<MusicService>();
            Assert.NotNull(service);
            Type serviceType = service.GetType();
            var methods = serviceType.GetMethods();
            Assert.Contains(methods, m => m is { Name: "GetAllArtists", ReturnType.Name: "List`1" });
            CountPoints();
            Assert.Contains(methods, m => m.Name == "GetAlbumsByArtistId" && m.ReturnType.Name == "List`1");
            CountPoints();
            var getArtist = serviceType.GetMethod("GetAllArtists");
            object? result = getArtist?.Invoke(service, Array.Empty<object?>());
            Assert.NotNull(result);
            List<Artist>? artists = result as List<Artist>;
            Assert.NotNull(artists);
            CountPoints();
            Assert.Equal(275, artists.Count());
            CountPoints();
        }
    }

    [Theory]
    [InlineData(58, 11)]
    [InlineData(67, 0)]
    [InlineData(1, 2)]
    public void Task03Test02(int artistId, int albumCount)
    {
        using (var scope = _factory.Services.CreateScope())
        {
            MusicService? service = scope.ServiceProvider.GetService<MusicService>();
            Assert.NotNull(service);
            Type serviceType = service.GetType();
            var methods = serviceType.GetMethods();
            Assert.Contains(methods, m => m is { Name: "GetAllArtists", ReturnType.Name: "List`1" });
            Assert.Contains(methods, m => m.Name == "GetAlbumsByArtistId" && m.ReturnType.Name == "List`1");
            var getArtist = serviceType.GetMethod("GetAllArtists");
            object? result = getArtist?.Invoke(service, Array.Empty<object?>());
            Assert.NotNull(result);
            List<Artist>? artists = result as List<Artist>;
            Assert.NotNull(artists);
            Assert.Equal(275, artists.Count());
            var getAlbums = serviceType.GetMethod("GetAlbumsByArtistId");
            Assert.NotNull(getAlbums);
            result = getAlbums.Invoke(service, new object?[] { artistId });
            Assert.NotNull(result);
            List<Album>? albums = result as List<Album>;
            Assert.NotNull(albums);
            Assert.Equal(albumCount, albums.Count());
        }
        CountPoints();
    }

    [Fact]
    public async void Task04Test01()
    {
        HttpResponseMessage response = await SentQuery(58);
        string content = await response.Content.ReadAsStringAsync();
        //Act
        IDocument document = _parser.ParseDocument(content);
        //Assert
        var form = document.QuerySelector("form");
        Assert.NotNull(form);
        CountPoints();
        var select = document.QuerySelector("select");
        Assert.NotNull(select);
        CountPoints();
        Assert.Equal(275, select.Children.Length);
        CountPoints();
        Assert.Equal("artistid", select.GetAttribute("name")?.ToLower());
        CountPoints();
        var button1 = document.QuerySelector("form button");
        var button2 = document.QuerySelector("form input[type='submit']");
        if (button1 is not null)
        {
            Assert.Equal("submit", button1.GetAttribute("type"));
            CountPoints();
        }
        else
        {
            Assert.NotNull(button2);
            CountPoints();
        }
    }

    [Theory]
    [InlineData(58, 11, "Fireball")]
    [InlineData(128, 1, "Retrospective I (1974-1980)")]
    [InlineData(45, 0, "")]
    [InlineData(22, 14, "Coda")]
    public async void Task04Test02(int id, int count, string album)
    {
        HttpResponseMessage response = await SentQuery(id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        string content = await response.Content.ReadAsStringAsync();
        //Act
        IDocument document = _parser.ParseDocument(content);
        //Assert
        var ul = document.QuerySelectorAll("ul").FirstOrDefault(l => l.Children.Length == count);
        Assert.NotNull(ul);
        if (ul.Children.Length > 0)
        {
            Assert.Contains(ul.Children, i => i.TextContent == album);
        }

        CountPoints();
    }

    [Theory]
    [InlineData("-1", HttpStatusCode.NotFound)]
    [InlineData("300", HttpStatusCode.NotFound)]
    public async void Task05Test01(string id, HttpStatusCode status)
    {
        //Arrange
        HttpResponseMessage response1 = await _client.GetAsync($"/Album/Index?artistid={id}");
        HttpResponseMessage response2 = await _client.GetAsync($"/Album/Index");
        //Act
        if (response2.StatusCode == HttpStatusCode.OK)
        {
            Assert.Equal(response1.StatusCode, status);
        }
        else
        {
            Assert.Fail("");
        }
        CountPoints();
    }

    [Fact]
    public void TaskSummary()
    {
        Assert.Equal(_points, _points);
        output.WriteLine($"Points: {_points}, Percent: { 100 * _points / 28d:F2}%, Student: {HomeController.Student}");
    }

    private void CountPoints()
    {
        _points++;
        output.WriteLine($"Points: {_points}");
    }

    private async Task<HttpResponseMessage> SentQuery(int id)
    {
        return await _client.GetAsync($"/Album/Index?ArtistId={id}");
    }
    
}