using Script.Managers;
using TMPro;
using UnityEngine;

namespace Script.Stations
{
    public class UpgradeStationController : MonoBehaviour
    {
        [SerializeField] private Canvas upgradeCanvas;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private int level;
        [SerializeField] private int price;
        [SerializeField] private bool staying;

        private void Awake()
        {
            upgradeCanvas.gameObject.SetActive(false);

            ChangePrice();
            levelText.text="Level: "+level;
        }

        private void OnTriggerEnter(Collider other)
        {
            staying = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!staying)return;
            var isActiveCanvas = upgradeCanvas.isActiveAndEnabled;

            if (isActiveCanvas) return;
            upgradeCanvas.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!staying) return;
            staying = false;
            upgradeCanvas.gameObject.SetActive(false);
        }

        public void Upgrade()
        {
            var money = MoneyManager.Instance.Money;
            if (money < price) return;

            MoneyManager.Instance.Money = -price;
            LevelUp = 1;
            levelText.text="Level: "+level;
            
            if (level % 5 == 0)
                UpgradeShip();

            ChangePrice();
        }

        private void ChangePrice()
        {
            price = level * 100;
            priceText.text = "Price: " + price;
        }

        private int LevelUp
        {
            set
            {
                level+=value;
                levelText.text="Level: "+level;
            }
        }

        private void UpgradeShip()
        {
            Debug.Log("Ship Upgraded.");
        }
        
    }
}
