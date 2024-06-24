using System;

namespace DaysRemaining.Utilities
{
    internal class DayMonitor
    {
        private const string CVAR_VENDING_EXPIRATION = "daysRemainingVendingExpiration";
        private const string BUFF_VENDING_EXPIRATION = "buffDaysRemainingVendingExpiration";
        private const string BUFF_VENDING_EXPIRED = "buffDaysRemainingVendingRentalExpired";

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

        /// <summary>
        /// Call this to update the remaining expiration days for all players (including the local/primary player).
        /// </summary>
        internal static void SetExpirationDaysRemaining()
        {
            SetExpirationDaysRemaining(GameManager.Instance.World.GetPrimaryPlayer());

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
                _ = player.Buffs.AddBuff(daysRemaining > 0 ? BUFF_VENDING_EXPIRATION : BUFF_VENDING_EXPIRED);
            }
        }

        /// <summary>
        /// Call this to update the primary player's (local) data related to vending expiration date.
        /// </summary>
        /// <param name="primaryPlayer">EntityPlayerLocal to update.</param>
        /// <remarks>While we could use rentalEndTime instead of setting a CVAR, following the same pattern for local player that we do for remote players helps to keep things consistent across the buffs and other hooks.</remarks>
        internal static void SetExpirationDaysRemaining(EntityPlayerLocal primaryPlayer)
        {
            if (primaryPlayer == null)
            {
                _log.Warn("Called SetExpirationDaysRemaining with a null EntityPlayerLocal! This is not expected on a dedicated server, but will not break the game.");
                return;
            }

            if (primaryPlayer.RentedVMPosition == Vector3i.zero)
            {
                return; // primary player does not have a vending machine rental
            }
            var daysRemaining = Math.Max(primaryPlayer.RentalEndDay - GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime), 0);
            if (daysRemaining != primaryPlayer.GetCVar(CVAR_VENDING_EXPIRATION))
            {
                primaryPlayer.SetCVar(CVAR_VENDING_EXPIRATION, daysRemaining);
                _ = primaryPlayer.Buffs.AddBuff(daysRemaining > 0 ? BUFF_VENDING_EXPIRATION : BUFF_VENDING_EXPIRED);
            }
        }

        /// <summary>
        /// Well... is it a new day or not?
        /// </summary>
        /// <param name="day">The current day (in case the caller needs to use it for something).</param>
        /// <returns>Whether it is a new day since the last check.</returns>
        private static bool IsNewDay(out int day)
        {
            day = GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime);
            return day != CurrentDay;
        }
    }
}
