namespace NierReinGachaSimulator.Components;

public partial class GachaSummary
{
    [CascadingParameter(Name = "PullItemCounter")] public int PullItemCounter { get; set; }

    [CascadingParameter(Name = "Gacha")] public GachaModel Gacha { get; set; }

    [Parameter] public RarityType RarityType { get; set; }

    [Parameter] public int WeaponCount { get; set; }

    [Parameter] public int? CostumeCount { get; set; }

    private decimal CostumeRngOffset => GetOffsetInternal(CostumeCount, true);

    private decimal WeaponRngOffset => GetOffsetInternal(WeaponCount - CostumeCount, false);

    private static Color GetRngColor(decimal offset)
    {
        return (offset) switch
        {
            decimal x when x < -15M => Color.Error,
            decimal x when x > 15M => Color.Success,
            _ => Color.Default
        };
    }

    private decimal GetOffsetInternal(int? entityCount, bool isCostume)
    {
        if (entityCount == null || PullItemCounter == 0) return 0.0M;

        decimal entityRate = Gacha.RarityRateList.Find(x => x.RarityType == RarityType && x.WithCostume == isCostume)?.Rate ?? 0;
        decimal entityRateLast = Gacha.LastChanceRarityRateList.Find(x => x.RarityType == RarityType && x.WithCostume == isCostume)?.Rate ?? 0;
        decimal entityRateTotal = (entityRate * 0.9M) + (entityRateLast * 0.1M);

        if (entityRateTotal == 0.0M) return 0.0M;

        decimal averagePullRate = PullItemCounter * entityRateTotal / PullItemCounter;
        decimal entityPullRate = 100.0M * entityCount.Value / PullItemCounter;

        return entityPullRate - averagePullRate;
    }
}
