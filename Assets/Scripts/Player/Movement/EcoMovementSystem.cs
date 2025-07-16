using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECO.Player
{
    public class EcoMovementSystem : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float floatSpeed = 5f;
        public float glideSpeed = 3f;
        public float jumpForce = 8f;
        public float gravity = 9.81f;
        public float maxFallSpeed = 10f;

        private Rigidbody rb;
        private bool isGrounded;
        private int jumpCount;
        private const int maxJumps = 2;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = false;
        }

        public void HandleMovement(Vector2 input)
        {
            Vector3 movement = new Vector3(input.x, 0, input.y) * floatSpeed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }

        public void HandleGlide(bool isGliding)
        {
            if (isGliding && rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, -glideSpeed, rb.velocity.z);
            }
        }

        public void HandleJump()
        {
            if (jumpCount < maxJumps)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                jumpCount++;
            }
        }

        void FixedUpdate()
        {
            ApplyGravity();
            CheckGrounded();
        }

        private void ApplyGravity()
        {
            if (!isGrounded)
            {
                rb.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed), rb.velocity.z);
            }
        }

        private void CheckGrounded()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
            if (isGrounded)
            {
                jumpCount = 0;
            }
        }

        public void UpgradeSpeed(float multiplier)
        {
            floatSpeed *= multiplier;
        }
    }
}