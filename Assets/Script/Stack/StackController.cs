using Script.Enemy;
using Script.Managers;
using Script.Player;
using UnityEngine;

namespace Script.Stack
{
    public class StackController : MonoBehaviour
    {
        [SerializeField] private Collider stackCollider;
        public StackSpawner stackSpawner;
        public int stackNumber;
        public bool isHolding,canHold;
        public int stackMoney;
        private void Start()
        {
            stackCollider = GetComponent<BoxCollider>();
            isHolding = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Holder")) return;
            if (isHolding) return;
            
            var holdCont = other.GetComponent<HoldController>();
            canHold = holdCont.canHold;
            if (!canHold) return;
            Debug.Log("After canHold");
            //if (!other.gameObject.CompareTag("Holder")) return;
            stackSpawner.DeleteStack(stackNumber,holdCont);
            Debug.Log("After stack spawner delete");
            isHolding = true;
            stackCollider.enabled = false;
            gameObject.layer = 0;
            // else if (other.transform.parent.CompareTag("Enemy"))
            // {
            //     canHold = other.GetComponent<EnemyHoldController>().canHold;
            //     if (!canHold) return;
            //     if (isHolding) return;
            //     if (!other.gameObject.CompareTag("Holder")) return;
            //     stackSpawner.DeleteStack(stackNumber,other.gameObject);
            //     isHolding = true;
            //     stackCollider.enabled = false;
            //     
            // }
        } //Trigger for Stacks get in ship if ship can hold.
        
    }
}
