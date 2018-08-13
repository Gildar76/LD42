using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class DeathController : MonoBehaviour
	{
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                GameManager.Instance.GameOver();
               
            }
        }
	}
}
