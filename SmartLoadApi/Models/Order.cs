using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartLoadApi.Models
{
    public class Order
    {
        [Required]
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        [Range(0, long.MaxValue)]
        [JsonPropertyName("payout_cents")]
        public long PayoutCents { get; set; }

        [Range(1, long.MaxValue)]
        [JsonPropertyName("weight_lbs")]
        public long WeightLbs { get; set; }

        [Range(1, long.MaxValue)]
        [JsonPropertyName("volume_cuft")]
        public long VolumeCuft { get; set; }

        [Required]
        [JsonPropertyName("origin")]
        public string Origin { get; set; } = default!;

        [Required]
        [JsonPropertyName("destination")]
        public string Destination { get; set; } = default!;

        [Required]
        [JsonPropertyName("pickup_date")]
        public DateTime PickupDate { get; set; }

        [Required]
        [JsonPropertyName("delivery_date")]
        public DateTime DeliveryDate { get; set; }

        [JsonPropertyName("is_hazmat")]
        public bool IsHazmat { get; set; }
    }
}
