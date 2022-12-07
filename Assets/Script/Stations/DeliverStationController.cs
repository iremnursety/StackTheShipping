using Script.Player;
using UnityEngine;

namespace Script.Stations
{
    public class DeliverStationController : MonoBehaviour
    {
        [SerializeField] private HoldController holdController;
        private void Awake()
        {
            holdController = FindObjectOfType<HoldController>();
        
        }

        public void Deliver()
        {
            holdController.EmptyHolder();
        }
       
    }
}
