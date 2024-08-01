using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class EnemySpawnPointComponentAuthoring : MonoBehaviour
    {
        public GameObject EnemyEntity;

        public class EnemySpawnPointComponentBaker : Baker<EnemySpawnPointComponentAuthoring>
        {
            public override void Bake(EnemySpawnPointComponentAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new EnemySpawnPointComponent
                    {
                        EnemyPrefab = GetEntity(authoring.EnemyEntity, TransformUsageFlags.Dynamic)
                    });
            }
        }
    }
}