using System.Text.Json.Serialization;

namespace Greenglobal.Core.Models
{
    public class ApplicationRequest
    {
        public string Name { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        public string Code { get; set; }

        [JsonPropertyName("icon_path")]
        public string? IconPath { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; } = 1;
    }
}
