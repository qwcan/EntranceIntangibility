using System.Numerics;
using GameNetcodeStuff;
using HarmonyLib;

namespace EntranceIntangibility.patch;

[HarmonyPatch(typeof(PlayerControllerB))]
public class PlayerControllerPatch
{
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(PlayerControllerB.DamagePlayer))]
    private static bool DamagePlayerPrefixPatch(
        PlayerControllerB __instance, int damageNumber, CauseOfDeath causeOfDeath, bool fallDamage, Vector3 force
    )
    {
        if (Plugin.Instance.IsIntangible((int)__instance.playerClientId))
        {
            //Plugin.Log.LogInfo( $"Player {__instance.playerClientId} is intangible, ignoring damage");
            return false;
        }

        return true;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PlayerControllerB.AllowPlayerDeath))]
    private static void AllowPlayerDeathPatch(PlayerControllerB __instance, ref bool __result)
    {
        if( Plugin.Instance.IsIntangible( (int)__instance.playerClientId ) )
        {
            //Plugin.Log.LogInfo( $"Player {__instance.playerClientId} is intangible, ignoring damage");
            __result = false;
        }
    }
    
    
}