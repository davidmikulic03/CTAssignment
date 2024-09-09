using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase {

    private GameInput Input;
    private Entity Player;

    protected override void OnCreate() {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMoveInput>();
        Input = new GameInput();
    }
    protected override void OnStartRunning() {
        Input.Enable();
        Input.Gameplay.Fire.performed += OnFire;
        Player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }
    private void OnFire(InputAction.CallbackContext context) {
        if (!SystemAPI.Exists(Player)) return;

        SystemAPI.SetComponentEnabled<FireProjectileTag>(Player, true); 
    }
    protected override void OnUpdate() {
        Vector2 moveInput = Input.Gameplay.Move.ReadValue<Vector2>();

        foreach (var input in SystemAPI.Query<RefRW<PlayerMoveInput>>()) {
            input.ValueRW.Value = moveInput;
        }
    }
    protected override void OnStopRunning() {
        Input.Disable();
        Player = Entity.Null;
    }
}
