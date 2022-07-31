namespace NierReinGachaSimulator.Components;

public partial class GachaPullItem
{
    [Parameter][EditorRequired] public GachaPullItemModel Model { get; set; }

    public string AttributeTypeStr => Model.Weapon?.AttributeType.ToString().ToLowerInvariant();

    public string WeaponTypeStr => Model.Weapon?.WeaponType.ToString().ToLowerInvariant();

    private string BackgroundImage => Model.Weapon.RarityType switch
    {
        RarityType.FourStar or RarityType.FiveStar => "rainbow",
        RarityType.ThreeStar => "gold",
        _ => "silver"
    };
}
