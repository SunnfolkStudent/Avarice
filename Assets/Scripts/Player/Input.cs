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
       public bool Attack1 { get; private set; }
       public bool Attack2 { get; private set; }
       public bool Attack3 { get; private set; }
       
       private void Update()
       {
           MoveDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
           SprintPressed = _inputSystem.Player.Sprint.triggered;
           Attack1 = _inputSystem.Player.Attack1.triggered;
           Attack2 = _inputSystem.Player.Attack2.triggered;
           Attack3 = _inputSystem.Player.Attack3.triggered;
       }
    }
}