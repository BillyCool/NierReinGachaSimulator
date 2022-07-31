namespace NierReinGachaSimulator.Components;

public partial class GachaDetails
{
    [Parameter][EditorRequired] public GachaModel Gacha { get; set; }

    private int CurrentGachaId;
    private int PullCounter;
    private readonly List<GachaPullItemModel> Pulls = new();
    private readonly Dictionary<RarityType, int> WeaponTracking = new();
    private readonly Dictionary<RarityType, int> CostumeTracking = new();

    private int PullItemCounter => PullCounter * Gacha.ItemsPerPull;

    private int? GetCostumeCount(RarityType rarityType) => rarityType == RarityType.TwoStar ? null : CostumeTracking.TryGetValue(rarityType, out int count) ? count : 0;

    private int GetWeaponCount(RarityType rarityType) => WeaponTracking.TryGetValue(rarityType, out int count) ? count : 0;

    protected override void OnInitialized()
    {
        if (Gacha != null)
        {
            CurrentGachaId = Gacha.GachaId;
            Reset();
        }
    }

    protected override void OnParametersSet()
    {
        if (Gacha != null && CurrentGachaId != Gacha.GachaId)
        {
            CurrentGachaId = Gacha.GachaId;
            Reset();
        }
    }

    private void Pull()
    {
        Pulls.Clear();

        for (int i = 0; i < Gacha.ItemsPerPull - 1; i++)
        {
            Pulls.Add(PullItem());
        }
        Pulls.Add(PullLastItem());

        PullCounter++;
    }

    private void Reset()
    {
        Pulls.Clear();
        for (int i = 0; i < Gacha.ItemsPerPull; i++)
        {
            Pulls.Add(null);
        }
        PullCounter = 0;
        WeaponTracking.Clear();
        CostumeTracking.Clear();
    }

    private GachaPullItemModel PullItem()
    {
        if (Gacha == null) return null;

        GachaPullItemModel item = PullItemFromPool(Gacha.RarityRateDetailList);
        TrackItem(item);

        return item;
    }

    private GachaPullItemModel PullLastItem()
    {
        if (Gacha == null) return null;

        GachaPullItemModel item = PullItemFromPool(Gacha.LastChanceRarityRateDetailList);
        TrackItem(item);

        return item;
    }

    private GachaPullItemModel PullItemFromPool(List<RarityRateDetail> items)
    {
        Random rand = new();

        // Get universal probability
        decimal u = items.Sum(x => x.Rate);

        // Get random number between 0 and total probability
        decimal r = (decimal)rand.NextDouble() * u;

        // Loop until the random number is less than the cumulative probability
        decimal sum = 0;
        foreach (var item in items)
        {
            if (r <= (sum = sum + item.Rate))
            {
                return new GachaPullItemModel
                {
                    Weapon = item.Weapon,
                    Costume = item.Costume
                };
            }
        }

        // Should never get here
        return null;
    }

    private void TrackItem(GachaPullItemModel item)
    {
        if (item == null) return;

        if (!WeaponTracking.ContainsKey(item.Weapon.RarityType))
        {
            WeaponTracking[item.Weapon.RarityType] = 0;
        }

        WeaponTracking[item.Weapon.RarityType]++;

        if (item.Costume != null)
        {
            if (!CostumeTracking.ContainsKey(item.Weapon.RarityType))
            {
                CostumeTracking[item.Weapon.RarityType] = 0;
            }

            CostumeTracking[item.Weapon.RarityType]++;
        }
    }
}
