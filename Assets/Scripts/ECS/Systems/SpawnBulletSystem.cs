using ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

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

                NativeList<DistanceHit> distanceHits = new(Allocator.Temp);
                CollisionFilter collisionFilter = new() {BelongsTo = collisionLayer.ValueRO.BelongsTo, CollidesWith = collisionLayer.ValueRO.CollidesWith};

                if (!physicsWorldSingleton.OverlapSphere(localToWorld.ValueRO.Position, gun.ValueRO.FireRadius, ref distanceHits, collisionFilter)) continue;

                float3 direction3 = math.normalize(distanceHits[0].Position - localToWorld.ValueRO.Position);
                Entity bulletEntity = state.EntityManager.Instantiate(gun.ValueRO.BulletPrefab);
                SystemAPI.GetComponentRW<LocalTransform>(bulletEntity).ValueRW.Position = localToWorld.ValueRO.Position;
                SystemAPI.SetComponent(bulletEntity, new MovementDirection() { Direction = new float2 { x = direction3.x, y = direction3.y }});
            }
        }
    }
}