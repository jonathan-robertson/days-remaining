using System;

namespace DaysRemaining
{
    internal class Helpers
    {
        private static readonly ModLog<Helpers> _log = new ModLog<Helpers>();

        public static bool TryGetTileEntityVendingMachine(Vector3i blockPos, out TileEntityVendingMachine tileEntityVendingMachine)
        {
            var world = GameManager.Instance.World;
            var tileEntity = world.GetTileEntity(world.ChunkCache.ClusterIdx, blockPos);
            if (tileEntity == null || !(tileEntity is TileEntityVendingMachine te))
            {
                tileEntityVendingMachine = null;
                return false;
            }
            tileEntityVendingMachine = te;
            return true;
        }

        public static bool TryGetTileEntityVendingMachine(TileEntity tileEntity, out TileEntityVendingMachine tileEntityVendingMachine)
        {
            if (tileEntity is TileEntityVendingMachine te)
            {
                tileEntityVendingMachine = te;
                return true;
            }
            tileEntityVendingMachine = null;
            return false;
        }

        public static bool TryGetOwner(ILockable lockable, out PlatformUserIdentifierAbs owner)
        {
            owner = lockable.GetOwner();
            return owner != null;
        }

        public static bool TryGetClientInfo(PlatformUserIdentifierAbs id, out ClientInfo clientInfo)
        {
            clientInfo = ConnectionManager.Instance.Clients.ForUserId(id);
            return clientInfo != null;
        }

        public static bool TryGetClientInfo(int entityId, out ClientInfo clientInfo)
        {
            clientInfo = ConnectionManager.Instance.Clients.ForEntityId(entityId);
            return clientInfo != null;
        }

        public static bool TryGetVendingMachineRentalData(Vector3i blockPos, out PlatformUserIdentifierAbs owner, out int rentalEndDay)
        {
            if (!TryGetTileEntityVendingMachine(blockPos, out var tileEntityVendingMachine))
            {
                _log.Warn($"could not find vending machine tile entity at {blockPos}");
                owner = null;
                rentalEndDay = 0;
                return false;
            }
            owner = tileEntityVendingMachine.GetOwner();
            rentalEndDay = tileEntityVendingMachine.RentalEndDay;
            return true;
        }

        /// <summary>
        /// Call this to update the player's client-side data related to vending expiration date.
        /// </summary>
        /// <param name="clientInfo">ClientInfo containing the current rental information.</param>
        /// <param name="player">EntityPlayer to update.</param>
        public static void SetExpirationDaysRemaining(ClientInfo clientInfo, EntityPlayer player)
        {
            if (clientInfo == null || player == null)
            {
                _log.Warn($"ClientInfo and EntityPlayer params must not be null; ClientInfo {(clientInfo != null ? "exists" : "does not exist")}, EntityPlayer {(player != null ? "exists" : "does not exist")}.");
                return;
            }
            if (clientInfo.latestPlayerData.rentalEndDay == 0)
            {
                return;
            }
            var daysRemaining = Math.Max(clientInfo.latestPlayerData.rentalEndDay - GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime), 0);
            if (daysRemaining != player.GetCVar("daysRemainingVendingExpiration"))
            {
                player.SetCVar("daysRemainingVendingExpiration", daysRemaining);
                _ = player.Buffs.AddBuff("buffDaysRemainingVendingExpiration");
            }
        }
    }
}
