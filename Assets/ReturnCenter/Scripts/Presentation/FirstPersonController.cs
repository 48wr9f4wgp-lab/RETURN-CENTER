using UnityEngine;

namespace ReturnCenter.Presentation
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4.5f;
        [SerializeField] private float lookSensitivity = 2.0f;
        [SerializeField] private float gravity = -20f;

        private CharacterController _controller;
        private Camera _camera;
        private float _pitch;
        private float _verticalVelocity;

        public Camera ViewCamera => _camera;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _camera = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            var yaw = Input.GetAxisRaw("Mouse X") * lookSensitivity;
            var pitchDelta = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
            transform.Rotate(0f, yaw, 0f);
            _pitch = Mathf.Clamp(_pitch - pitchDelta, -80f, 80f);
            _camera.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);

            var movement = (transform.right * Input.GetAxisRaw("Horizontal") +
                            transform.forward * Input.GetAxisRaw("Vertical"));
            movement = Vector3.ClampMagnitude(movement, 1f) * moveSpeed;

            if (_controller.isGrounded && _verticalVelocity < 0f)
                _verticalVelocity = -2f;
            _verticalVelocity += gravity * Time.deltaTime;
            movement.y = _verticalVelocity;

            _controller.Move(movement * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
