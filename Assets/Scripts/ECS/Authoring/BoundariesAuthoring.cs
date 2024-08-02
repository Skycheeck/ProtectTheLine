using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class BoundariesAuthoring : MonoBehaviour
    {
        public class BoundariesBaker : Baker<BoundariesAuthoring>
        {
            public override void Bake(BoundariesAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                Vector3 position = authoring.transform.position;
                Vector3 localScale = authoring.transform.localScale;
                AddComponent(entity, new Boundaries()
                {
                    MinX = position.x - localScale.x / 2,
                    MaxX = position.x + localScale.x / 2,
                    MinY = position.y - localScale.y / 2,
                    MaxY = position.y + localScale.y / 2,
                });
            }
        }
    }
}