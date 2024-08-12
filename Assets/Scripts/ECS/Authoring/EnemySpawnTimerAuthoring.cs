using ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class EnemySpawnTimerAuthoring : MonoBehaviour
    {
        public float Min;
        public float Max;

        public class EnemySpawnTimerBaker : Baker<EnemySpawnTimerAuthoring>
        {
            public override void Bake(EnemySpawnTimerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new EnemySpawnTimer
                    {
                        Min = authoring.Min, Max = authoring.Max, TimeLeft = Random.Range(authoring.Min, authoring.Max)
                    });
            }
        }
    }
}