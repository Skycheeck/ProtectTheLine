using ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS.Systems
{
    public partial struct MovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRW<LocalTransform> localTransform, RefRO<Speed> speed, RefRO<MovementDirection> movementDirection) 
                     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Speed>, RefRO<MovementDirection>>())
            {
                localTransform.ValueRW.Position += new float3(speed.ValueRO.Value * movementDirection.ValueRO.Direction * SystemAPI.Time.DeltaTime, 0f);
            }
        }
    }
}