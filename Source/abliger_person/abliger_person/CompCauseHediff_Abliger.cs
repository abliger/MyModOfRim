using Verse;
using Verse.Sound;
namespace RimWorld
{
    public class CompCauseHediff_Abliger : ThingComp
    {
        private CompProperties_Abliger Props
        {
            get
            {
                return (CompProperties_Abliger)this.props;
            }
        }
        // 获得电力信息
        private CompPowerTrader PowerTrader
        {
            get
            {
                // 使用 TryGetComp 方法通过范型获得 comp 集合中的对应属性对象
                return this.parent.TryGetComp<CompPowerTrader>();
            }
        }

        // 判断哪些棋子受影响
        private bool IsPawnAffected(Pawn target)
        {
            return (this.PowerTrader == null || this.PowerTrader.PowerOn) && !target.Dead && target.health != null && target.Position.DistanceTo(this.parent.Position) <= this.Props.range && target.IsPrisoner;
        }

        public override void CompTick()
        {
            if (!this.parent.IsHashIntervalTick(10))
            {
                return;
            }
            // 没有电直接返回
            CompPowerTrader compPowerTrader = this.parent.TryGetComp<CompPowerTrader>();
            if (compPowerTrader != null && !compPowerTrader.PowerOn)
            {
                return;
            }
            this.lastIntervalActive = false;
            foreach (Pawn pawn in this.parent.Map.mapPawns.AllPawnsSpawned)
            {
                if (this.IsPawnAffected(pawn))
                {
                    // 获得 症状对象
                    Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(this.Props.hediff, false);
                    if (hediff == null)
                    {
                        // 在大脑处添加症状
                        hediff = pawn.health.AddHediff(this.Props.hediff, pawn.health.hediffSet.GetBrain(), null, null);
                        hediff.Severity = 1f;
                        HediffComp_Link hediffComp_Link = hediff.TryGetComp<HediffComp_Link>();
                        if (hediffComp_Link != null)
                        {
                            hediffComp_Link.drawConnection = false;
                            hediffComp_Link.other = this.parent;
                        }
                    }
                    HediffComp_Disappears hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
                    if (hediffComp_Disappears == null)
                    {
                        Log.ErrorOnce("CompCauseHediff_AoE has a hediff in props which does not have a HediffComp_Disappears", 78945945);
                    }
                    else
                    {
                        hediffComp_Disappears.ticksToDisappear = 10 + 1;
                    }
                    this.lastIntervalActive = true;
                }
            }
        }
        public override void PostDraw()
        {
            if (Find.Selector.SelectedObjectsListForReading.Contains(this.parent))
            {
                foreach (Pawn pawn in this.parent.Map.mapPawns.AllPawnsSpawned)
                {
                    if (this.IsPawnAffected(pawn))
                    {
                        GenDraw.DrawLineBetween(pawn.DrawPos, this.parent.DrawPos);
                    }
                }
            }
        }

        private Sustainer activeSustainer;

        private bool lastIntervalActive;
    }
}