using DaysRemaining.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaysRemaining.Utilities
{
    internal class DayMonitor
    {
        private const string CVAR_VENDING_EXPIRATION = "daysRemainingVendingExpiration";
        private const string BUFF_VENDING_EXPIRATION = "buffDaysRemainingVendingExpiration";

        internal static int CurrentDay { get; private set; } = -1;

        private static readonly ModLog<DayMonitor> _log = new ModLog<DayMonitor>();

        internal static void OnGameUpdate()
        {
            if (IsNewDay(out var day))
            {
                CurrentDay = day;
                SetExpirationDaysRemaining();
            }
        }

        internal static void SetExpirationDaysRemaining()
        {
            var players = GameManager.Instance.World.Players.list;
            for (var i = 0; i < players.Count; i++)
            {
                if (Helpers.TryGetClientInfo(players[i].entityId, out var clientInfo))
                {
                    SetExpirationDaysRemaining(clientInfo, players[i]);
                }
            }
        }

        /// <summary>
        /// Call this to update the player's client-side data related to vending expiration date.
        /// </summary>
        /// <param name="clientInfo">ClientInfo containing the current rental information.</param>
        /// <param name="player">EntityPlayer to update.</param>
        /// <remarks>Would've loved to use rentalEndTime here, but it isn't reported to the server (just rentalEndDay). Maybe rentalEndTime is deprecated?</remarks>
        internal static void SetExpirationDaysRemaining(ClientInfo clientInfo, EntityPlayer player)
        {
            if (clientInfo == null || player == null)
            {
                _log.Warn($"ClientInfo and EntityPlayer params must not be null; ClientInfo {(clientInfo != null ? "exists" : "does not exist")}, EntityPlayer {(player != null ? "exists" : "does not exist")}.");
                return;
            }

            if (clientInfo.latestPlayerData.rentedVMPosition == Vector3i.zero)
            {
                return; // player does not have a vending machine rental
            }
            var daysRemaining = Math.Max(clientInfo.latestPlayerData.rentalEndDay - GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime), 0);
            if (daysRemaining != player.GetCVar(CVAR_VENDING_EXPIRATION))
            {
                player.SetCVar(CVAR_VENDING_EXPIRATION, daysRemaining);
                _ = player.Buffs.AddBuff(BUFF_VENDING_EXPIRATION);
            }
        }

        private static bool IsNewDay(out int day)
        {
            day = GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime);
            return day != CurrentDay;
        }
    }
}
