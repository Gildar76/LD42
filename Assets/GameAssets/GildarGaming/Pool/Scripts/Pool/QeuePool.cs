using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Queue based pool
/// </summary>

namespace GildarGaming
{
    public class QeuePool : Ipool
    {
        private Queue<GameObject> pool;
        public QeuePool(GameObject objectToPool, int size, GameObject parent)
        {
            pool = new Queue<GameObject>(size);
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

            pool.Enqueue(objectToAdd);

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
                pool.Enqueue(go);

            }
        }

        public GameObject GetObject()
        {
            if (pool.Count > 0) return pool.Dequeue();
            return null;
        }

        public void Grow(int newSize)
        {
            while (pool.Count < newSize)
            {
                GameObject go = GameObject.Instantiate<GameObject>(pool.Peek());
                go.transform.parent = pool.Peek().transform.parent;
                go.name = pool.Peek().name;
                go.SetActive(false);
                pool.Enqueue(go);
            }
        }

        public void Shrink(int newSize)
        {
            while (pool.Count > newSize)
            {
                GameObject go = pool.Dequeue();
                GameObject.Destroy(go);

            }

        }
    }
}
