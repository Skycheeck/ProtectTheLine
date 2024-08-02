using Unity.Entities;

namespace ECS.Components
{
    public struct Boundaries : IComponentData
    {
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;
    }
}