using ECS.Components;
using Unity.Entities;

namespace ECS.Systems
{
    [UpdateBefore(typeof(MovementSystem))]
    public partial struct ShipMovementDirectionSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Input>();
        }

        public void OnUpdate(ref SystemState state)
        {
            Input input = SystemAPI.GetSingleton<Input>();

            foreach (RefRW<MovementDirection> movementDirection in SystemAPI.Query<RefRW<MovementDirection>>().WithAll<Ship>())
            {
                movementDirection.ValueRW.Direction = input.Movement;
            }
        }
    }
}