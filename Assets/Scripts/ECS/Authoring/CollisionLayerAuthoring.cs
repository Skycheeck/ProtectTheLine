using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class CollisionLayerAuthoring : MonoBehaviour
    {
        public LayerMask CollidesWith;
        public class CollisionLayerBaker : Baker<CollisionLayerAuthoring>
        {
            public override void Bake(CollisionLayerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new CollisionLayer()
                {
                    BelongsTo = 1U << authoring.gameObject.layer,
                    CollidesWith = (uint) authoring.CollidesWith.value
                });
            }
        }
    }
}