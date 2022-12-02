using UnityEngine;

namespace Script.Managers
{
    public class StackManager : MonoBehaviour
    {
        public static StackManager Instance { get; private set; }
        public bool shipCanHold;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public bool CanHold
        {
            get => shipCanHold;
            set => shipCanHold = value;
        } //Get & Set if ship can hold or not.
        
    }
    
}
