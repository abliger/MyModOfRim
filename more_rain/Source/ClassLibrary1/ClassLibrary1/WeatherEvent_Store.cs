﻿using Mono.Unix.Native;
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
            this.list = (from c in this.map.AllCells where !c.Roofed(this.map) && c.InBounds(this.map) && c.Standable(this.map) select c).ToList<IntVec3>() ;
        }

        public override bool Expired => flag;
        public bool flag = false;
        public List<IntVec3> list;
        public DamageDef damageDef= DefDatabase<DamageDef>.GetNamed("CrushingInjury");
        private List<ThingDef> stone = ThingCategoryDefOf.StoneBlocks.childThingDefs;

        public override void FireEvent()
        {
            
        }

        public override void WeatherEventTick()
        {
            this.flag = true;
            Thing thing = ThingMaker.MakeThing(stone[new Random().Next(0,stone.Count)], null);
            thing.SetForbidden(true);
            thing.stackCount = 10;
            GenSpawn.Spawn(thing, RandomPostionToDamage(RandomPostion()) , this.map, WipeMode.FullRefund);
        }
        public IntVec3 RandomPostion()
        {
            return list[new Random().Next(0, list.Count)];
        }

        public IntVec3 RandomPostionToDamage(IntVec3 position)
        {
            Pawn pawn = position.GetFirstPawn(this.map);
            if (pawn != null)
            {
                pawn.TakeDamage(new DamageInfo(this.damageDef, damageDef.defaultDamage,damageDef.defaultArmorPenetration));
                return position;
            }
            Building build =position.GetFirstBuilding(this.map);
            if(build != null)
            {
                build.TakeDamage(new DamageInfo(this.damageDef, damageDef.defaultDamage, damageDef.defaultArmorPenetration));
            }
            
            return position;
        }
    }
}
    