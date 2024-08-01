using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    public struct Input : IComponentData
    {
        public float2 Movement;
    }
}