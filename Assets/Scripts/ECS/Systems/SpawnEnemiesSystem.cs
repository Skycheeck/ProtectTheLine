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
            state.RequireForUpdate<EnemySpawnTimer>();
            
            _enemyFactory = new EnemyFactory();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            RefRW<EnemiesLeft> enemiesLeft = SystemAPI.GetSingletonRW<EnemiesLeft>();
            
            if (enemiesLeft.ValueRO.Value < 1) return;
            
            RefRW<EnemySpawnTimer> enemySpawnTimer = SystemAPI.GetSingletonRW<EnemySpawnTimer>();
            enemySpawnTimer.ValueRW.TimeLeft -= SystemAPI.Time.DeltaTime;
                
            if (enemySpawnTimer.ValueRO.TimeLeft > 0f) return;

            NativeArray<Entity> entityArray = SystemAPI.QueryBuilder().WithAll<EnemySpawnPointComponent>().WithAll<LocalToWorld>().Build().ToEntityArray(Allocator.Temp);
            Entity entity = entityArray[Random.Range(0, entityArray.Length)];
            Entity enemyPrefab = SystemAPI.GetComponentRO<EnemySpawnPointComponent>(entity).ValueRO.EnemyPrefab;
            float3 position = SystemAPI.GetComponentRO<LocalToWorld>(entity).ValueRO.Position;
            
            _enemyFactory.Create(state.EntityManager, enemyPrefab, position);
            enemySpawnTimer.ValueRW.TimeLeft = Random.Range(enemySpawnTimer.ValueRO.Min, enemySpawnTimer.ValueRO.Max);
            enemiesLeft.ValueRW.Value--;
        }
    }
}