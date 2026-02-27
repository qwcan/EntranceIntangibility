using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using EntranceIntangibility.patch;
using GameNetcodeStuff;
using HarmonyLib;

namespace EntranceIntangibility;

[BepInPlugin(GUID, "EntranceIntangibility", "1.0.1")]
[BepInDependency("com.sigurd.csync", "5.0.1")] 
public class Plugin : BaseUnityPlugin
{
    
    public const string GUID = "EntranceIntangibility";
    public static Plugin Instance { get; set; }

    public static ManualLogSource Log => Instance.Logger;

    private readonly Harmony _harmony = new("EntranceIntangibility");

    internal new static EntranceConfig Config; 
    

    public Dictionary<int, long> LastEnteredDoor = new();

    public Plugin()
    {
        Instance = this;
    }
    
    
    
    
    private void Awake()
    {
        Config = new EntranceConfig(base.Config); 
        Log.LogInfo($"Applying patches...");
        ApplyPluginPatch();
        Log.LogInfo($"Patches applied");
    }

    public bool IsIntangible(int playerObj)
    {
        if (!LastEnteredDoor.TryGetValue(playerObj, out var value))
        {
            //Log.LogInfo($"Player {playerObj} has never entered the facility, assuming they are not intangible");
            return false;
        }

        bool intangible = DateTimeOffset.Now.ToUnixTimeMilliseconds() - value  <
                          Config.IntangibilityDuration.Value * 1000;
        //Log.LogInfo($"{value} vs {DateTimeOffset.Now.ToUnixTimeMilliseconds()} ( diff = {DateTimeOffset.Now.ToUnixTimeMilliseconds() - value } ) {Config.IntangibilityDuration.Value}");
        //Log.LogInfo($"Player {playerObj} is {(intangible ? "intangible" : "not intangible")}");
        return intangible;
    }

    /// <summary>
    /// Applies the patch to the game.
    /// </summary>
    private void ApplyPluginPatch()
    {
        _harmony.PatchAll(typeof(EnemyAIPatch));
        _harmony.PatchAll(typeof(PlayerControllerPatch));
        _harmony.PatchAll(typeof(EntranceTeleportPatch));
        _harmony.PatchAll(typeof(NetworkPatch));

        
    }
}