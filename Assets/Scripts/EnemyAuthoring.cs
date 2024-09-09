using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour {
    public float MoveSpeed;
    public float Acceleration;

    public class EnemyBaker : Baker<EnemyAuthoring> {
        
        public override void Bake(EnemyAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            uint randNum = (uint)UnityEngine.Random.Range(0, 10000);
            AddComponent(entity, new EnemyTag());
            AddComponent(entity, new Enemy { Acceleration = authoring.Acceleration, MoveSpeed = authoring.MoveSpeed, Velocity = 0, Random = new(1) });
            AddComponent(entity, new PlayerBounds { Value = new float2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize) });
        }
    }
}

public struct Enemy : IComponentData {
    public float MoveSpeed;
    public float Acceleration;
    public float2 Velocity;

    public Unity.Mathematics.Random Random;
}

public struct EnemyTag : IComponentData { }