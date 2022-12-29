using Script.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Sea
{
    public class SeaClickController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float range = 1000f;
        public Vector3 destinationVector;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (Camera.main == null)
                return;
            
            var ray = Camera.main.ScreenPointToRay(eventData.position);

            if (!Physics.Raycast(ray.origin, ray.direction, out var hit, range)) return;
            if (!hit.transform.CompareTag("Sea")) return;
            Debug.DrawRay(ray.origin, ray.direction * range, Color.green, 1);
            DestinationPoint = hit.point;
        }

        private Vector3 DestinationPoint
        {
            set
            {
                destinationVector = value;
                playerController.SetDestination(destinationVector);
            }
        }
    }
}