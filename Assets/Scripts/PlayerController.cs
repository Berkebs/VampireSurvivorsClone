using UnityEngine;
using UnityEngine.InputSystem;

namespace VampireSurvivorsClone
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;
        private PlayerInput playerInput;
        private InputAction moveAction;
        private Vector2 moveInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput>();
            
            // PlayerInput component should be set to use "InputSystem_Actions" asset
            // and the action map "Player" should be active.
        }

        private void OnEnable()
        {
            // Find the Move action. 
            // Note: Ensure the Action Name matches what's in the Input Actions Asset (Case sensitive)
            moveAction = playerInput.actions["Move"];
        }

        private void Update()
        {
            if (moveAction != null)
            {
                moveInput = moveAction.ReadValue<Vector2>();
            }
        }

        private void FixedUpdate()
        {
            // Apply movement physics
            // Using velocity allows for smooth movement and proper collision handling
            rb.linearVelocity = moveInput * moveSpeed;

            // Optional: Flip sprite based on direction
            if (moveInput.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
            }
        }
    }
}
