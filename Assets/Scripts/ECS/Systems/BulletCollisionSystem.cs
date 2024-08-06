using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace ECS.Systems
{
    public partial struct BulletCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            foreach ((RefRO<Bullet> bullet, RefRO<LocalToWorld> localToWorld, RefRO<CollisionLayer> collisionLayer, Entity bulletEntity) in 
                     SystemAPI.Query<RefRO<Bullet>, RefRO<LocalToWorld>, RefRO<CollisionLayer>>().WithEntityAccess())
            {
                PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                RaycastInput raycastInput = new()
                {
                    Start = localToWorld.ValueRO.Position,
                    End = localToWorld.ValueRO.Position + new float3(0, .1f, 0),
                    Filter = new CollisionFilter {BelongsTo = collisionLayer.ValueRO.BelongsTo, CollidesWith = collisionLayer.ValueRO.CollidesWith}
                };
                if (!physicsWorldSingleton.CastRay(raycastInput, out RaycastHit raycastHit)) continue;
                
                entityCommandBuffer.AddComponent(raycastHit.Entity, new TakeDamage { Damage = bullet.ValueRO.DamageOnHit });
                entityCommandBuffer.DestroyEntity(bulletEntity);
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}