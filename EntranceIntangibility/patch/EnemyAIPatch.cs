using System.Linq;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace EntranceIntangibility.patch;

[HarmonyPatch(typeof(EnemyAI))]
public class EnemyAIPatch
{
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(EnemyAI.PlayerIsTargetable))]
    private static void PlayerIsTargetablePatch(
        EnemyAI __instance, PlayerControllerB playerScript, ref bool __result
    )
    {
        if (Plugin.Instance.IsIntangible((int)playerScript.playerClientId))
        {
            __result = false;
        }
    }
    
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(EnemyAI.GetAllPlayersInLineOfSight))]
    private static void GetAllPlayersInLineOfSightPostfixPatch(
        EnemyAI __instance, float width, int range, Transform eyeObject, float proximityCheck,
        ref PlayerControllerB[] __result
    )
    {
        __result = __result?
            .Where(player => !Plugin.Instance.IsIntangible((int)player.playerClientId))
            .ToArray();
    }
    
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(EnemyAI.CheckLineOfSightForPlayer))]
    private static void CheckLineOfSightForPlayerPostfixPatch(
        EnemyAI __instance, float width, int range, int proximityAwareness, ref PlayerControllerB __result
    )
    {
        if (__result && Plugin.Instance.IsIntangible((int)__result.playerClientId))
        {
            __result = null;
        }
    }
    
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(EnemyAI.CheckLineOfSightForClosestPlayer))]
    private static void CheckLineOfSightForClosestPlayerPostfixPatch(
        EnemyAI __instance, float width, int range, int proximityAwareness, ref PlayerControllerB __result
    )
    {
        if (__result && Plugin.Instance.IsIntangible((int)__result.playerClientId))
        {
            __result = null;
        }

    }
}