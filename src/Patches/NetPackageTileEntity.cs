using DaysRemaining.Utilities;
using HarmonyLib;
using System;

namespace DaysRemaining.Patches
{
    [HarmonyPatch(typeof(NetPackageTileEntity), nameof(NetPackageTileEntity.Setup), typeof(TileEntity), typeof(TileEntity.StreamModeWrite), typeof(byte))]
    internal class NetPackageTileEntity_Setup_Patches
    {
        private static readonly ModLog<NetPackageTileEntity_Setup_Patches> _log = new ModLog<NetPackageTileEntity_Setup_Patches>();

        /// <summary>
        /// Called from server-side NetPackageTileEntity.ProcessPackage after ingesting data from client.
        /// Also called from client to update server.
        /// </summary>
        /// <param name="_te">TileEntity to be inspected if also a TileEntityVendingMachine.</param>
        /// <param name="___teWorldPos">Vector3i calculated world position of this tile.</param>
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
                                // close the open trader (vending machine) window because the cvar cannot auto-refresh its ui value on its own
                                clientInfo.SendPackage(NetPackageManager.GetPackage<NetPackageConsoleCmdClient>().Setup("xui close trader", true));
                                DayMonitor.SetExpirationDaysRemaining(clientInfo, player);
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
}
