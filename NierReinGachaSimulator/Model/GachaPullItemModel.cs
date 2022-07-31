namespace NierReinGachaSimulator.Model
{
    public class GachaPullItemModel
    {
        public GachaWeaponModel Weapon { get; init; }

        public GachaCostumeModel Costume { get; init; }

        public bool HasCostume => Costume != null;
    }
}
