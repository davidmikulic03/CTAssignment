using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float ProjectileSpeed;
    public float ProjectileLifeTime;
    public Vector2 Bounds = new Vector2(8.889875f, 5f);

    public class ProjectileBaker : Baker<ProjectileAuthoring> {
        public override void Bake(ProjectileAuthoring authoring) {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new ProjectileMoveSpeed { Value = authoring.ProjectileSpeed });
            AddComponent(entity, new PlayerBounds { Value = authoring.Bounds });
        }
    }
}
