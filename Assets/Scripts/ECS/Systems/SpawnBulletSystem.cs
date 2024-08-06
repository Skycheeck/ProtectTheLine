using ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using RaycastHit = Unity.Physics.RaycastHit;

namespace ECS.Systems
{
    public partial struct SpawnBulletSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRW<Gun> gun, RefRO<LocalToWorld> localToWorld, RefRO<CollisionLayer> collisionLayer) in 
                     SystemAPI.Query<RefRW<Gun>, RefRO<LocalToWorld>, RefRO<CollisionLayer>>())
            {
                gun.ValueRW.FireTimer -= SystemAPI.Time.DeltaTime;
                
                if (gun.ValueRW.FireTimer > 0) continue;
                gun.ValueRW.FireTimer = gun.ValueRO.TimeToFire;

                PhysicsWorldSingleton physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                RaycastInput raycastInput = new()
                {
                    Start = localToWorld.ValueRO.Position,
                    End = localToWorld.ValueRO.Position + new float3(0, 100, 0),
                    Filter = new CollisionFilter {BelongsTo = collisionLayer.ValueRO.BelongsTo, CollidesWith = collisionLayer.ValueRO.CollidesWith}
                };
                if (!physicsWorldSingleton.CastRay(raycastInput, out RaycastHit _)) continue;

                Entity bulletEntity = state.EntityManager.Instantiate(gun.ValueRO.BulletPrefab);
                SystemAPI.GetComponentRW<LocalTransform>(bulletEntity).ValueRW.Position = localToWorld.ValueRO.Position;
            }
        }
    }
}