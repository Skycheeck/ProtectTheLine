using Unity.Entities;

namespace ECS.Components
{
    public struct EnemySpawnTimer : IComponentData
    {
        public float Min, Max;
        public float TimeLeft;
    }
}