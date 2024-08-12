using Unity.Entities;

namespace ECS.Components
{
    public struct TakeDamage : IComponentData
    {
        public int Damage;
    }
}