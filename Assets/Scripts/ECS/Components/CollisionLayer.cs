using Unity.Entities;

namespace ECS.Components
{
    public struct CollisionLayer : IComponentData
    {
        public uint BelongsTo;
        public uint CollidesWith;
    }
}