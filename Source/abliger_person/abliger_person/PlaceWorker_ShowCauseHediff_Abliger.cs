using UnityEngine;
using Verse;
namespace RimWorld
{
    public class PlaceWorker_ShowCauseHediff_Abliger : PlaceWorker
    {
        // Token: 0x06009B5A RID: 39770 RVA: 0x00384BD8 File Offset: 0x00382DD8
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            CompProperties_Abliger compProperties = def.GetCompProperties<CompProperties_Abliger>();
            if (compProperties != null)
            {
                GenDraw.DrawRadiusRing(center, compProperties.range, Color.white, null);
            }
        }
    }
}