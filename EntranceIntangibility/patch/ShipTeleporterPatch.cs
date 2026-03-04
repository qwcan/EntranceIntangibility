using System;
using GameNetcodeStuff;
using HarmonyLib;

namespace EntranceIntangibility.patch;

[HarmonyPatch(typeof(ShipTeleporter))]
public class ShipTeleporterPatch
{
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(ShipTeleporter), "SetPlayerTeleporterId")]
    private static void SetPlayerTeleporterIdPatch(
        ShipTeleporter __instance, PlayerControllerB playerScript, int teleporterId )
    {
        //This *should* only be called when the player is actually teleported
        if (teleporterId == -1)
        {
            Plugin.Log.LogInfo($"Player {playerScript.playerClientId} teleported giving them intangibility for {Plugin.Config.IntangibilityDuration.Value} seconds: {DateTimeOffset.Now.ToUnixTimeMilliseconds()}");
            Plugin.Instance.LastEnteredDoor[(int)playerScript.playerClientId] = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}