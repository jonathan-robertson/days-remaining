using HarmonyLib;
using System;

namespace DaysRemaining.Patches
{
    [HarmonyPatch(typeof(World), "SetTime")]
    internal class World_SetTime_Patches
    {
        private static readonly ModLog<World_SetTime_Patches> _log = new ModLog<World_SetTime_Patches>();

        private static int prevMinute = -1;

        /// <summary>
        /// Fire when game updates time of day on its own or when admin updates game time via `st` command such that it produces a minute change.
        /// </summary>
        /// <param name="_time">New time the world was just set to.</param>
        public static void Postfix(ulong _time)
        {
            try
            {
                var curMinute = GameUtils.WorldTimeToMinutes(_time);
                if (curMinute == prevMinute)
                {
                    return;
                }
                prevMinute = curMinute;

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
