using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class SpawnManager : MonoBehaviour
	{
        private static SpawnManager instance;
        [SerializeField]
        GameGrid grid;
        List<Material> brickMaterialList;
        public int gridSizeX;
        public int gridSizeY;
        public GameObject brickExplosion;
        public GameObject bombPrefab;
        public List<GameObject> brickPrefabs;
        public GameObject bombExplosion;
        GameObject ball;
        Rigidbody ballRb;

        public List<GameObject> activeBricks;
        public static SpawnManager Instance
        {
            get
            {

                return instance;
            }


        }

        public GameGrid Grid
        {
            get
            {
                return grid;
            }

            set
            {
                grid = value;
            }
        }

        void Awake()
        {

            if (instance == null)
            {
                instance = this;
            }
        }

        void Start()
        {
            ball = GameObject.FindWithTag("Ball");
            PoolManager.Instance.CreatePool(bombExplosion, 50, PoolType.StackPool, null);
            PoolManager.Instance.CreatePool(bombPrefab, 20, PoolType.StackPool, null);
            Grid = new GameGrid(gridSizeX, gridSizeY);
            for (int i = 0; i < brickPrefabs.Count; i++)
            {
                PoolManager.Instance.CreatePool(brickPrefabs[i], 2000, PoolType.StackPool, null);

            }
            PoolManager.Instance.CreatePool(brickExplosion, 100, PoolType.StackPool, null);

            activeBricks = new List<GameObject>();
            brickMaterialList = new List<Material>();
            Restart();

        }
    
        public void Reset()
        {

            foreach (GameObject obj in activeBricks)
            {
                if (obj.activeInHierarchy)
                {
                    PoolManager.Instance.DestroyObject(obj);
                }


            }
            foreach (GridNode node in Grid.Grid)
            {
                if (node == null) continue;
                if (node.currentObject != null)
                {
                    if (node.currentObject.activeInHierarchy)
                    {
                        PoolManager.Instance.DestroyObject(node.currentObject);
                    }

                    node.currentObject = null;
                }
            }
        }
        public void Restart()
        {

            activeBricks.Clear();
            //brickMaterialList.Clear();


            foreach (GridNode node in Grid.Grid)
            {

                if (node == null) continue;
                if (node.currentObject != null)
                {
                    //if (node.currentObject.activeInHierarchy) PoolManager.Instance.DestroyObject(node.currentObject);
                    //node.currentObject = null;
                }
                if (brickPrefabs[0] != null)
                {
                    if (node.Position.y == 0)
                    {
                        node.currentObject = PoolManager.Instance.GetPooledObject(brickPrefabs[0], node.Position, Quaternion.identity);
                        brickMaterialList.Add(node.currentObject.GetComponentInChildren<MeshRenderer>().material);
                        activeBricks.Add(node.currentObject);
                    } else
                    {
                        node.currentObject = null;

                    }



                }
                

            }


        }

        public void BlowUpBomb(GameObject bomb)
        {
            PoolManager.Instance.GetPooledObject(bombExplosion, bomb.transform.position, Quaternion.identity);

            for (int i = 0; i < activeBricks.Count; i++)
            {
                if (activeBricks[i].activeInHierarchy)
                {
                    if (Vector3.Distance(activeBricks[i].transform.position, bomb.transform.position) < 30.0f)
                    {
                        PoolManager.Instance.DestroyObject(activeBricks[i]);
                        GameManager.Instance.Score += 5;
                    }
                }
            }

        }

        void FixedUpdate()
        {
            if (ballRb == null)
            {
                ballRb = ball.GetComponent<Rigidbody>();

            }
            if (ballRb.velocity.magnitude > 100.0f)
            {
                ballRb.velocity = ballRb.velocity.normalized * 100f;
            }
            else if (ballRb.velocity.magnitude < 20.0f)
            {
                ballRb.velocity = ballRb.velocity.normalized * 20f;
            }
            
        }
        void Update()
        {
            for (int i = 0; i < brickMaterialList.Count; i++)
            {
                float emission = Mathf.PingPong(Time.time, 0.5f);
                Color baseColor = brickMaterialList[i].color;
                Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
                brickMaterialList[i].SetColor("_EmissionColor", finalColor);
            }
        }
        public void Spawn(int spawnCount)
        {
            
            //Pick what to spawn
            for (int i = 0; i <= spawnCount; i++)
            {
                int index = Random.Range(0, brickPrefabs.Count);
                //Randomize spawn of bombs based on number of bricks added.

                GridNode spawnOnNode;
                spawnOnNode = Grid.Grid[Random.Range(0, Grid.Grid.Length - 1)];
                //If gridnode is occupied by another brick. Find a new one (could cause an infinite loop but we'll cover that later).
                int j = 0;
                while (spawnOnNode.currentObject != null)
                {
                    if (!spawnOnNode.currentObject.activeInHierarchy)
                    {
                        activeBricks.Remove(spawnOnNode.currentObject);
                        spawnOnNode.currentObject = null;
                    }
                    j++;
                    spawnOnNode = Grid.Grid[Random.Range(0, Grid.Grid.Length - 1)];
                    if (j > 100)
                    {
                        return;
                    }
                }
                if (Random.Range(0, 200) < spawnCount)
                {
                    spawnOnNode.currentObject = PoolManager.Instance.GetPooledObject(bombPrefab, spawnOnNode.Position, Quaternion.identity);
                    
                } else
                {
                    spawnOnNode.currentObject = PoolManager.Instance.GetPooledObject(brickPrefabs[index], spawnOnNode.Position, Quaternion.identity);
                    brickMaterialList.Add(spawnOnNode.currentObject.GetComponentInChildren<MeshRenderer>().material);
                    activeBricks.Add(spawnOnNode.currentObject);
                }


            }

        }

        
    }
}
