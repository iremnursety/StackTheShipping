using UnityEngine;
using UnityEngine.AI;

namespace Script.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
       
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }


        public void SetDestination(Vector3 destinationPoint)
        {
            navMeshAgent.destination = destinationPoint;
        }
        
    }
}