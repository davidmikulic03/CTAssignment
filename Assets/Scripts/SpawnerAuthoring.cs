using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour {


    public GameObject Prefab;
    public float SpawnRate;
    public float SpawnRadius;
    public int SpawnAmount;

    class SpawnerBaker : Baker<SpawnerAuthoring> {
        private Unity.Mathematics.Random rand = new Unity.Mathematics.Random();
        public override void Bake(SpawnerAuthoring authoring) {

            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            rand.InitState();

            AddComponent(entity,
                new Spawner {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    SpawnPosition = float2.zero,
                    NextSpawnTime = 0,
                    SpawnRate = authoring.SpawnRate,
                    SpawnAmount = authoring.SpawnAmount,
                    SpawnRadius = authoring.SpawnRadius,
                    random = rand
                });
        }
    }
}
