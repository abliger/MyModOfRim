using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Verse;

namespace RimWorld
{
    public class CompProperties_Abliger_Ablity : CompProperties_AbilityEffect
    {
        public CompProperties_Abliger_Ablity()
        {
            this.compClass = typeof(CompAbilityEffect_Abliger_Ablity);
        }
        public HediffDef hediff;
        public List<CompPropertiesAbligerDamage> compPropertiesAbligerDamage = new List<CompPropertiesAbligerDamage>();
        public class CompPropertiesAbligerDamage
        {
            public float radius;
            public DamageDef damageDef;
            public int damAmount =-1;
            public float armorPenetration =-1f;
            public bool ignoreSelfDam = false;
        }
    }
}