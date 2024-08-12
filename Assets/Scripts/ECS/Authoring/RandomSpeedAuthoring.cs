using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ECS.Authoring
{
    public class RandomSpeedAuthoring : MonoBehaviour
    {
        public float2 Min;
        public float2 Max;

        public class RandomSpeedBaker : Baker<RandomSpeedAuthoring>
        {
            public override void Bake(RandomSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Speed {Value = new float2
                {
                    x = Random.Range(authoring.Min.x, authoring.Max.x),
                    y = Random.Range(authoring.Min.y, authoring.Max.y)
                }});
            }
        }
    }
}