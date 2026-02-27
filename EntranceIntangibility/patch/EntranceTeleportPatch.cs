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
    [HarmonyPrefix]
    private static void TeleportPlayerClientRpcPatch(EntranceTeleport __instance, int playerObj)
    {
        Plugin.Log.LogInfo($"Player {playerObj} entered the facility (ClientRPC), giving them intangibility for {Plugin.Config.IntangibilityDuration.Value} seconds: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
        Plugin.Instance.LastEnteredDoor[playerObj] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
    
    
    [HarmonyPatch(nameof(EntranceTeleport.TeleportPlayerServerRpc))]
    [HarmonyPrefix]
    private static void TeleportPlayerServerRpcPatch(EntranceTeleport __instance, int playerObj)
    {
        Plugin.Log.LogInfo($"Player {playerObj} entered the facility (ServerRPC), giving them intangibility for {Plugin.Config.IntangibilityDuration.Value} seconds: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
        Plugin.Instance.LastEnteredDoor[playerObj] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
    
}