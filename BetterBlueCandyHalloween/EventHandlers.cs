using Exiled.API.Enums;
using MEC;
using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using PlayerRoles;

namespace BetterBlueCandyHalloween
{
    public class EventHandlers : Plugin<Config>
    {
        public override string Author => "Bankokwak";
        public override string Name => "Better Blue Candy Halloween";
        public override Version Version => new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(8, 13, 1);
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Scp330.EatingScp330 += OnEat;
            base.OnEnabled();
        }
    
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Scp330.EatingScp330 -= OnEat;
            base.OnDisabled();
        }
        
        private void OnEat(EatingScp330EventArgs ev)
        {
            if (ev.Candy.Kind == CandyKindID.Blue)
            {
                ev.IsAllowed = false;
                var RolePlayer = ev.Player.Role.Type;
                var posPlayer = ev.Player.Position;
                var healthPlayer = ev.Player.Health;
                ev.Player.CurrentItem.Destroy();
                ev.Player.DropItems();
                ev.Player.Role.Set(RoleTypeId.Tutorial);
                ev.Player.IsGodModeEnabled = false;
                ev.Player.Position = posPlayer;
                ev.Player.Health = healthPlayer;
                ev.Player.EnableEffect(EffectType.Marshmallow);
                ev.Player.AddAhp(base.Config.ShieldBlueCandy, base.Config.ShieldBlueCandy, 0f);
                Timing.CallDelayed(base.Config.TimeBlueCandy, () =>
                {
                    ev.Player.DisableEffect(EffectType.Marshmallow);
                    ev.Player.ArtificialHealth = 0f;
                    var posPlayer = ev.Player.Position;
                    var healthPlayer = ev.Player.Health;
                    ev.Player.Role.Set(RolePlayer);
                    ev.Player.Position = posPlayer;
                    ev.Player.Health = healthPlayer;
                    ev.Player.EnableEffect(EffectType.Blinded, 1, 5f);
                });
            }
        }
    }
}
