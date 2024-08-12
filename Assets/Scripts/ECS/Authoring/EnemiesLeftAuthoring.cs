using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace  ECS.Authoring
{
    public class EnemiesLeftAuthoring : MonoBehaviour
    {
        public int EnemiesLeft;

        public class EnemiesLeftBaker : Baker<EnemiesLeftAuthoring>
        {
            public override void Bake(EnemiesLeftAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemiesLeft {Value = authoring.EnemiesLeft});
            }
        }
    }
}