using System;
using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class GunAuthoring : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public float FireRadius;
        public float TimeToFire;

        public class GunBaker : Baker<GunAuthoring>
        {
            public override void Bake(GunAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new Gun
                    {
                        BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic),
                        FireRadius = authoring.FireRadius,
                        TimeToFire = authoring.TimeToFire,
                        FireTimer = authoring.TimeToFire
                    });
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius: FireRadius);
        }
    }
}