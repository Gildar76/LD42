using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
    [System.Serializable]
	public class GameGrid 
	{
        public GridNode[] Grid { get; set; }
        [SerializeField]
        public int sizeX;
        [SerializeField]
        public int sizeY;
        public GameGrid(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            Grid = new GridNode[sizeX * sizeY + 1];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    Grid[x + (sizeX* y)] = new GridNode(new Vector3(x * 6, y * -4, -1.0f), null);
                }
            }
            
        }

        
	}
}
