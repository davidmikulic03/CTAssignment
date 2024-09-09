using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

public partial struct ProjectileSystem : ISystem {

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach(var (transform, moveSpeed) in SystemAPI.Query<RefRW<LocalTransform>, ProjectileMoveSpeed>()) {
            transform.ValueRW.Position += transform.ValueRO.Up() * moveSpeed.Value * deltaTime;
        }
    }
}
