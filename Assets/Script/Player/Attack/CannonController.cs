using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player.Attack
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GameObject cannonBullet;
        private float rotationSpeed;
        private Vector3 playerVelocity;
        private Quaternion zeroRotation;
        private InputActionAsset asset;
        private Coroutine activeCoroutine;

        private void Start()
        {
            rotationSpeed = 10f;
            playerInput = GetComponent<PlayerInput>();
            zeroRotation = Quaternion.Euler(0f, 0f, 0f);
        }


        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(ResetButton());
                activeCoroutine = null;
            }
            var input = playerInput.actions["Rotation"].ReadValue<Vector2>();
            if (input == Vector2.zero) return;

            var targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        } //Cannon Rotation with Input System Joystick.

        public void Attack()
        {
           
        }

        public void Reset()
        {
            activeCoroutine ??= StartCoroutine(ResetButton());
        } //Cannon Rotation Reset Button Interaction


        private IEnumerator ResetButton()
        {
            while (Quaternion.Angle(transform.localRotation, zeroRotation) > 0f)
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, zeroRotation,
                    rotationSpeed * Time.fixedDeltaTime);
                yield return null;

                if (Quaternion.Angle(transform.rotation, zeroRotation) != 0f) continue;
                transform.localRotation = zeroRotation;
                break;
            }
            yield return new WaitForSeconds(0.5f);
            activeCoroutine = null;
        } //Coroutine for reset cannon rotation with Slerp
    }
}