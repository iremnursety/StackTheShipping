using UnityEngine;

namespace Script.Player.Attack
{
    public class CannonBulletController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log(other.gameObject.name);
                Destroy(gameObject);
            } 
            Debug.Log(other.name);
            Destroy(gameObject);
        }
    }
}