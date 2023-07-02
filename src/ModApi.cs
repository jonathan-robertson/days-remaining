using DaysRemaining.Utilities;
using HarmonyLib;
using System;
using System.Reflection;

namespace DaysRemaining
{
    internal class ModApi : IModApi
    {
        private static readonly ModLog<ModApi> _log = new ModLog<ModApi>();

        public static bool DebugMode { get; set; } = true; // TODO: disable before release

        public void InitMod(Mod _modInstance)
        {
            var harmony = new Harmony(GetType().ToString());
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            // TODO: confirm if rental state only updates when vend is closed
            // TODO: on rental agreement changed, calculate and send cvar

            ModEvents.PlayerSpawnedInWorld.RegisterHandler(OnPlayerSpawnedInWorld);
            ModEvents.GameUpdate.RegisterHandler(OnGameUpdate);
        }

        private void OnGameUpdate()
        {
            try
            {
                DayMonitor.OnGameUpdate();
            }
            catch (Exception e)
            {
                _log.Error($"OnGameUpdate", e);
            }
        }

        private void OnPlayerSpawnedInWorld(ClientInfo _cInfo, RespawnType _respawnReason, Vector3i _pos)
        {
            try
            {
                if (_cInfo == null) // local client
                {
                    // TODO: var ppId = ((_cInfo != null) ? _cInfo.InternalId : null) ?? PlatformManager.InternalLocalUserIdentifier;
                    return;
                }

                switch (_respawnReason)
                {
                    case RespawnType.JoinMultiplayer:
                        if (GameManager.Instance.World.Players.dict.TryGetValue(_cInfo.entityId, out var player))
                        {
                            DayMonitor.SetExpirationDaysRemaining(_cInfo, player);
                        }
                        return;
                }
            }
            catch (Exception e)
            {
                _log.Error("OnPlayerSpawnedInWorld", e);
            }
        }
    }
}
