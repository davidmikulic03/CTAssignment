using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{

    public float LinearAcceleration;
    public float AngularAcceleration;
    public Vector2 Bounds = new Vector2(8.889875f, 5f);
    public GameObject ProjectilePrefab;

    class PlayerBaker : Baker<PlayerAuthoring> {
        public override void Bake(PlayerAuthoring authoring) {
            Entity playerEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerTag>(playerEntity);
            AddComponent<PlayerMoveInput>(playerEntity);
            AddComponent(playerEntity, new PlayerAcceleration { Value = new(authoring.AngularAcceleration, authoring.LinearAcceleration) });
            AddComponent(playerEntity, new PlayerBounds { Value = authoring.Bounds });
            AddComponent<PlayerVelocity>(playerEntity);
            AddComponent<PlayerAngularVelocity>(playerEntity);
            AddComponent(playerEntity, new ProjectilePrefab { Value = GetEntity(authoring.ProjectilePrefab, TransformUsageFlags.Dynamic) });
            AddComponent<FireProjectileTag>(playerEntity);
            SetComponentEnabled<FireProjectileTag>(playerEntity, false);
        }
    }
}

public struct PlayerMoveInput : IComponentData {
    public float2 Value;
}
public struct PlayerAcceleration : IComponentData {
    public float2 Value;
}
public struct PlayerVelocity : IComponentData {
    public float2 Value;
}
public struct PlayerAngularVelocity : IComponentData {
    public float Value;
}
public struct PlayerBounds : IComponentData {
    public float2 Value;
}

public struct PlayerTag : IComponentData { }
public struct ProjectilePrefab : IComponentData {
    public Entity Value;
}
public struct ProjectileMoveSpeed : IComponentData {
    public float Value;
}
public struct DestroyTag : IComponentData, IEnableableComponent { }
public struct ProjectileLifeTime : IComponentData {
    public float Value;
}
public struct ProjectileLifeTimeCounter : IComponentData {
    public float Value;
}

public struct FireProjectileTag : IComponentData, IEnableableComponent { }