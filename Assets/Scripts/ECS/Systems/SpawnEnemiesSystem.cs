using DefaultNamespace;
using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Random = UnityEngine.Random;

namespace ECS.Systems
{
    public partial struct SpawnEnemiesSystem : ISystem
    {
        private EnemyFactory _enemyFactory;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemiesLeft>();
            
            state.EntityManager.AddComponent<EnemySpawnTimer>(state.SystemHandle);
            ResetTimer(ref state);
            _enemyFactory = new EnemyFactory();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RefRW<EnemiesLeft> enemiesLeft = SystemAPI.GetSingletonRW<EnemiesLeft>();
            
            if (enemiesLeft.ValueRO.Value < 1) return;
            
            RefRW<EnemySpawnTimer> enemySpawnTimer = SystemAPI.GetComponentRW<EnemySpawnTimer>(state.SystemHandle);
            ref EnemySpawnTimer spawnTimer = ref enemySpawnTimer.ValueRW;
            spawnTimer.Time -= SystemAPI.Time.DeltaTime;
                
            if (spawnTimer.Time > 0f) return;

            NativeArray<Entity> entityArray = SystemAPI.QueryBuilder().WithAll<EnemySpawnPointComponent>().WithAll<LocalToWorld>().Build().ToEntityArray(Allocator.Temp);
            Entity entity = entityArray[Random.Range(0, entityArray.Length)];
            Entity enemyPrefab = SystemAPI.GetComponentRO<EnemySpawnPointComponent>(entity).ValueRO.EnemyPrefab;
            float3 position = SystemAPI.GetComponentRO<LocalToWorld>(entity).ValueRO.Position;
            
            _enemyFactory.Create(state.EntityManager, enemyPrefab, position);
            ResetTimer(ref state);
            enemiesLeft.ValueRW.Value--;
        }
        

        private void ResetTimer(ref SystemState state)
        {
            SystemAPI.SetComponent(state.SystemHandle, new EnemySpawnTimer {Time = 3f});
        }
    }
}