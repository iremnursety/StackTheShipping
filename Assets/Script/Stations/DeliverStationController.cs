using System.Globalization;
using Script.Managers;
using TMPro;
using UnityEngine;

namespace Script.Stations
{
    public class DeliverStationController : MonoBehaviour
    {
        [SerializeField] private float timer;
        [SerializeField] private bool staying;
        [SerializeField] private TextMeshProUGUI timerText;

        private void Awake()
        {
            timerText.gameObject.SetActive(false);
        }

        public void Deliver()
        {
            StackManager.Instance.Deliver();
            Staying = false;
            TimerActive = false;
            timer = 3f;
        } //Deliver Stacks and set staying and timer back.

        private bool TimerActive
        {
            set => timerText.gameObject.SetActive(value);
        } //Canvas Text Timer Set

        private bool Staying
        {
            get => staying;
            set => staying = value;
        } //Staying get and set.


        private void OnTriggerEnter(Collider other)
        {
            var stackCount = StackManager.Instance.stackController.Count;
            if (stackCount == 0) return;
            Debug.Log(other.tag + "Enter");
            if (!other.CompareTag("Holder")) return;
            Staying = true;
            timer = 3f;
            TimerActive = true;
        } //OnTriggerEnter for player Deliver Stacks.

        private void OnTriggerStay(Collider other)
        {
            var stackCount = StackManager.Instance.stackController.Count;
            if (stackCount == 0) return;
            if (!other.CompareTag("Holder")) return;
            if (timer > 0) return;

            Deliver();
        } //OnTriggerStay for player Deliver Stacks.

        private void OnTriggerExit(Collider other)
        {
            if (!staying) return;
            if (!other.CompareTag("Holder")) return;
            Staying = false;
            TimerActive = false;
        } //OnTriggerExit for player Deliver Stacks.

        private void Update()
        {
            if (!Staying)
                return;
            Timer();
        }

        private void Timer()
        {
            timer -= 1 * Time.deltaTime;
            timerText.text = Mathf.RoundToInt(timer).ToString(CultureInfo.InvariantCulture);
        }
    }
}