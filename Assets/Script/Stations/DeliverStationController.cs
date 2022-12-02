using Script.Player;
using UnityEngine;

namespace Script.Stations
{
    public class DeliverStationController : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private Vector3 stationVector;
        [SerializeField] private HoldController holdController;
        [SerializeField] private Canvas deliverCanvas;
        private void Awake()
        {
            holdController = FindObjectOfType<HoldController>();
            
            stationVector = transform.position;
            deliverCanvas.gameObject.SetActive(false);
        }
        

        private void Update()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            distance = Vector3.Distance(holdController.transform.position, stationVector);
            deliverCanvas.gameObject.SetActive(distance <= 9f);
        } //Check Distance between ship and station. TODO: Thinking to put it in StationManager.
        
    }
}
