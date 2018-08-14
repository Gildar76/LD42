using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class Exploder : MonoBehaviour
	{
        float shakeDuration = 0.3f;
        AudioSource explosionAudio;
        float timer = 0.0f;
        Camera mainCamera;
        [SerializeField]
        float cameraMaxOffset = 10.0f;
        Vector3 cameraOriginalPosition;
        [SerializeField]
        float duration = 3.0f;
        Vector3 minPos;
        Vector3 MaxPos;
        float destoryTimer = 0.0f;

        void Start()
        {

            cameraOriginalPosition = GameManager.Instance.cameraPos;
            mainCamera = Camera.main;
            minPos = new Vector3(cameraOriginalPosition.x - cameraMaxOffset, cameraOriginalPosition.y - cameraMaxOffset, cameraOriginalPosition.z);

        }

        void OnEnable()
        {
            if (explosionAudio == null)
            {
                explosionAudio = GetComponent<AudioSource>();
            }
            if (explosionAudio.clip == null)
            {
                explosionAudio.clip = AudioManager.Instance.BombExplosion;
            }
            explosionAudio.volume = AudioManager.Instance.volumeLevel;
            explosionAudio.Play();
            GetComponent<ParticleSystem>().Clear(); ;
            GetComponent<ParticleSystem>().Play();
        }

        void LateUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= shakeDuration)
            {
                mainCamera.transform.position = cameraOriginalPosition;
            } else
            {
                Vector3 currentCameraPosition = cameraOriginalPosition;
                currentCameraPosition.x = currentCameraPosition.x + Random.Range(-cameraMaxOffset, cameraMaxOffset + 10.0f);
                currentCameraPosition.y = currentCameraPosition.y + Random.Range(-cameraMaxOffset, cameraMaxOffset + 10.0f);
                mainCamera.transform.position = currentCameraPosition;
                

            }
            if (destoryTimer >= 2.0f)
            {
                GetComponent<ParticleSystem>().Clear();
                PoolManager.Instance.DestroyObject(this.gameObject);
            }

        }
	}
}
