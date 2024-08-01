using Unity.Entities;
using UnityEngine;
using Input = ECS.Components.Input;

namespace ECS.Systems
{
    public partial class InputSystem : SystemBase
    {
        private PlayerControls _playerControls;

        protected override void OnCreate()
        {
            if (!SystemAPI.TryGetSingleton(out Input input))
            {
                EntityManager.CreateSingleton<Input>();
            }

            _playerControls = new PlayerControls();
            _playerControls.Enable();
        }

        protected override void OnUpdate()
        {
            SystemAPI.SetSingleton(new Input {Movement = _playerControls.Player.Move.ReadValue<Vector2>()});
        }
    }
}