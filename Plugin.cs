﻿using System;
using BepInEx;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
namespace InfiniteQuotaTime;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        // Plugin load logic goes here!
        // This script acts like a unity object.
        Logger.LogInfo($"Infinite Quota Time Active");
        harmony.PatchAll(typeof(InfiniteQuotaTime));
    }

    //This defines the InfiniteQuotaTime class to run on the TimeOfDay object on the Awake function
    [HarmonyPatch(typeof(TimeOfDay), "Awake")]
    
    class InfiniteQuotaTime
    {
        //This is a postfix - it runs AFTER the normal awake function for all TimeOfDay objects
        //Something to note is that there is only one TimeOfDay object - it's not like an enemy where there's many
        private static void Postfix(ref TimeOfDay __instance)
        {
          if(GameNetworkManager.Instance.isHostingGame){ // Only runs if the user running the mod is the host
            //Sets the starting amount of days until quota to 99 - change the number here to change the number of days until the quota is due 
            int numDaysTillQuota = 999;
            //References the singular time of day object and sets to the numDaysTillQuota value
            __instance.quotaVariables.deadlineDaysAmount = numDaysTillQuota;
          }
        }  
    }
}