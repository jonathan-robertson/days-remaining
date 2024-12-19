using DaysRemaining.Utilities;
using HarmonyLib;
using System;

namespace DaysRemaining.Patches
{
    [HarmonyPatch(typeof(TileEntityVendingMachine), nameof(TileEntityVendingMachine.Rent))]
    internal class TileEntityVendingMachine_Rent_Patches
    {
        private static readonly ModLog<TileEntityVendingMachine_Rent_Patches> _log = new ModLog<TileEntityVendingMachine_Rent_Patches>();

        /// <summary>
        /// Called from local TileEntityVendingMachine.Rent after attempting a rental purchase.
        /// </summary>
        /// <param name="__result">Whether or not the rental attempt resulted in a purchase (or extension) of the rental agreement.</param>
        public static void Postfix(bool __result)
        {
            try
            {
                if (!__result)
                {
                    return;
                }
                DayMonitor.SetExpirationDaysRemaining(GameManager.Instance.World.GetPrimaryPlayer());
            }
            catch (Exception e)
            {
                _log.Error("Postfix", e);
            }
        }
    }
}
