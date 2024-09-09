using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public partial struct SpawnerSystem : ISystem {
    public void OnCreate(ref SystemState state) {
        foreach (var spawner in SystemAPI.Query<RefRW<Spawner>>()) {
            spawner.ValueRW.random.InitState();
        }
    }

    public void OnUpdate(ref SystemState state) {
        uint seed = 1;
        foreach(RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>()) {
            if(spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime) {
                for(int i = 0; i < spawner.ValueRO.SpawnAmount; i++) {
                    Entity entity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);

                    spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
                    float2 nextSpawnLoc = spawner.ValueRW.random.NextFloat2Direction() * spawner.ValueRO.SpawnRadius;
                    nextSpawnLoc *= spawner.ValueRW.random.NextFloat();
                    state.EntityManager.SetComponentData(entity, LocalTransform.FromPosition(new float3(nextSpawnLoc, 0)));
                    var enemy = SystemAPI.GetComponentRW<Enemy>(entity);
                    if (enemy.IsValid) {
                        enemy.ValueRW.Random.state = seed;
                        seed++;
                    }
                }
            }
        }
    }
}
