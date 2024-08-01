using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS.Authoring
{
    public class MovementDirectionAuthoring : MonoBehaviour
    {
        public float2 Direction;

        public class MovementDirectionBaker : Baker<MovementDirectionAuthoring>
        {
            public override void Bake(MovementDirectionAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MovementDirection {Direction = authoring.Direction});
            }
        }
    }
}