using DefaultNamespace;
using ECS.Components;
using Unity.Entities;

namespace ECS.Systems
{
    public partial struct GameOverSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (RefRO<Health> health in
                     SystemAPI.Query<RefRO<Health>>().WithAll<Player>().WithChangeFilter<Health>())
            {
                if (health.ValueRO.Value <= 0)
                {
                    ShowRestartScreen(ref state, "Defeat");
                }
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