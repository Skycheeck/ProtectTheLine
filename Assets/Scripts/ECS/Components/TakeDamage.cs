using Unity.Entities;

namespace ECS.Components
{
    public struct TakeDamage : IComponentData
    {
        public float Damage;
    }
}