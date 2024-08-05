using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS.Systems
{
    public partial struct AutoDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            foreach ((RefRW<AutoDestroy> autoDestroy, Entity entity) in SystemAPI.Query<RefRW<AutoDestroy>>().WithEntityAccess())
            {
                autoDestroy.ValueRW.TimeToDestroy -= SystemAPI.Time.DeltaTime;
                if (autoDestroy.ValueRO.TimeToDestroy > 0) continue;
                entityCommandBuffer.DestroyEntity(entity);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}