using System;
using UnityEngine;

namespace Legacy
{
    public class PlayerController : MonoBehaviour
    {
        public Action<float> Walking;

        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float rotateSpeed = 500f;

        private Rigidbody _rigidbody;
        private bool gettingInput;

        private Joystick joystick;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            joystick = FindObjectOfType<Joystick>(true);
        }

        private void Update()
        {
            Move();
            LookForward();
        }

        public void ChangeSpeed(float to) => 
            moveSpeed = to;

        private void LookForward()
        {
            Vector3 target = _rigidbody.velocity;

            if(target.sqrMagnitude > 0 && gettingInput)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target), rotateSpeed * Time.deltaTime);
        }

        private void Move()
        {
            float velX = joystick.value.x * moveSpeed;
            float velY = joystick.value.y * moveSpeed;

            if(joystick.value.sqrMagnitude > 0) 
            {
                gettingInput = true;
            }
            else 
            {
                gettingInput = false;
            }
        
            _rigidbody.velocity = Vector3.forward * velY + Vector3.right * velX + Vector3.up * _rigidbody.velocity.y;

            float walkingSpeed = _rigidbody.velocity.sqrMagnitude / (moveSpeed*moveSpeed);
            if(gettingInput && walkingSpeed < 0.2f)
                walkingSpeed = 0.2f;

            Walking?.Invoke(walkingSpeed);
        }
    }
}
