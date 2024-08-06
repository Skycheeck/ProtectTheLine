using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace ECS.Systems
{
    public partial struct EnemyTakeDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new(Allocator.Temp);
            
            foreach ((RefRO<TakeDamage> takeDamage, RefRW<Health> health, Entity entity) in 
                     SystemAPI.Query<RefRO<TakeDamage>, RefRW<Health>>().WithAll<Enemy>().WithEntityAccess())
            {
                health.ValueRW.Value -= takeDamage.ValueRO.Damage;
                
                if (health.ValueRO.Value > 0)
                {
                    entityCommandBuffer.RemoveComponent<TakeDamage>(entity);
                    continue;
                }
                
                entityCommandBuffer.DestroyEntity(entity);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}