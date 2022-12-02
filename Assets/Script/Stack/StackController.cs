using Script.Managers;
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
            canHold = StackManager.Instance.CanHold;
            if (!canHold) return;
            if (isHolding) return;
            if (!other.gameObject.CompareTag("Holder")) return;
            stackSpawner.DeleteStack(stackNumber);
            isHolding = true;
            stackCollider.enabled = false;
        } //Trigger for Stacks get in ship if ship can hold.
        
    }
}
