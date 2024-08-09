using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IEnemyFactory
    {
        void Create(EntityManager entityManager, Entity enemyPrefab, float3 spawnPosition);
    }

    public struct EnemyFactory : IEnemyFactory
    {
        public void Create(EntityManager entityManager, Entity enemyPrefab, float3 spawnPosition)
        {
            Entity instance = entityManager.Instantiate(enemyPrefab);
            LocalTransform localTransform = entityManager.GetComponentData<LocalTransform>(instance);
            localTransform = LocalTransform.FromPositionRotationScale(spawnPosition, quaternion.RotateZ(Mathf.Deg2Rad * 180), localTransform.Scale);
            entityManager.SetComponentData(instance, localTransform);
        }
    }
}