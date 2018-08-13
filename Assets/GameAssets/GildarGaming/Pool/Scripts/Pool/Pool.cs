using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Stack based pool
/// </summary>
namespace GildarGaming
{
    public class Pool : Ipool
    {
        private Stack<GameObject> pool;
        public Pool(GameObject objectToPool, int size, GameObject parent)
        {
            pool = new Stack<GameObject>(size);
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

            pool.Push(objectToAdd);

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
                pool.Push(go);

            }
        }

        public GameObject GetObject()
        {
            if (pool.Count == 0) return null;
            return pool.Pop();

        }

        public void Grow(int newSize)
        {
            while (pool.Count < newSize)
            {
                GameObject go = GameObject.Instantiate<GameObject>(pool.Peek());
                go.transform.parent = pool.Peek().transform.parent;
                go.name = pool.Peek().name;
                go.SetActive(false);
                pool.Push(go);
            }
        }

        public void Shrink(int newSize)
        {
            while (pool.Count > newSize)
            {
                GameObject go = pool.Pop();
                GameObject.Destroy(go);

            }

        }
    }
}
