using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class BombController : MonoBehaviour
	{
        private string ballTag = "Ball";
        bool isDestroyed = false;
        void Awake()
        {


        }

        void OnEnable()
        {
            isDestroyed = false;
        }

        void OnCollisionEnter(Collision other)
        {

            if (isDestroyed) return;
            if (other.gameObject.CompareTag(ballTag))
            {

                //Soawn an explosion or something
                SpawnManager.Instance.BlowUpBomb(gameObject);


                PoolManager.Instance.DestroyObject(this.gameObject);
                isDestroyed = true;

            }
        }
    }
}
