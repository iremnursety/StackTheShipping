using UnityEngine;

namespace Script.Managers
{
    public class CanvasManager : MonoBehaviour
    {
        public static CanvasManager Instance { get;  set; }

        [SerializeField] private Canvas info;
        [SerializeField] private Canvas deliverStation;
        [SerializeField] private Canvas upgradeStation;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            
            CanvasInfo = true;
            CanvasDeliverStation = false;
            CanvasUpgradeStation = false;
        }

        public bool CanvasInfo
        {
            set => info.gameObject.SetActive(value);
        }
        public bool CanvasDeliverStation
        {
            set => deliverStation.gameObject.SetActive(value);
        }
        public bool CanvasUpgradeStation
        {
            set => upgradeStation.gameObject.SetActive(value);
        }
    }
}
