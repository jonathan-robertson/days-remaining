namespace DaysRemaining
{
    internal class Helpers
    {
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

        public static void SetExpirationDaysRemaining(EntityPlayer player)
        {
            SetExpirationDaysRemaining(player, player.RentalEndDay);
        }

        public static void SetExpirationDaysRemaining(EntityPlayer player, int rentalEndDay)
        {
            if (rentalEndDay == 0)
            {
                return;
            }
            var daysRemaining = rentalEndDay - GameUtils.WorldTimeToDays(GameManager.Instance.World.worldTime);
            if (daysRemaining < 0)
            {
                daysRemaining = 0;
            }
            player.SetCVar("daysRemainingVendingExpiration", daysRemaining);
            _ = player.Buffs.AddBuff("buffDaysRemainingVendingExpiration");
        }
    }
}
