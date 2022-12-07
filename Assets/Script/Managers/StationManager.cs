
using UnityEngine;

namespace Script.Managers
{
    public class StationManager : MonoBehaviour
    {
        public static StationManager Instance { get; set; }
        
        public GameObject player;
        public GameObject deliverStation;
        public GameObject upgradeStation;

        private Vector3 _deliverVector;
        private Vector3 _upgradeVector;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            _deliverVector = deliverStation.transform.position;
            _upgradeVector = upgradeStation.transform.position;
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
            var distance = Vector3.Distance(player.transform.position, _upgradeVector);
            CanvasManager.Instance.CanvasUpgradeStation = distance <= 10f;
        }
        private void DeliverDistance()
        {
            var distance = Vector3.Distance(player.transform.position, _deliverVector);
            CanvasManager.Instance.CanvasDeliverStation = distance <= 10f;
        }
    }
}
