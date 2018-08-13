using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class PlayerMovementController : MonoBehaviour
	{

        public float movementSpeed;
        private Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        void FixedUpdate()
        {
            
            float movementX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector3(movementX * movementSpeed, 0.0f, 0.0f);
        }

	}
}
