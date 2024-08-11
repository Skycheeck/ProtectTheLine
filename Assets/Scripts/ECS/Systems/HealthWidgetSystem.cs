using DefaultNamespace;
using ECS.Components;
using Unity.Entities;

namespace ECS.Systems
{
    public partial struct HealthWidgetSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (RefRO<Health> health in SystemAPI.Query<RefRO<Health>>().WithAll<Player>().WithChangeFilter<Health>())
            {
                HealthWidget.Instance.UpdateHealth(health.ValueRO.Value);
            }
        }
    }
}