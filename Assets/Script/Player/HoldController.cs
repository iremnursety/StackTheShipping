using System.Collections;
using System.Collections.Generic;
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
        public List<StackController> stackControllers = new List<StackController>();
        private Coroutine activeCoroutine;
        private int holdValue;
        public bool canHold;
    

        private void Start()
        {
            //TODO: Save and reload and game start.
            CanHoldValue = maxHold;
            canHold= true;
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

            StackControllerAddList(stack);
            //AddList=stack;
        } //Add stack on holding list and change scale and positions for set on ship.
        // private StackController AddList
        // {
        //     set => stackControllers.Add(value);
        // }
        private void StackControllerAddList(StackController stacks)
        {
            stackControllers.Add(stacks);
        }

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
                var tempStack = stackControllers[i];
                MoneyManager.Instance.Money = tempStack.stackMoney * stackCount;
                Destroy(tempStack.gameObject);
                yield return new WaitForSeconds(0.2f);
            }

            stackControllers.Clear();
            playerCollider.SetActive(true);

            CanHoldValue = maxHold;
            holdingStack = 0;
            canHold= true;

            activeCoroutine = null;
        } //Coroutine for destroy stacks and adding money to Money Manager.

        private int CanHoldValue
        {
            get => holdValue;
            set
            {
                holdValue = value;
                if (holdValue == 0)
                    canHold = false;
            }
        } //Get set for holding value on ship.

        
    }
}