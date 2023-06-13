using HarmonyLib;
using System;

namespace DaysRemaining.Patches
{
    [HarmonyPatch(typeof(GameManager), "SetWorldTime")]
    internal class GameManager_SetWorldTime_Patches
    {
        private static readonly ModLog<GameManager_SetWorldTime_Patches> _log = new ModLog<GameManager_SetWorldTime_Patches>();

        public static void Postfix()
        {
            try
            {
                var players = GameManager.Instance.World.Players.list;
                for (var i = 0; i < players.Count; i++)
                {
                    if (Helpers.TryGetClientInfo(players[i].entityId, out var clientInfo))
                    {
                        Helpers.SetExpirationDaysRemaining(clientInfo, players[i]);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }

    [HarmonyPatch(typeof(GameManager), "updateTimeOfDay")]
    internal class GameManager_updateTimeOfDay_Patches
    {
        private static readonly ModLog<GameManager_updateTimeOfDay_Patches> _log = new ModLog<GameManager_updateTimeOfDay_Patches>();

        public static void Postfix()
        {
            try
            {
                var players = GameManager.Instance.World.Players.list;
                for (var i = 0; i < players.Count; i++)
                {
                    if (Helpers.TryGetClientInfo(players[i].entityId, out var clientInfo))
                    {
                        Helpers.SetExpirationDaysRemaining(clientInfo, players[i]);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }
}
