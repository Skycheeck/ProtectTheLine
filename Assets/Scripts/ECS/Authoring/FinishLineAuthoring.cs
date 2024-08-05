using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class FinishLineAuthoring : MonoBehaviour
    {
        public class FinishLineBaker : Baker<FinishLineAuthoring>
        {
            public override void Bake(FinishLineAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new FinishLine {YPosition = authoring.transform.position.y});
            }
        }
    }
}