using Unity.Entities;
using Unity.Mathematics;

namespace ECS.Components
{
    public struct Speed : IComponentData
    {
        public float2 Value;
    }
}