using System.Collections;
using System.Collections.Generic;
using Script.Managers;
using Script.Stack;
using UnityEngine;

namespace Script.Player
{
    public class HoldController : MonoBehaviour
    {
        [SerializeField] private List<StackController> stackController = new List<StackController>();
        [SerializeField] private GameObject playerCollider;
        [SerializeField] private int maxHold;
        
        private Coroutine _activeCoroutine;
        private int _holdValue;
    

        private void Start()
        {
            //TODO: Save and reload and game start.
            CanHoldValue = maxHold;
           StackManager.Instance.CanHold = true;
        }

        public void HoldStack(StackController stack)
        {
            if (_holdValue == 0) return;
            var stackTransform = stack.transform;

            stackTransform.parent = gameObject.transform;
            stackTransform.localScale = new Vector3(1, 0.5f, 0.5f);
            stackTransform.localPosition = Vector3.zero + Vector3.up * stackController.Count / 2;
            stackTransform.localRotation = new Quaternion(0, 0, 0, 0);

            stackController.Add(stack);
            stack.name = "Holding" + stackController.Count;
            CanHoldValue -= 1;
        } //Add stack on holding list and change scale and positions for set on ship.

        public void EmptyHolder()
        {
            _activeCoroutine ??= StartCoroutine(HoldDestroyer());
            Debug.Log(_activeCoroutine);
        } //Starting Coroutine for Empty Holder Destroying holding objects and clear hold list.

        private IEnumerator HoldDestroyer()
        {
            var stackCount = stackController.Count;
            if (stackCount <= 0) yield break;
            for (var i = stackCount - 1; i > -1; i--)
            {
                var tempStack = stackController[i];
                MoneyManager.Instance.Money = tempStack.stackMoney * stackCount;
                Destroy(tempStack.gameObject);
                yield return new WaitForSeconds(0.2f);
            }

            stackController.Clear();
            playerCollider.SetActive(true);

            CanHoldValue = maxHold;
            StackManager.Instance.CanHold = true;

            _activeCoroutine = null;
        } //Coroutine for destroy stacks and adding money to Money Manager.

        private int CanHoldValue
        {
            get => _holdValue;
            set
            {
                _holdValue = value;
                if (_holdValue == 0)
                    StackManager.Instance.CanHold = false;
            }
        } //Get set for holding value on ship.

        
    }
}