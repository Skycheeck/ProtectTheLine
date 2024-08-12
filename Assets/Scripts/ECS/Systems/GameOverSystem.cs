using System.Linq;
using DefaultNamespace;
using ECS.Components;
using Unity.Entities;

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
            if (CheckForVictory(ref state)) return;
            CheckForDefeat(ref state);
        }

        private bool CheckForVictory(ref SystemState state)
        {
            if (SystemAPI.GetSingleton<EnemiesLeft>().Value > 0) return false;
            if (!SystemAPI.QueryBuilder().WithAll<Enemy>().Build().IsEmpty) return false;
            
            state.Enabled = false;
            ShowRestartScreen(ref state, "Victory");
            return true;

        }

        private void CheckForDefeat(ref SystemState state)
        {
            foreach (RefRO<Health> health in SystemAPI.Query<RefRO<Health>>().WithAll<Player>().WithChangeFilter<Health>())
            {
                if (!(health.ValueRO.Value <= 0)) continue;
                
                state.Enabled = false;
                ShowRestartScreen(ref state, "Defeat");
            }
        }

        private void ShowRestartScreen(ref SystemState state, string title)
        {
            RestartScreen restartScreen = RestartScreen.Instance;
            restartScreen.gameObject.SetActive(true);
            restartScreen.SetTitle(title);
        }
    }
}