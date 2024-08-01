using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    public struct MovementDirection : IComponentData
    {
        public float2 Direction;
    }
}