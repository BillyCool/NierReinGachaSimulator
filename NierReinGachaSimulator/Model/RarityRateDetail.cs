namespace NierReinGachaSimulator.Model
{
    public class RarityRateDetail
    {
        public GachaWeaponModel Weapon { get; init; }

        public GachaCostumeModel Costume { get; init; }

        public decimal Rate { get; set; }
    }
}
