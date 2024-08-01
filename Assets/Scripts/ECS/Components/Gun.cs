using Unity.Entities;

namespace ECS.Components
{
    public struct Gun : IComponentData
    {
        public Entity BulletPrefab;
        public float TimeToFire;
        public float FireTimer;
    }
}