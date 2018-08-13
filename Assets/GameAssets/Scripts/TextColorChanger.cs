using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace GildarGaming.LD42
{
	public class TextColorChanger : MonoBehaviour
	{
        public float changeInterval = 3.0f;
        float timer = 0.0f;
        TextMeshProUGUI textmeshPro;
        Color startColor;
        public Color newColor;

        void Awake()
        {
            textmeshPro = GetComponent<TextMeshProUGUI>();
            startColor = textmeshPro.color;
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer > changeInterval)
            {
                timer = 0.0f;
                if (textmeshPro.color != startColor)
                {
                    textmeshPro.color = startColor;
                } else
                {
                    textmeshPro.color = newColor;
                }

            }
        }

	}
}
