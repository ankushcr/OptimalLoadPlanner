using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartLoadApi.Models
{
    public class LoadRequest
    {
        [Required]
        [JsonPropertyName("truck")]
        public Truck Truck { get; set; } = default!;

        [Required]
        [JsonPropertyName("orders")]
        public List<Order> Orders { get; set; } = new();
    }
}
