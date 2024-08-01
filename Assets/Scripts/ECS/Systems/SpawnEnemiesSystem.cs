using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ECS.Systems
{
    public partial struct SpawnEnemiesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.EntityManager.AddComponent<EnemySpawnTimer>(state.SystemHandle);
            ResetTimer(ref state);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RefRW<EnemySpawnTimer> enemySpawnTimer = SystemAPI.GetComponentRW<EnemySpawnTimer>(state.SystemHandle);
            ref EnemySpawnTimer spawnTimer = ref enemySpawnTimer.ValueRW;
            spawnTimer.Time -= SystemAPI.Time.DeltaTime;
                
            if (spawnTimer.Time > 0f) return;

            NativeArray<Entity> entityArray = SystemAPI.QueryBuilder().WithAll<EnemySpawnPointComponent>().WithAll<LocalToWorld>().Build().ToEntityArray(Allocator.Temp);
            Entity entity = entityArray[Random.Range(0, entityArray.Length)];
            Entity enemyPrefab = SystemAPI.GetComponentRO<EnemySpawnPointComponent>(entity).ValueRO.EnemyPrefab;
            float3 position = SystemAPI.GetComponentRO<LocalToWorld>(entity).ValueRO.Position;
            CreateEnemy(ref state, enemyPrefab, position);
            ResetTimer(ref state);
        }

        private void CreateEnemy(ref SystemState state, Entity enemyPrefab, float3 spawnPosition)
        {
            Entity instance = state.EntityManager.Instantiate(enemyPrefab);
            RefRW<LocalTransform> localTransform = SystemAPI.GetComponentRW<LocalTransform>(instance);
            localTransform.ValueRW = LocalTransform.FromPositionRotationScale(spawnPosition, quaternion.RotateZ(Mathf.Deg2Rad * 180), localTransform.ValueRO.Scale);
        }

        private void ResetTimer(ref SystemState state)
        {
            SystemAPI.SetComponent(state.SystemHandle, new EnemySpawnTimer {Time = 3f});
        }
    }
}