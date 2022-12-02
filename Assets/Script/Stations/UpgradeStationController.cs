using Script.Managers;
using Script.Player;
using TMPro;
using UnityEngine;

namespace Script.Stations
{
    public class UpgradeStationController : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private Vector3 stationVector;
        [SerializeField] private HoldController holdController;
        [SerializeField] private Canvas upgradeCanvas;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private int level;
        [SerializeField] private int price;

        private void Awake()
        {
            holdController = FindObjectOfType<HoldController>();
            
            stationVector = transform.position;
            upgradeCanvas.gameObject.SetActive(false);

            ChangePrice();
            levelText.text="Level: "+level;
        }
        
        private void Update()
        {
            CheckDistance();
        }

        public void Upgrade()
        {
            var money = MoneyManager.Instance.Money;
            if (money <= price) return;

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
        private void CheckDistance()
        {
            distance = Vector3.Distance(holdController.transform.position, stationVector);
            upgradeCanvas.gameObject.SetActive(distance <= 9f);
        }//Check Distance between ship and station. TODO: Thinking to put it in StackManager.
        
    }
}
