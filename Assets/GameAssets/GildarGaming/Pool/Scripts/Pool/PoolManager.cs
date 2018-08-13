using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GildarGaming
{
    public enum PoolType
    {
        QueuePool, StackPool, ListPool
    }
    public class PoolManager
    {

        private PoolType poolType;
        public bool debug = false;
        public static PoolManager Instance
        {
            get
            {
                //Make sure instance exists, if it doesn't create it.
                if (instance == null) instance = new PoolManager();
                return instance;
            }
        }

        private Dictionary<GameObject, Ipool> objectPools;
        private static PoolManager instance;
        //Dictionary to keep track of every instance
        Dictionary<int, GameObject> poolLink;

        public PoolManager()
        {
            if (instance != null) return;
        
            objectPools = new Dictionary<GameObject, Ipool>();
            poolLink = new Dictionary<int, GameObject>();


        }

        /// <summary>
        /// Builds an object pool of objectToPool of size size.
        /// </summary>
        /// <param name="objectToPool">Object to use for this pool (usually a prefab)</param>
        /// <param name="size">Number of objects to generate.</param>
        /// <param name="parent">The parent object. Can be used to control position, or somply as a container.</param>
        public void CreatePool(GameObject objectToPool, int size, PoolType type, GameObject parent)
        {
            //We use the objectToPool.name to index our pools
            if (objectPools.ContainsKey(objectToPool))
            {
                //If it exists, we clear and refill the pool
                objectPools.Remove(objectToPool);
            }

            switch (type)
            {
                case PoolType.ListPool:
                    objectPools[objectToPool] = new ListPool(objectToPool, size, parent);

                    break;
                case PoolType.QueuePool:
                    objectPools[objectToPool] = new QeuePool(objectToPool, size, parent);
                    break;
                case PoolType.StackPool:
                    objectPools[objectToPool] = new Pool(objectToPool, size, parent);
                    break;

                  

            }

            

        }



        /// <summary>
        /// Gets a pooled object by prefab. 
        /// </summary>
        /// <param name="prefab">Prefab / Game object to look for.</param>
        /// <param name="position">Starting position</param>
        /// <param name="rotation">Starting rotation</param>
        /// <returns>Returns a GameObject from the selected pool.</returns>
        public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {

            //GameObject go = GetPooledObject(prefab, position, rotation);
            if (objectPools.ContainsKey(prefab))
            {
                GameObject go = objectPools[prefab].GetObject();
                if (go == null) return null;
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
                poolLink.Add(go.GetInstanceID(), prefab);
                return go;
            }
            return null;


        }

        /// <summary>
        /// Deactivates an object.
        /// </summary>
        /// <param name="objectToDestroy">Gameobject to deactivate</param>
        public void DestroyObject(GameObject objectToDestroy)
        {
            GameObject prefab;
            poolLink.TryGetValue(objectToDestroy.GetInstanceID(), out prefab);

            
            if (objectPools.ContainsKey(prefab))
            {

                objectToDestroy.SetActive(false);
                objectPools[prefab].AddObject(objectToDestroy);
                poolLink.Remove(objectToDestroy.GetInstanceID());
            }

        }
        /// <summary>
        /// Reduces the size of the pool at runtime.
        /// </summary>
        /// <param name="objectToPool">Object taht identifies the pool</param>
        /// <param name="newSize">The new size. If size is larger than the current pool, nothing will be done. </param>
        private void ShrinkPool(GameObject objectToPool, int newSize)
        {
            Debug.Log("Shrinking pool");
            if (objectPools.ContainsKey(objectToPool))
            {
                objectPools[objectToPool].Shrink(newSize);
            }
        }
        /// <summary>
        /// Increase the size of the pool at runtime
        /// </summary>
        /// <param name="objectToPool">Object taht identifies the pool</param>
        /// <param name="newSize">New size. If size is smaller than the current pool, nothing will be done.</param>
        private void GrowPool(GameObject objectToPool, int newSize)
        {

            if (objectPools.ContainsKey(objectToPool))
            {
                objectPools[objectToPool].Grow(newSize);
            }
        }

        /// <summary>
        /// Grows or shrinks the pool at runtime, depending on current size vs. new size
        /// </summary>
        /// <param name="objectToPool">Gameobject that identifies the pool.</param>
        /// <param name="newSize">The new pool size</param>
        public void SetPoolSize(GameObject objectToPool, int newSize)
        {

            if (objectPools.ContainsKey(objectToPool))
            {

                //Just calling shrink and grow on the pool should eventually set the right size. If it's to big, grow will do nothing and vice versa.
                objectPools[objectToPool].Grow(newSize);
                objectPools[objectToPool].Shrink(newSize);
            }
        }
        /// <summary>
        /// Currently only prints pool sizes (doctionary, not underlaying pools) and all keys to the console. Useful if your object isn't found.
        /// </summary>
        public void PrintPoolStats()
        {
            Debug.Log("Base pool count: " + objectPools.Count);
            foreach (KeyValuePair<GameObject, Ipool> thePool in objectPools)
            {
                Debug.Log(thePool.Key + " : " + thePool.Value.Count);

            }
        }
    }

}
