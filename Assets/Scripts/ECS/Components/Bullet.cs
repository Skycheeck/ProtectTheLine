using Unity.Entities;

namespace ECS.Components
{
    public struct Bullet : IComponentData
    {
        public int DamageOnHit;
    }
}