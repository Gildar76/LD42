using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
    
	public class BallHitter : MonoBehaviour
	{
        private AudioSource ballHitSource;
        private Rigidbody BallRigidbody;
        public float forceToAdd;
        string ballTag = "Ball";

        void Awake()
        {
            ballHitSource = GetComponent<AudioSource>();
        }

        void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.CompareTag(ballTag))
            {
                float realForceToAdd = forceToAdd + GameManager.Instance.Score * 3;
                Rigidbody ballRb = other.gameObject.GetComponent<Rigidbody>();
                //ballRb.velocity = ballRb.velocity * -1;
                ballRb.AddForce(new Vector3(ballRb.velocity.x * -1, realForceToAdd, 0.0f));
                if (ballHitSource.clip == null)
                {
                    ballHitSource.clip = AudioManager.Instance.BallHitPlayer;
                    
                }
                ballHitSource.Play();


            }
        }
    }
}
