using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem {
    //[BurstCompile]
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;

        //new PlayerMoveJob{ DeltaTime = deltaTime }.Schedule();

        foreach(var (transform, velocity, angularVelocity, input, acceleration, bounds) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<PlayerVelocity>, RefRW<PlayerAngularVelocity>, RefRO<PlayerMoveInput>, RefRO<PlayerAcceleration>, PlayerBounds>().WithAll<PlayerTag>()) {

            velocity.ValueRW.Value *= 0.99f;
            angularVelocity.ValueRW.Value *= 0.99f;

            angularVelocity.ValueRW.Value -= input.ValueRO.Value.x * acceleration.ValueRO.Value.x * deltaTime;
            velocity.ValueRW.Value += transform.ValueRO.Up().xy * input.ValueRO.Value.y * acceleration.ValueRO.Value.y * deltaTime;

            transform.ValueRW.Position.xy += velocity.ValueRO.Value * deltaTime;
            transform.ValueRW = transform.ValueRW.RotateZ(angularVelocity.ValueRO.Value * deltaTime);

            if(transform.ValueRO.Position.x > bounds.Value.x || transform.ValueRO.Position.x < -bounds.Value.x) 
                transform.ValueRW.Position.x = -math.sign(transform.ValueRW.Position.x) * bounds.Value.x;
            if (transform.ValueRO.Position.y > bounds.Value.y || transform.ValueRO.Position.y < -bounds.Value.y)
                transform.ValueRW.Position.y = -math.sign(transform.ValueRW.Position.y) * bounds.Value.y;
        }

    }
}