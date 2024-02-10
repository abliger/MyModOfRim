using Verse;
namespace RimWorld
{
    public class CompProperties_Abliger : CompProperties
    {
        public CompProperties_Abliger()
        {
            // 此处获得建筑的逻辑处理类型,游戏获得此类型后自动执行此类型的 CompTick 方法
            this.compClass = typeof(CompCauseHediff_Abliger);
        }
        /*
          CompProperties_Abliger 文件解析 ThingDef > comps > li Class="CompProperties_Abliger"  标签下的内容
          该文件解析 hediff 和通过重写下面的方法获得 Thingdef 中的定义并赋值到 range 属性上.
          ResolveReferences 方法是解析 xml使一定会调用的方法,可让我们获得 ThingDef 定义的属性
         */
        public override void ResolveReferences(ThingDef parentDef)
        {
            base.ResolveReferences(parentDef);
            if (parentDef.specialDisplayRadius == 0f)
            {
                Log.Error("please add Thingdef's specialDisplayRadius");
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