namespace NierReinGachaSimulator.Pages;

public partial class GachaPage
{
    [Parameter] public int GachaId { get; set; }

    [CascadingParameter(Name = "Gachas")] public GachaModel[] Gachas { get; set; }

    private GachaModel Gacha => Gachas?.FirstOrDefault(x => x.GachaId == GachaId);
}
