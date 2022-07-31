namespace NierReinGachaSimulator.Shared;

public partial class MainLayout
{
    [Inject] private ILocalStorageService LocalStorageService { get; set; }

    private MudThemeProvider ThemeProvider;
    private bool IsDrawerOpen = true;
    private bool IsDarkMode = true;

    private readonly MudTheme Theme = new();
    private GachaModel[] Gachas = Array.Empty<GachaModel>();

    protected override async Task OnInitializedAsync()
    {
        Gachas = await Http.GetFromJsonAsync<GachaModel[]>("data/gacha.json?v=20220801");
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            bool? isDarkTheme = await LocalStorageService.GetItemAsync<bool?>("NierReinGachaSimulator.IsDarkMode");

            if (isDarkTheme.HasValue)
            {
                IsDarkMode = isDarkTheme.Value;
            }
            else
            {
                IsDarkMode = await ThemeProvider.GetSystemPreference();
                await LocalStorageService.SetItemAsync("NierReinGachaSimulator.IsDarkMode", IsDarkMode);
            }

            StateHasChanged();
        }
    }

    private void DrawerToggle() => IsDrawerOpen = !IsDrawerOpen;

    public async void ThemeToggle(bool _)
    {
        IsDarkMode = !IsDarkMode;
        await LocalStorageService.SetItemAsync("NierReinGachaSimulator.IsDarkMode", IsDarkMode);
    }
}
