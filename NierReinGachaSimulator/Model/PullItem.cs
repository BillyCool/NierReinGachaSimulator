namespace NierReinGachaSimulator.Model
{
    public class PullItem
    {
        public GachaWeapon Weapon { get; init; }

        public GachaCostume Costume { get; init; }

        public bool HasCostume => Costume != null;
    }
}
