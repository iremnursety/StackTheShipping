using System.Collections;
using System.Collections.Generic;
using Script.Player;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Script.Stack
{
    public class StackSpawner : MonoBehaviour
    {
        private Coroutine activeCoroutine;

        [SerializeField] private int stackNumber,randomPrefab;
        [SerializeField] private List<GameObject> stackPrefabList = new List<GameObject>();
        
        [SerializeField] private float range, randomValue, minRandom, maxRandom;
        [SerializeField] private HoldController holdController;
        [SerializeField] private Vector3 tempVector;
        [SerializeField] private List<float> tempValueList = new List<float>();
        [SerializeField] private List<StackController> stackList = new List<StackController>();

        
        
        private void Awake()
        {
            ResizeSpawner();
            holdController = FindObjectOfType<HoldController>();
        }

        private void ResizeSpawner()
        {
            var parentScale = transform.parent.localScale;
            var objScale = gameObject.transform.localScale;

            var tempScale = new Vector3(objScale.x / parentScale.x, objScale.y / parentScale.y,
                objScale.z / parentScale.z);
            transform.localScale = tempScale;
        } //Resize Spawner for objects to fit in parent area.

        private void Start()
        {
            range = 1000;
            //------------Set min max Random values.------------
            maxRandom = (transform.parent.localScale.x / 2);
            minRandom = -maxRandom;
            //--------------------------------------------------
            for (stackNumber = 0; stackNumber < 10;)
            {
                RandomLocation();
            }
        }

        private void Update()
        {
            if (stackNumber < 30 && activeCoroutine == null) activeCoroutine = StartCoroutine(SpawnObjectsInTime());
        }

        private IEnumerator SpawnObjectsInTime()
        {
            yield return new WaitForSeconds(2f);
            RandomLocation();
            activeCoroutine = null;
        } //RandomLocation in seconds.


        private void RandomLocation()
        {
            randomPrefab = Random.Range(0, stackPrefabList.Count);
            
            for (var i = 0f; i < 2; i++)
            {
                randomValue = Random.Range(minRandom, maxRandom);
                tempValueList.Add(randomValue);
            }

            tempVector = new Vector3(tempValueList[0], -2.41f, tempValueList[1]);


            var ray = new Ray(transform.position, tempVector);
            if (!Physics.Raycast(ray, out var hit))
            {
                tempValueList.Clear();
                return;
            }

            if (hit.transform.CompareTag("Sea"))
            {
                Debug.DrawRay(ray.origin, tempVector, Color.green, range);
                SpawnObject();
                tempValueList.Clear();
                stackNumber += 1;
            }

            tempValueList.Clear();
        } //Get random pos between min and max value. If ray is on Sea Spawn on pos.

        private void SpawnObject()
        {
            //Spawn Stack and Add to Stack List and Vector List.
            stackList.Add(Instantiate(stackPrefabList[randomPrefab].GetComponent<StackController>()));
            //actualVectorList.Add(tempVector);

            //Stack obj settings.
            var tempObj = stackList[stackList.Count - 1];
            var tempObjTransform = tempObj.transform;
            tempObjTransform.parent = gameObject.transform;
            tempObjTransform.localPosition = tempVector;
            tempObj.name = "Stack" + (stackList.Count - 1);
            tempObj.stackSpawner = this;
            tempObj.stackNumber = stackNumber;
        } //Spawn Stack on random pos value. Change parent and name. Add on List.

        public void DeleteStack(int stackIndex)
        {
            var tempStack = stackList[stackIndex];
            holdController.HoldStack(tempStack);
            stackList.RemoveAt(stackIndex);
            for (var i = stackIndex; i < stackList.Count; i++)
            {
                stackList[i].stackNumber = i;
                stackList[i].name = "Stack" + i;
            }

            stackNumber = stackList.Count;
        } //Delete object from list stackIndex.

        public void SpawnMore()
        {
            for(var i=0;i<10;i++)
                RandomLocation();
        }
    }
}