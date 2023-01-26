using UnityEngine;

namespace Script.Managers
{
    public class StationManager : MonoBehaviour
    {
        public static StationManager Instance { get; set; }
        
        public GameObject player;
        public GameObject deliverStation;
        public GameObject upgradeStation;

        private Vector3 deliverVector;
        private Vector3 upgradeVector;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /*private void Start()
        {
            deliverVector = deliverStation.transform.position;
            upgradeVector = upgradeStation.transform.position;
        }

        private void Update()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            UpgradeDistance();
            DeliverDistance();
        }

        private void UpgradeDistance()
        {
            var distance = Vector3.Distance(player.transform.position, upgradeVector);
            CanvasManager.Instance.CanvasUpgradeStation = distance <= 10f;
        }
        private void DeliverDistance()
        {
            var distance = Vector3.Distance(player.transform.position, deliverVector);
            CanvasManager.Instance.CanvasDeliverStation = distance <= 10f;
        }*/
    }
}
