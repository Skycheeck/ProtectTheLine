using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Systems
{
    [UpdateAfter(typeof(MovementSystem))]
    public partial struct FinishLineCrossingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Player>();
            state.RequireForUpdate<FinishLine>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            FinishLine finishLine = SystemAPI.GetSingleton<FinishLine>();
            EntityCommandBuffer commandBuffer = new(Allocator.Temp);

            foreach ((LocalToWorld localToWorld, Entity entity) in SystemAPI.Query<LocalToWorld>().WithAll<Enemy>().WithEntityAccess())
            {
                if (localToWorld.Position.y > finishLine.YPosition) continue;
                commandBuffer.DestroyEntity(entity);
                DamagePlayer(ref state);
            }
            
            commandBuffer.Playback(state.EntityManager);
            commandBuffer.Dispose();
        }

        [BurstCompile]
        private void DamagePlayer(ref SystemState state)
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            RefRW<Health> health = SystemAPI.GetComponentRW<Health>(playerEntity);
            health.ValueRW.Value -= 1;
        }
    }
}