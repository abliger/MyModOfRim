using Mono.Unix.Native;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.UIElements;
using Verse;

namespace abliger
{
    public class WeatherEvent_Store : WeatherEvent
    {
      
        public WeatherEvent_Store(Map map) : base(map)
        {
           this.list= this.map.AllCells.ToList<IntVec3>();
           Scribe_Defs.Look<DamageDef>(ref this.damageDef, "CrushingInjury");
        }

        public override bool Expired => age> Rand.Range(1200, 4000);
        public int age = default;
        public List<IntVec3> list;
        public DamageDef damageDef;

        public override void FireEvent()
        {
            
        }

        public override void WeatherEventTick()
        {
            this.age++;
            if(this.age % 200 == 0)
            {
            
            Thing thing = ThingMaker.MakeThing(ThingDefOf.BlocksGranite, null);
            thing.SetForbidden(true);
            thing.stackCount = 10;
                
            GenSpawn.Spawn(thing, RandomPostionToDamage(RandomPostion(default)) , this.map, WipeMode.FullRefund);
            }
        }
        public IntVec3 RandomPostion(IntVec3 position)
        {
            if(position == default)
            {
                position = list[new Random().Next(0, list.Count)];
            }
            if(!position.Roofed(this.map) && position.InBounds(this.map) && position.Standable(this.map) 
                && position.GetFirstBuilding(this.map) == null
                //position.GetFirstItem(this.map) == null || ThingDefOf.BlocksGranite == position.GetFirstItem(this.map).def
            )
            {
                return position;
            }
            else
            {
                return RandomPostion(default);
            }
        }

        public IntVec3 RandomPostionToDamage(IntVec3 position)
        {

            Pawn pawn = position.GetFirstPawn(this.map);
            if (pawn != null)
            {
                pawn.TakeDamage(new DamageInfo(this.damageDef,30f));
            }
            return position;
        }
    }
}
    