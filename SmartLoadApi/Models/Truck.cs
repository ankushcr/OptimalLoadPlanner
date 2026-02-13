using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartLoadApi.Models
{
    public class Truck
    {
        [Required]
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        [Range(1, long.MaxValue)]
        [JsonPropertyName("max_weight_lbs")]
        public long MaxWeightLbs { get; set; }

        [Range(1, long.MaxValue)]
        [JsonPropertyName("max_volume_cuft")]
        public long MaxVolumeCuft { get; set; }
    }
}
