using System.Globalization;
using Script.Managers;
using Script.Player;
using TMPro;
using UnityEngine;

namespace Script.Stations
{
    public class DeliverStationController : MonoBehaviour
    {
        [SerializeField] private HoldController holdController;
        [SerializeField] private float timer;
        [SerializeField] private bool staying;
        [SerializeField] private TextMeshProUGUI timerText;

        private void Awake()
        {
            holdController = FindObjectOfType<HoldController>();
            timerText.gameObject.SetActive(false);
        }

        public void Deliver()
        {
            holdController.EmptyHolder();
            Staying = false;
            TimerActive = false;
            timer = 3f;
        }

        private bool TimerActive
        {
            set => timerText.gameObject.SetActive(value);
        }

        private bool Staying
        {
            get => staying;
            set => staying = value;
        }


        private void OnTriggerEnter(Collider other)
        {
            var stackCount = StackManager.Instance.stackController.Count;
            if (stackCount == 0) return;
            Debug.Log(other.tag + "Enter");
            if (!other.CompareTag("Holder")) return;
            Staying = true;
            timer = 3f;
            TimerActive = true;
        }

        private void OnTriggerStay(Collider other)
        {
            var stackCount = StackManager.Instance.stackController.Count;
            if (stackCount == 0) return;
            if (!other.CompareTag("Holder")) return;
            if (timer > 0) return;

            Deliver();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!staying) return;
            if (!other.CompareTag("Holder")) return;
            Staying = false;
            TimerActive = false;
        }

        private void Update()
        {
            if (Staying)
            {
                timer -= 1 * Time.deltaTime;
                timerText.text = Mathf.RoundToInt(timer).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}