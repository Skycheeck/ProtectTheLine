using ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace ECS.Systems
{
    public partial struct SpawnBulletSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRW<Gun> gun, RefRO<LocalToWorld> localToWorld) in SystemAPI.Query<RefRW<Gun>, RefRO<LocalToWorld>>())
            {
                gun.ValueRW.FireTimer -= SystemAPI.Time.DeltaTime;
                
                if (gun.ValueRW.FireTimer > 0) continue;
                gun.ValueRW.FireTimer = gun.ValueRO.TimeToFire;
                
                Entity bulletEntity = state.EntityManager.Instantiate(gun.ValueRO.BulletPrefab);
                SystemAPI.GetComponentRW<LocalTransform>(bulletEntity).ValueRW.Position = localToWorld.ValueRO.Position;
            }
        }
    }
}