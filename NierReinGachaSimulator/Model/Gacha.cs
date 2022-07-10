﻿namespace NierReinGachaSimulator.Model
{
    public class Gacha
    {
        public int GachaId { get; init; }

        public string Name { get; init; }

        public DateTime StartTime { get; init; }

        public DateTime EndTime { get; init; }

        public List<RarityRateListElement> RarityRateList { get; init; }

        public List<RarityRateListElement> LastChanceRarityRateList { get; init; }

        public List<RarityRateDetail> RarityRateDetailList { get; init; }

        public List<RarityRateDetail> LastChanceRarityRateDetailList { get; init; }
    }
}