using System.Linq;
using DefaultNamespace;
using ECS.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace ECS.Systems
{
    public partial struct GameOverSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemiesLeft>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.QueryBuilder().WithAll<GameIsOver>().Build().IsEmpty) return;
            EntityCommandBuffer ecb = new(Allocator.Temp);
            
            if (CheckForVictory(ref state, ref ecb)) return;
            CheckForDefeat(ref state, ref ecb);
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        private bool CheckForVictory(ref SystemState state, ref EntityCommandBuffer entityCommandBuffer)
        {
            if (SystemAPI.GetSingleton<EnemiesLeft>().Value > 0) return false;
            if (!SystemAPI.QueryBuilder().WithAll<Enemy>().Build().IsEmpty) return false;
            
            CreateGameIsOverEntity(ref state, ref entityCommandBuffer);
            ShowRestartScreen(ref state, "Victory");
            return true;

        }

        private void CheckForDefeat(ref SystemState state, ref EntityCommandBuffer entityCommandBuffer)
        {
            foreach (RefRO<Health> health in SystemAPI.Query<RefRO<Health>>().WithAll<Player>().WithChangeFilter<Health>())
            {
                if (!(health.ValueRO.Value <= 0)) continue;
                
                CreateGameIsOverEntity(ref state, ref entityCommandBuffer);
                ShowRestartScreen(ref state, "Defeat");
            }
        }

        private void ShowRestartScreen(ref SystemState state, string title)
        {
            RestartScreen restartScreen = RestartScreen.Instance;
            restartScreen.gameObject.SetActive(true);
            restartScreen.SetTitle(title);
        }

        private void CreateGameIsOverEntity(ref SystemState state, ref EntityCommandBuffer ecb)
        {
            Entity entity = SystemAPI.GetSingletonEntity<EnemiesLeft>();
            ecb.AddComponent(entity, new GameIsOver());
        }
    }
}