using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Player.Attack
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private GameObject cannonBullet;
        private float cannonBulletPower;
        private float rotationSpeed;
        private Vector3 playerVelocity;
        private Quaternion zeroRotation;
        private InputActionAsset asset;
        private Coroutine activeCoroutineReset;
        private Coroutine activeCoroutineAttack;

        private void Start()
        {
            rotationSpeed = 10f;
            cannonBulletPower = 30f;
            playerInput = GetComponent<PlayerInput>();
            zeroRotation = Quaternion.Euler(0f, 0f, 0f);
            
        }


        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (activeCoroutineReset != null)
            {
                StopCoroutine(ResetButton());
                activeCoroutineReset = null;
            }
            var input = playerInput.actions["Rotation"].ReadValue<Vector2>();
            if (input == Vector2.zero) return;

            var targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * rotationSpeed);
        } //Cannon Rotation with Input System Joystick.

        public void Attack()
        {
            activeCoroutineAttack ??= StartCoroutine(AttackButton());
        }

        private IEnumerator AttackButton()
        {
            var bullet = Instantiate(cannonBullet,gameObject.transform);
            var bulletRig = bullet.GetComponent<Rigidbody>();
            
            bulletRig.AddForce(bullet.transform.forward*cannonBulletPower,ForceMode.Impulse);
            
            yield return new WaitForSeconds(2f);
            //Destroy(bullet);
            activeCoroutineAttack = null;
        }//Create a clone of the bullet and add force on the bullet.

        public void Reset()
        {
            activeCoroutineReset ??= StartCoroutine(ResetButton());
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
            activeCoroutineReset = null;
        } //Coroutine for reset cannon rotation with Slerp
    }
}