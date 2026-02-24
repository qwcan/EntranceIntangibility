using System;
using System.Collections;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace EntranceIntangibility.patch;

[HarmonyPatch(typeof(EntranceTeleport))]
public class EntranceTeleportPatch
{
    [HarmonyPatch(nameof(EntranceTeleport.TeleportPlayerClientRpc))]
    [HarmonyPostfix]
    private static void TeleportPlayerClientRpcPatch(EntranceTeleport __instance, int playerObj)
    {
        Plugin.Log.LogInfo($"Player {playerObj} entered the facility, giving them intangibility for {Plugin.Config.IntangibilityDuration.Value} seconds: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
        Plugin.Instance.LastEnteredDoor[playerObj] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
    
}