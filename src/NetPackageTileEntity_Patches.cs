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
        public static void Postfix(TileEntity _te)
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
                                Helpers.SetExpirationDaysRemaining(player, tileEntityVendingMachine.RentalEndDay);
                            }
                        }
                    }
                }

                //_log.Trace($"triggered; rentalEndDay is {__state} and was converted to {___rentalEndDay}");
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }

    //[HarmonyPatch(typeof(TileEntityVendingMachine), "write")]
    internal class TileEntityVendingMachine_write_Patches
    {
        private static readonly ModLog<TileEntityVendingMachine_write_Patches> _log = new ModLog<TileEntityVendingMachine_write_Patches>();

        public static void Prefix(TileEntityVendingMachine __instance, ref int ___rentalEndDay, ref int __state)
        {
            try
            {
                __state = ___rentalEndDay;
                ___rentalEndDay = (int)__instance.RentTimeRemaining;
                _log.Trace($"triggered; rentalEndDay is {__state} and was converted to {___rentalEndDay}");
            }
            catch (Exception e)
            {
                _log.Error("Prefix", e);
            }
        }

        public static void Postfix(ref int ___rentalEndDay, ref int __state)
        {
            try
            {
                ___rentalEndDay = __state;
                _log.Trace($"triggered; rentalEndDay has been set back to {___rentalEndDay} for back-end consistency");
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }

    //[HarmonyPatch(typeof(TileEntityVendingMachine), "read")]
    internal class TileEntityVendingMachine_read_Patches
    {
        private static readonly ModLog<TileEntityVendingMachine_read_Patches> _log = new ModLog<TileEntityVendingMachine_read_Patches>();

        public static void Prefix(TileEntityVendingMachine __instance, ref int ___rentalEndDay, ref int __state)
        {
            try
            {
                _log.Trace($"triggered; rentalEndDay is {___rentalEndDay}");
                __state = ___rentalEndDay;
            }
            catch (Exception e)
            {
                _log.Error("Prefix", e);
            }
        }

        public static void Postfix(ref int ___rentalEndDay, ref int __state)
        {
            try
            {
                if (___rentalEndDay < __state)
                {
                    _log.Trace($"triggered; rentalEndDay was changed to {___rentalEndDay}; changing it back");
                    ___rentalEndDay = __state;
                }
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }

    //[HarmonyPatch(typeof(NetPackageTileEntity), "Setup")]
    internal class NetPackageTileEntity_Setup_PatchesXXX
    {
        private static readonly ModLog<NetPackageTileEntity_Setup_PatchesXXX> _log = new ModLog<NetPackageTileEntity_Setup_PatchesXXX>();

        public static void Prefix(World _world, bool ___bValidEntityId, int ___teEntityId, int ___clrIdx, Vector3i ___teWorldPos)
        {
            try
            {
                if (_world == null)
                {
                    return;
                }
                var tileEntity = ___bValidEntityId ? _world.GetTileEntity(___teEntityId) : _world.GetTileEntity(___clrIdx, ___teWorldPos);
                if (tileEntity == null)
                {
                    return;
                }
                // TODO: if self is host, modify rentalEndDay to days remaining for this data
                if (tileEntity is TileEntityVendingMachine vending)
                {

                    return;
                }
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }

    //[HarmonyPatch(typeof(NetPackageTileEntity), "ProcessPackage")]
    internal class NetPackageTileEntity_ProcessPackage_Patches
    {
        private static readonly ModLog<NetPackageTileEntity_ProcessPackage_Patches> _log = new ModLog<NetPackageTileEntity_ProcessPackage_Patches>();

        public static void Prefix(NetPackageTileEntity __instance)
        {
            try
            {
                // TODO: if self is server, echo back a
                _log.Trace("PREFIX CALLED");
            }
            catch (Exception e)
            {
                _log.Error("Prefix", e);
            }
        }

        public static void Postfix(NetPackageTileEntity __instance)
        {
            try
            {
                // TODO: if self is server, echo back a
                _log.Trace("POSTFIX CALLED");
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }
}
