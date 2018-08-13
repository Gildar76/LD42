using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// List based pool. Slow for large pools. Needs to be optimized with an index pointer to be fast.
/// </summary>
namespace GildarGaming
{
    public class ListPool : Ipool
    {
        private List<GameObject> pool;
        public ListPool(GameObject objectToPool, int size, GameObject parent)
        {
            pool = new List<GameObject>(size);
            Fill(objectToPool, size, parent);
        }

        public int Count
        {
            get
            {
                return pool.Count;
            }
        }

        public void AddObject(GameObject objectToAdd)
        {

            pool.Add(objectToAdd);

        }

        public void Clear()
        {
            pool.Clear();
        }

        public void Fill(GameObject originalObject, int size, GameObject prentObject)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject go = GameObject.Instantiate<GameObject>(originalObject);
                go.SetActive(false);
                go.name = originalObject.name;
                if (prentObject != null) go.transform.parent = prentObject.transform;
                pool.Add(go);

            }
        }

        public GameObject GetObject()
        {

            for (int i = 0; i < pool.Count; i++)
            {


                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
            return null;
        }

        public void Grow(int newSize)
        {

            Debug.Log(newSize);
            while (pool.Count <= newSize)
            {

                GameObject go = GameObject.Instantiate<GameObject>(pool[0]);

                go.transform.parent = pool[0].transform.parent;
                go.name = pool[0].name;
                go.SetActive(false);
                pool.Add(go);
            }
        }

        public void Shrink(int newSize)
        {
            while (pool.Count > newSize)
            {
                pool.RemoveAt(pool.Count - 1);


            }

        }
    }
}
