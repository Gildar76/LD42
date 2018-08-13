using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GildarGaming
{
    interface Ipool
    {
        /// <summary>
        /// Gets Gameobject from pool.
        /// </summary>
        /// <returns>GameObject</returns>
        GameObject GetObject();
        /// <summary>
        /// Adds object to pool.
        /// </summary>
        /// <param name="objectToAdd">The object to add</param>
        void AddObject(GameObject objectToAdd);
        /// <summary>
        /// Empty the pool.
        /// </summary>
        void Clear();
        /// <summary>
        /// Fills the pool with copies of original object.
        /// </summary>
        /// <param name="originalObject">Game object to use for this pool.</param>
        /// <param name="size">Number of objects to instantiate.</param>
        /// <param name="prentObject">A parentobject to serve as a container.</param>
        void Fill(GameObject originalObject, int size, GameObject prentObject);
        /// <summary>
        /// Increase size of the pool
        /// </summary>
        /// <param name="newSize">new size of the pool</param>
        void Grow(int newSize);
        /// <summary>
        /// Reduces size of the pool, destroying excess game objects
        /// </summary>
        /// <param name="newSize">new size</param>
        void Shrink(int newSize);
        int Count { get; }

    }
}
