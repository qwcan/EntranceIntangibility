using System;
using GameNetcodeStuff;
using HarmonyLib;

namespace EntranceIntangibility.patch;

[HarmonyPatch(typeof(ShipTeleporter))]
public class ShipTeleporterPatch
{
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ShipTeleporter), "TeleportPlayerOutWithInverseTeleporter")]
    private static void SetTeleportPlayerOutWithInverseTeleporter(
        ShipTeleporter __instance, ref int playerObj )
    {
        Plugin.Log.LogInfo($"Player {playerObj} inverse teleported, giving them intangibility for {Plugin.Config.IntangibilityDuration.Value} seconds: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
        Plugin.Instance.LastEnteredDoor[playerObj] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }
}