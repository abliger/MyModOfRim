using Verse;
namespace RimWorld
{
    public class CompProperties_Abliger : CompProperties
    {
        public CompProperties_Abliger()
        {
            this.compClass = typeof(CompCauseHediff_Abliger);
        }

        public override void ResolveReferences(ThingDef parentDef)
        {
            base.ResolveReferences(parentDef);
            if (this.range == 0f && parentDef.specialDisplayRadius == 0f)
            {
                Log.Error("use Thingdef's specialDisplayRadius or add CompProperties_Abliger's range label");
            }
            else
            {
                this.range = parentDef.specialDisplayRadius;
            }
        }

        public float range;
        public HediffDef hediff;
    }
}