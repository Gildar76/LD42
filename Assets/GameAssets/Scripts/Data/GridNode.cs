using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class GridNode 
	{
        public  Vector3 Position { get; set; }
        public GameObject currentObject { get; set; }

        public GridNode(Vector3 position, GameObject currentObject)
        {
            Position = position;
            this.currentObject = currentObject;
        }
    }
}
