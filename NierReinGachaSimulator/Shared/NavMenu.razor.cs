namespace NierReinGachaSimulator.Shared;

public partial class NavMenu
{
    [CascadingParameter(Name = "Gachas")] public GachaModel[] Gachas { get; set; }
}
