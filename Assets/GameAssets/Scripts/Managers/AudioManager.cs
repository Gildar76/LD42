using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class AudioManager : MonoBehaviour
	{
        private static AudioManager instance;
        [SerializeField]
        AudioClip music;
        [SerializeField]
        AudioClip ballHitPlayer;
        [SerializeField]
        AudioClip ballHitBrick;
        [SerializeField]
        AudioClip bombExplosion;
        public float  volumeLevel;
        [SerializeField]
        AudioClip deathSound;
        [SerializeField]
        AudioClip menuClick;
        public static AudioManager Instance
        {
            get
            {
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public AudioClip Music
        {
            get
            {
                return music;
            }

            set
            {
                music = value;
            }
        }

        public AudioClip BallHitPlayer
        {
            get
            {
                return ballHitPlayer;
            }

            set
            {
                ballHitPlayer = value;
            }
        }

        public AudioClip BallHitBrick
        {
            get
            {
                return ballHitBrick;
            }

            set
            {
                ballHitBrick = value;
            }
        }

        public AudioClip BombExplosion
        {
            get
            {
                return bombExplosion;
            }

            set
            {
                bombExplosion = value;
            }
        }

        public AudioClip DeathSound
        {
            get
            {
                return deathSound;
            }

            set
            {
                deathSound = value;
            }
        }

        public AudioClip MenuClick
        {
            get
            {
                return menuClick;
            }

            set
            {
                menuClick = value;
            }
        }

        void Awake()
        {
            instance = this;
        }

        public void SetVolume(float value)
        {
            //Quick method just to set all volume levels.
            AudioSource[] allsources = FindObjectsOfType<AudioSource>();
            for (int i = 0; i < allsources.Length; i++)
            {
                allsources[i].volume = value;
            }
            volumeLevel = value;
        }


    }
}
