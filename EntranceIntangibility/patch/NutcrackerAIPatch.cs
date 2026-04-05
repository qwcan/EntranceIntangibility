using System.Linq;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace EntranceIntangibility.patch;

[HarmonyPatch(typeof(NutcrackerEnemyAI))]
public class NutcrackerAIPatch
{
    
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(NutcrackerEnemyAI.CheckLineOfSightForLocalPlayer))]
    private static void CheckLineOfSightForLocalPlayerPostfixPatch(
        NutcrackerEnemyAI __instance, ref bool __result
    )
    {
        if (__result && Plugin.Instance.IsIntangible((int)GameNetworkManager.Instance.localPlayerController.playerClientId))
        {
            //Plugin.Log.LogInfo( $"Player {GameNetworkManager.Instance.localPlayerController.playerClientId} is intangible, ignoring nutcracker LOS");
            __result = false;
        }

    }
}