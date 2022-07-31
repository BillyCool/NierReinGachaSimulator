namespace NierReinGachaSimulator.Pages;

public partial class HomePage
{
    [CascadingParameter(Name = "Gachas")] public GachaModel[] Gachas { get; set; }
}
