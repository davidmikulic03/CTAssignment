
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct EnsureBoundsSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        foreach(var (transform, bounds) in SystemAPI.Query<RefRW<LocalTransform>, PlayerBounds>()) {
            
            if (transform.ValueRO.Position.x > bounds.Value.x || transform.ValueRO.Position.x < -bounds.Value.x)
                transform.ValueRW.Position.x = -math.sign(transform.ValueRW.Position.x) * bounds.Value.x;
            if (transform.ValueRO.Position.y > bounds.Value.y || transform.ValueRO.Position.y < -bounds.Value.y)
                transform.ValueRW.Position.y = -math.sign(transform.ValueRW.Position.y) * bounds.Value.y;
        }
    }
}
