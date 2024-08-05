using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class AutoDestroyAuthoring : MonoBehaviour
    {
        public float TimeToDestroy;

        public class AutoDestroyBaker : Baker<AutoDestroyAuthoring>
        {
            public override void Bake(AutoDestroyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new AutoDestroy {TimeToDestroy = authoring.TimeToDestroy});
            }
        }
    }
}