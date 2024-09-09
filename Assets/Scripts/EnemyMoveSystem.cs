using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct EnemyMoveSystem : ISystem {
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        foreach(var (enemy, transform) in SystemAPI.Query<RefRW<Enemy>, RefRW<LocalTransform>>().WithAll<EnemyTag>()) {
            //if (enemy.ValueRO.Random.state == 0)
            //    enemy.ValueRW.Random.state++;
            enemy.ValueRW.Velocity += enemy.ValueRW.Random.NextFloat2Direction() * enemy.ValueRO.Acceleration * SystemAPI.Time.DeltaTime;
            enemy.ValueRW.Velocity = math.normalize(enemy.ValueRW.Velocity);
            transform.ValueRW.Position.xy += enemy.ValueRO.Velocity * SystemAPI.Time.DeltaTime;
        }
    }
}
