using System.Collections;
using Script.Managers;
using Script.Stack;
using UnityEngine;

namespace Script.Player
{
    public class HoldController : MonoBehaviour
    {
        [SerializeField] private GameObject playerCollider;
        [SerializeField] private int maxHold;
        [SerializeField] private int holdingStack;
        
        private Coroutine activeCoroutine;
        private int holdValue;
    

        private void Start()
        {
            //TODO: Save and reload and game start.
            CanHoldValue = maxHold;
           StackManager.Instance.CanHold = true;
        }

        public void HoldStack(StackController stack)
        {
            if (holdValue == 0) return;
            var stackTransform = stack.transform;

            stackTransform.parent = gameObject.transform;
            stackTransform.localScale = new Vector3(1, 0.5f, 0.5f);
            stackTransform.localPosition = Vector3.zero + Vector3.up * holdingStack / 2;
            stackTransform.localRotation = new Quaternion(0, 0, 0, 0);

            //stackController.Add(stack);
            stack.name = "Holding" + holdingStack;
            CanHoldValue -= 1;
            holdingStack += 1;
            
            StackManager.Instance.AddList=stack;
        } //Add stack on holding list and change scale and positions for set on ship.

        public void EmptyHolder()
        {
            activeCoroutine ??= StartCoroutine(HoldDestroyer());
            Debug.Log(activeCoroutine);
        } //Starting Coroutine for Empty Holder Destroying holding objects and clear hold list.

        private IEnumerator HoldDestroyer()
        {
            var stackCount = holdingStack;
            if (stackCount <= 0) yield break;
            for (var i = stackCount - 1; i > -1; i--)
            {
                var tempStack = StackManager.Instance.stackController[i];
                MoneyManager.Instance.Money = tempStack.stackMoney * stackCount;
                Destroy(tempStack.gameObject);
                yield return new WaitForSeconds(0.2f);
            }

            StackManager.Instance.stackController.Clear();
            playerCollider.SetActive(true);

            CanHoldValue = maxHold;
            holdingStack = 0;
            StackManager.Instance.CanHold = true;

            activeCoroutine = null;
        } //Coroutine for destroy stacks and adding money to Money Manager.

        private int CanHoldValue
        {
            get => holdValue;
            set
            {
                holdValue = value;
                if (holdValue == 0)
                    StackManager.Instance.CanHold = false;
            }
        } //Get set for holding value on ship.

        
    }
}