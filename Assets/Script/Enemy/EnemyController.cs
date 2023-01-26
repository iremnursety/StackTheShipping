using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Script.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        [SerializeField] private LayerMask stack;
        [SerializeField] private float searchRadius;
        [SerializeField] private List<Collider> stacks = new List<Collider>();
        [SerializeField] private float enemyMoney;
        //private LinkedList<ColliderDistance> collidersList = new LinkedList<ColliderDistance>();
        private Coroutine activeCoroutine;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (activeCoroutine != null) return;
            if (stacks.Count != 0) return;

            activeCoroutine = StartCoroutine(MoveToStack());
        }

        private IEnumerator MoveToStack()
        {
            var colliders = Physics.OverlapSphere(navMeshAgent.transform.position, searchRadius, stack);

            if (colliders.Length == 0)
                yield break;

            foreach (var t in colliders)
            {
                stacks.Add(t);
            }
            var randomStack = Random.Range(0, stacks.Count);
            NewPath(stacks[randomStack].gameObject);
            yield return new WaitForSeconds(1f);
            stacks.Clear();
            activeCoroutine = null;
        }

        private void NewPath(GameObject randomStackPath)
        {
            if (navMeshAgent.hasPath) return;
            navMeshAgent.SetDestination(randomStackPath.transform.position);
            Debug.Log("New Path");
            //if(navMeshAgent.hasPath)
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(navMeshAgent.transform.position, searchRadius);
        // }

        // private class ColliderDistance
        // {
        //     private Collider collider;
        //     private float distance;
        //
        //     public ColliderDistance(Collider col, float dist)
        //     {
        //         collider = col;
        //         distance = dist;
        //     }
        // }
    }
}