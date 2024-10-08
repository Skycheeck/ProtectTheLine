﻿using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class BulletAuthoring : MonoBehaviour
    {
        public int DamageOnHit;

        public class BulletBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Bullet {DamageOnHit = authoring.DamageOnHit});
            }
        }
    }
}