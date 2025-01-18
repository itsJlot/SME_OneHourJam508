using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private void Start()
        {
            Walk = input.actions["Walk"];
            Jump = input.actions["Jump"];
            Look = input.actions["Look"];
            Run = input.actions["Run"];

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (!grounded) rig.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        
            if (Walk.IsPressed())
            {
                var walkValue = Walk.ReadValue<Vector2>();
                var walkDirection = transform.TransformDirection(new Vector3(walkValue.x, 0f, walkValue.y).normalized);
                if (Run.IsPressed() && walkValue.y > -0.1f) walkDirection *= runMultiplier;
                
                rig.AddForce(new Vector3(walkDirection.x, 0f, walkDirection.z) * walkSpeed);
            }

            if (grounded && !jumping && Jump.IsPressed())
            {
                jumping = true;
                grounded = false;

                rig.velocity *= 0f;
                
                StartCoroutine(DoJump());
            }

            Rotation += Look.ReadValue<Vector2>() * (lookSpeed * Time.deltaTime);

            transform.localEulerAngles = new Vector3(0f, Rotation.x, 0f);

            if (Rotation.y > camLimits.max) Rotation.y = camLimits.max;
            else if (Rotation.y < camLimits.min) Rotation.y = camLimits.min;
            cam.transform.localEulerAngles = new Vector3(-Rotation.y, 0f, 0f);
            
            rig.velocity = new Vector3(rig.velocity.x * 0.25f, rig.velocity.y, rig.velocity.z * 0.25f);
        }

        private IEnumerator DoJump()
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Force);
            
            var t = 0f;
            while (t < jumpTime && Jump.IsPressed() && !grounded)
            {
                t += Time.deltaTime;
                yield return null;
            }

            if (rig.velocity.y > 0f) rig.velocity += Vector3.down * (rig.velocity.y * 0.5f);
            
            yield return new WaitUntil(() => kindaGrounded || grounded);
            jumping = false;
        }
        
        private InputAction Walk;
        private InputAction Jump;
        private InputAction Look;
        private InputAction Run;

        private Vector2 Rotation;

        public Rigidbody rig;
    
        [HideInInspector]
        public bool jumping;
        
        public PlayerInput input;
        public Camera cam;

        [Header("Movement")]

        public float walkSpeed = 1f;
        
        public float jumpForce = 1f;
        public float jumpTime = 1f;
        public float gravity = 1f;

        [Header("Sprint")] 
        
        public float runMultiplier = 2f;

        [Header("Camera")]
        
        public float lookSpeed = 1f;
        public AxisLimit camLimits = new();

        [Header("Grounded")] 
        
        public bool grounded;
        public bool kindaGrounded;

        [Serializable]
        public class AxisLimit
        {
            [SerializeField] 
            public float min;
            [SerializeField] 
            public float max;
        }
    }
}