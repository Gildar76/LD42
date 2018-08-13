GildarGaming.Pool v1.0

Currently only supports pooling of GameObjects, no other objects.
The pool supports 3 types, Stack, Queue, and List. Stack or Queue is the preferred types unless the pool is very small.

USAGE>
Create a pool
PoolManager.Instance.CreatePool(prefab, size, poolType.Stack, null);
Note: The last value is the parent object, null is the preferred unless you have a specific reason to parent the objects.

Get object from pool.
GameObject go = PoolManager.Instance.GetPooledObject(prefab, position, rotation);

Return object to pool.
PoolManager.Instance.Destroy(objectToDestroy)<
This will return the object to the pool and deactivate it.

More information is available in code. See SsmpleSpawnManager for an example on how a basic spawn manager can be setup.
