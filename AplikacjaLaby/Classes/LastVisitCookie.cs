using AplikacjaLaby.Models.Services;

namespace AplikacjaLaby.Classes
{
    public class LastVisitCookie
    {
        private readonly RequestDelegate _next; // Delegate calls next middleware layer

        private readonly ITimeProvider _timeProvider;

        public readonly static string CookieName = "VISIT";

        public LastVisitCookie(RequestDelegate @delegate, ITimeProvider timeProvider) // @ sign allows us to use keyword (delegate) as variable name
        {
            _next = @delegate;
            _timeProvider = timeProvider;
        }

        public async Task Invoke(HttpContext context) // Method will be called on given context everytime this layer is called
        {
            if (context.Request.Cookies.ContainsKey(CookieName)) // If there is Cookie paramater named CookieName:
            {
                if (context.Request.Cookies.TryGetValue(CookieName, out string? value)) // If there is a value in CookieName property
                {
                    var visitTime = DateTime.Parse(value);
                    context.Items.Add(CookieName, visitTime); // Save parsed time in collection of Items from current context
                }                    
            }
            else 
                context.Items.Add(CookieName, "First Visit"); // If there were no such parameter add "First Visit" string to request.

            context.Response.Cookies.Append(CookieName, _timeProvider.GetCurrentTime().ToString()); // Anyway append new Cookie with given name and Current date

            await _next(context); // Pass modified context and call next layer
        }
    }
}
