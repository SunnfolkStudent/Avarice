using UnityEngine;

namespace Scripts.Player
{
    public class GetInput : MonoBehaviour
    {
       private InputSystem_Actions _inputSystem;
       private void Awake() => _inputSystem = new InputSystem_Actions();
       private void OnEnable() => _inputSystem.Enable();
       private void OnDisable() => _inputSystem.Disable();

       public Vector2 MoveDirection { get; private set; }
       public bool SprintPressed { get; private set; }
       public bool DashAttack { get; private set; }
       public bool FireBallAttack { get; private set; }
       
       private void Update()
       {
           MoveDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
           SprintPressed = _inputSystem.Player.Sprint.triggered;
           DashAttack = _inputSystem.Player.Dash.triggered;
           FireBallAttack = _inputSystem.Player.Fireball.triggered;
       }
    }
}