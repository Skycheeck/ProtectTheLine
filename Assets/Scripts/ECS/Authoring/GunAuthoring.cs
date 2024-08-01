using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class GunAuthoring : MonoBehaviour
    {
        public GameObject BulletPrefab;
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
                        TimeToFire = authoring.TimeToFire,
                        FireTimer = authoring.TimeToFire
                    });
            }
        }
    }
}