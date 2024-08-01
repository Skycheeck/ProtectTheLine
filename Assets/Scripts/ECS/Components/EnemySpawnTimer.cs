using Unity.Entities;

namespace ECS.Components
{
    public struct EnemySpawnTimer : IComponentData
    {
        public float Time;
    }
}