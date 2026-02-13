using Microsoft.Extensions.Caching.Memory;
using SmartLoadApi.Models;

namespace SmartLoadApi.Services
{
    public class LoadOptimizerService
    {
        //caching can be implemented to further optimize performance
        //private readonly IMemoryCache _cache;

        public LoadResponse Optimize(Truck truck, List<Order> orders)
        {
            //check for empty orders
            if (orders.Count == 0)
                return Empty(truck);

            int n = orders.Count;

            long maxWeight = truck.MaxWeightLbs;
            long maxVolume = truck.MaxVolumeCuft;

            long bestPayout = 0;
            long bestWeight = 0;
            long bestVolume = 0;
            int bestMask = 0;

            //process all the order states
            for (int mask = 0; mask < (1 << n); mask++)
            {
                long totalWeight = 0;
                long totalVolume = 0;
                long totalPayout = 0;

                bool hazmatIncluded = false;
                bool nonHazmatIncluded = false;
                bool valid = true;

                //process total weight, volume, payout for valid orders
                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) == 0)
                        continue;

                    var order = orders[i];

                    totalWeight += order.WeightLbs;
                    if (totalWeight > maxWeight)
                    {
                        valid = false;
                        break;
                    }

                    totalVolume += order.VolumeCuft;
                    if (totalVolume > maxVolume)
                    {
                        valid = false;
                        break;
                    }

                    totalPayout += order.PayoutCents;

                    if (order.IsHazmat)
                        hazmatIncluded = true;
                    else
                        nonHazmatIncluded = true;

                    if (hazmatIncluded && nonHazmatIncluded)
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                    continue;

                bool includeOrders = false;


                if (totalPayout > bestPayout)
                {
                    includeOrders = true;
                }
                else if (totalPayout == bestPayout)
                {
                    if (totalWeight > bestWeight)
                    {
                        includeOrders = true;
                    }
                    else if (totalWeight == bestWeight && totalVolume > bestVolume)
                    {
                        includeOrders = true;
                    }
                }

                if (includeOrders)
                {
                    bestPayout = totalPayout;
                    bestWeight = totalWeight;
                    bestVolume = totalVolume;
                    bestMask = mask;
                }
            }

            if (bestPayout == 0)
                return Empty(truck);

            var selected = new List<string>();
            long finalWeight = 0;
            long finalVolume = 0;

            for (int i = 0; i < n; i++)
            {
                if ((bestMask & (1 << i)) == 0)
                    continue;

                selected.Add(orders[i].Id);
                finalWeight += orders[i].WeightLbs;
                finalVolume += orders[i].VolumeCuft;
            }

            return new LoadResponse
            {
                TruckId = truck.Id,
                SelectedOrderIds = selected,
                TotalPayoutCents = bestPayout,
                TotalWeightLbs = finalWeight,
                TotalVolumeCuft = finalVolume,
                UtilizationWeightPercent =
                    Math.Round((double)finalWeight / maxWeight * 100, 2),
                UtilizationVolumePercent =
                    Math.Round((double)finalVolume / maxVolume * 100, 2)
            };
        }

        private LoadResponse Empty(Truck truck) =>
            new()
            {
                TruckId = truck.Id,
                SelectedOrderIds = new(),
                TotalPayoutCents = 0,
                TotalWeightLbs = 0,
                TotalVolumeCuft = 0,
                UtilizationWeightPercent = 0,
                UtilizationVolumePercent = 0
            };
    }
}
