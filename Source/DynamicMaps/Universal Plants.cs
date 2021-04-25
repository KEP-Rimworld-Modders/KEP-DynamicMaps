﻿using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace DynamicMapGen
{
    [HarmonyPatch(typeof(WildPlantSpawner), "CalculatePlantsWhichCanGrowAt")]
    internal static class UniversalPlants
    {
        internal static void Postfix(IntVec3 c, Map ___map, List<ThingDef> outPlants)
        {
            ThingDef thingDef;
            thingDef = ThingDef.Named("DM_Pansy");
            if (!thingDef.CanEverPlantAt_NewTemp(c, ___map))
            {
                return;
            }
            outPlants.Add(thingDef);
            thingDef = ThingDef.Named("DM_Daffodil");
            if (!thingDef.CanEverPlantAt_NewTemp(c, ___map))
            {
                return;
            }
            outPlants.Add(thingDef);
            outPlants.Add(thingDef);
            thingDef = ThingDef.Named("DM_Dandelion");
            if (!thingDef.CanEverPlantAt_NewTemp(c, ___map))
            {
                return;
            }
            outPlants.Add(thingDef);
        }
    }
    [HarmonyPatch(typeof(WildPlantSpawner), "GetCommonalityOfPlant")]
    internal static class UniversalPlantsCommonality
    {
        internal static void Postfix(ref float __result, ThingDef plant)
        {
            if (plant.defName == "DM_Pansy")
                __result = 5;
            if (plant.defName == "DM_Daffodil")
                __result = 5;
            if (plant.defName == "DM_Dandelion")
                __result = 5;
        }
    }
    [HarmonyPatch(typeof(Plant), "Graphic")]
    internal static class PlantMaturityPatch
    {
        static Graphic postfix(ref Graphic __result, ThingDef ___def, float growthInt)
        {
            Log.Error("testing maturitypatch");
            if (___def.plant.immatureGraphic != null && growthInt > ___def.plant.harvestMinGrowth)
            {
                return ___def.graphic;
            }
            return __result;
        }

    }
}
