using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData {

    public Entity Prefab;
    public float2 SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;
    public int SpawnAmount;
    public float SpawnRadius;

    public Random random;
}
