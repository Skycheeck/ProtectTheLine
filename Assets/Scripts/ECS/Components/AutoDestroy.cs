using Unity.Entities;

namespace ECS.Components
{
    public struct AutoDestroy : IComponentData
    {
        public float TimeToDestroy;
    }
}