using System.Text.Json.Serialization;

namespace SmartLoadApi.Models
{
    public class LoadResponse
    {
       [JsonPropertyName("truck_id")]
       public string TruckId { get; set; } = default!;
       [JsonPropertyName("selected_order_ids")]
       public List<string> SelectedOrderIds { get; set; } = new();
       [JsonPropertyName("total_payout_cents")]
       public long TotalPayoutCents { get; set; }
       [JsonPropertyName("total_weight_lbs")]
       public long TotalWeightLbs { get; set; }
       [JsonPropertyName("total_volume_cuft")]
       public long TotalVolumeCuft { get; set; }
       [JsonPropertyName("utilization_weight_percent")]
       public double UtilizationWeightPercent { get; set; }
       [JsonPropertyName("utilization_volume_percent")]
       public double UtilizationVolumePercent { get; set; }
    }
}
