using System.ComponentModel.DataAnnotations;

namespace Elwark.Extensions.AspNet.HttpClientAppName
{
    public class HttpClientAppNameOptions
    {
        [Required]
        public string AppName { get; set; } = string.Empty;

        [Required]
        public string HeaderName { get; set; } = "App-Name";
    }
}