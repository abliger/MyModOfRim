using RimWorld;
using RimWorld.Planet;
using System;
using Verse;

namespace abliger
{
    public class BloodRain_Thought_Situational : Thought_Memory
    {
        public override int CurStageIndex
        {
            get
            {
                if (PawnKindDefOf.Sanguophage == this.pawn.kindDef || this.pawn.genes.HasGene(GeneDefOf.Hemogenic))
                {
                    return 1;
                }
                if (PawnKindDefOf.SanguophageThrall == this.pawn.kindDef || this.pawn.genes.HasGene(GeneDefOf.Hemogenic))
                {
                    return 2;
                }
                return 0;
            }
        }
    }
}
