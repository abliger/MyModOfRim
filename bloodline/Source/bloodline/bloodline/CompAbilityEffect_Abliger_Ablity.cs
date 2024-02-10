using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using Verse;
using Verse.Noise;

namespace RimWorld
{
    public class CompAbilityEffect_Abliger_Ablity : CompAbilityEffect
    {
        private new CompProperties_Abliger_Ablity Props
        {
            get
            {
                return (CompProperties_Abliger_Ablity)this.props;
            }
        }

        private Pawn Pawn
        {
            get
            {
                return this.parent.pawn;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            foreach (var item in this.Props.compPropertiesAbligerDamage)
            {
                GenExplosion.DoExplosion(this.Pawn.PositionHeld, this.Pawn.MapHeld, item.radius,item.damageDef, this.Pawn, item.damAmount,item.armorPenetration, 
                    null, null, null, null, null, 1f, 1, null, false, null, 0f, 1, 1f, false, null,
                    item.ignoreSelfDam? new List<Thing> { this.Pawn }:null,
                    null, true, 0.6f, 0f, true, null, 1f);
            }
            base.Apply(target, dest);
        }
        public override void CompTick()
        {
            if (this.parent.Casting)
            {
                
                Hediff hediff = this.Pawn.health.hediffSet.GetFirstHediffOfDef(this.Props.hediff, false);
                if (hediff == null)
                {
                    hediff = this.Pawn.health.AddHediff(this.Props.hediff, null, null, null);
                }

            }
        }
        // Mech_Diabolus
        public override void PostApplied(List<LocalTargetInfo> targets, Map map)
        {
            FleckMaker.WaterSplash(this.Pawn.Position.ToVector3Shifted(), this.Pawn.Map, 2f * 6f, 20f);
            Vector3 vector = Vector3.zero;
            Effecter effecter = EffecterDefOf.Vaporize_Heatwave.Spawn();
            foreach (LocalTargetInfo localTargetInfo in targets)
            {
                vector += localTargetInfo.Cell.ToVector3Shifted();
                effecter.Trigger(localTargetInfo.ToTargetInfo(map), TargetInfo.Invalid, -1);
                EffecterDefOf.MechChargerWasteProduced.Spawn().Trigger(localTargetInfo.ToTargetInfo(map), TargetInfo.Invalid, -1);
            }
            vector /= (float)targets.Count<LocalTargetInfo>();
            IntVec3 intVec = vector.ToIntVec3();
            EffecterDefOf.ApocrionAoeResolve.Spawn(intVec, map, 1f).EffectTick(new TargetInfo(intVec, map, false), new TargetInfo(intVec, map, false));
            effecter.Cleanup();
            Explosion explosion = (Explosion)GenSpawn.Spawn(ThingDefOf.Explosion, this.Pawn.Position, this.Pawn.Map, WipeMode.Vanish);
            explosion.Position = this.Pawn.Position;
            explosion.radius = 30f;
            explosion.damType = DamageDefOf.MechBandShockwave;
            explosion.damAmount = -1;
            List<IntVec3> list = new List<IntVec3> { };
            list.AddRange(DamageDefOf.MechBandShockwave.Worker.ExplosionCellsToHit(explosion));
            DamageDefOf.MechBandShockwave.Worker.ExplosionStart(explosion, list);
        }
    }
}