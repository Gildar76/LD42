using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class BrickController : MonoBehaviour
	{
        AudioSource hitAudio;
        Rigidbody rb;
        [SerializeField]
        int score;
        [SerializeField]
        private int addBrickCount;
        [SerializeField]
        private string ballTag = "Ball";
        bool isDestroyed = false;
        void Awake()
        {
            rb = GetComponent<Rigidbody>();

        }

        void OnEnable()
        {
            if (hitAudio == null)
            {
                hitAudio = GetComponent<AudioSource>();

            }
            isDestroyed = false;
        }

        void OnCollisionEnter(Collision other)
        {
            if (hitAudio.clip == null)
            {
                hitAudio.clip = AudioManager.Instance.BallHitBrick;
            }
            if (isDestroyed) return;
            if (other.gameObject.CompareTag(ballTag))
            {
                hitAudio.volume = AudioManager.Instance.volumeLevel;
                hitAudio.Play();
                LightFlicker lf = other.gameObject.GetComponent<LightFlicker>();
                if (lf != null)
                {
                    Debug.Log("Lf enabled");
                    lf.enabled = true;
                }

                GameManager.Instance.Score += score;
                //Soawn an explosion or something
                PoolManager.Instance.GetPooledObject(SpawnManager.Instance.brickExplosion, transform.position, Quaternion.identity);
                SpawnManager.Instance.Spawn(addBrickCount);

                PoolManager.Instance.DestroyObject(this.gameObject);
                isDestroyed = true;

            }
        }

	}
}
