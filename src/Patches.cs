using HarmonyLib;
using System;

namespace DaysRemaining
{
    [HarmonyPatch(typeof(NetPackageTileEntity), "Setup", typeof(TileEntity), typeof(TileEntity.StreamModeWrite), typeof(byte))]
    internal class NetPackageTileEntity_Setup_Patches
    {
        private static readonly ModLog<NetPackageTileEntity_Setup_Patches> _log = new ModLog<NetPackageTileEntity_Setup_Patches>();

        /// <summary>
        /// Called from server-side NetPackageTileEntity.ProcessPackage after ingesting data from client.
        /// Also called from client to update server.
        /// </summary>
        /// <param name="_te">TileEntity to be inspected if also a TileEntityVendingMachine.</param>
        public static void Postfix(TileEntity _te, Vector3i ___teWorldPos)
        {
            try
            {
                if (Helpers.TryGetTileEntityVendingMachine(_te, out var tileEntityVendingMachine))
                {
                    if (Helpers.TryGetOwner(tileEntityVendingMachine, out var owner))
                    {
                        if (Helpers.TryGetClientInfo(owner, out var clientInfo))
                        {
                            if (GameManager.Instance.World.Players.dict.TryGetValue(clientInfo.entityId, out var player))
                            {
                                // update this data on server's end now since we know it'll be on the way next time a player save packet is sent from client.
                                clientInfo.latestPlayerData.rentedVMPosition = ___teWorldPos;
                                clientInfo.latestPlayerData.rentalEndDay = tileEntityVendingMachine.RentalEndDay;
                                Helpers.SetExpirationDaysRemaining(clientInfo, player);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }

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
