using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS.Authoring
{
    public class SpeedAuthoring : MonoBehaviour
    {
        public float2 Speed;

        public class SpeedBaker : Baker<SpeedAuthoring>
        {
            public override void Bake(SpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Speed {Value = authoring.Speed});
            }
        }
    }
}