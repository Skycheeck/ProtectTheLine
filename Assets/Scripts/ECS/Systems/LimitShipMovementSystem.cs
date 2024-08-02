using ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    [UpdateAfter(typeof(MovementSystem))]
    public partial struct LimitShipMovementSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Boundaries>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Boundaries boundaries = SystemAPI.GetSingleton<Boundaries>();

            foreach (RefRW<LocalTransform> localTransform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Ship>())
            {
                localTransform.ValueRW.Position = new float3
                {
                    x = math.clamp(localTransform.ValueRO.Position.x, boundaries.MinX, boundaries.MaxX),
                    y = math.clamp(localTransform.ValueRO.Position.y, boundaries.MinY, boundaries.MaxY),
                    z = localTransform.ValueRO.Position.z
                };
            }
        }
    }
}