using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class LightFlicker : MonoBehaviour
	{
        Light light;
        float timer = 0.0f;
        float maxtime = 0.4f;

        void OnEneble()
        {
            timer = 0.0f;
            if (light == null)
            {
                light = GetComponentInChildren<Light>();
            }
            light.intensity = 1.0f;
        }
        void Update()
        {
            if (light == null)
            {
                light = GetComponentInChildren<Light>();
            }
            timer += Time.deltaTime;
            if (timer > maxtime / 2)
            {
                light.intensity = 1.8f - (timer / 2) * 4;
            } else
            {
                light.intensity = 1 + timer * 20f;
            }
            if (timer > maxtime)
            {
                light.intensity = 1.0f;
                this.enabled = false;
            }

        }
	}
}
