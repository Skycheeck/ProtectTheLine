using Unity.Entities;

namespace ECS.Components
{
    public struct EnemySpawnPointComponent : IComponentData
    {
        public Entity EnemyPrefab;
    }
}