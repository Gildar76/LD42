using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class ExplosionCOntroller : MonoBehaviour
	{
        private float explosionDuraction;
        private float timer;
        void Awake()
        {
            explosionDuraction = GetComponent<ParticleSystem>().main.duration;
        }
        void OnEnable()
        {
            timer = 0.0f;

        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer > explosionDuraction)
            {
                PoolManager.Instance.DestroyObject(this.gameObject);

            }
        }
	}
}
