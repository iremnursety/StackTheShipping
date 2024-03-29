using System.Collections.Generic;
using Script.Player;
using Script.Stack;
using UnityEngine;

namespace Script.Managers
{
    public class StackManager : MonoBehaviour
    {
        public static StackManager Instance { get; private set; }
        private HoldController holdController;
        
        //public List<StackController> playerStackController = new List<StackController>();
        //public List<StackController> enemyStackController = new List<StackController>();
        public int maxCount;
        public bool shipCanHold;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            holdController = FindObjectOfType<HoldController>();
        }

        public bool CanHold
        {
            get => shipCanHold;
            set => shipCanHold = value;
        } //Get & Set if ship can hold or not.

        // public StackController PlayerAddList
        // {
        //     set => playerStackController.Add(value);
        // }
        // public StackController EnemyAddList
        // {
        //     set => enemyStackController.Add(value);
        // }
        //TODO: Move HoldController clear to StackManager
        public void PlayerDeliver()
        {
            holdController.EmptyHolder();
        }
        public void DeliverForEnemy(GameObject enemyDeliver)
        {
            var enemyHolder = enemyDeliver.GetComponent<HoldController>();
            enemyHolder.EmptyHolder();
        }

        public void PlayerStackCount()
        {
            
        }
        public void EnemyStackCount()
        {
            
        }
    }
    
}
