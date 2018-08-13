using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD42
{
	public class GameManager : MonoBehaviour
	{
        [SerializeField]
        private int score;
        public ScoreUpdateEvent livesUpdate; //We use scoreupdate for both, should have a better name...
        AudioSource deathAudio;
        [SerializeField]
        private int lives;
        static GameManager instance;
        [SerializeField]
        float startingForce;
        public ScoreUpdateEvent scoreUpdate;

        public static GameManager Instance
        {
            get
            {
                return instance;
            }


        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {

                score = value;
                scoreUpdate.Invoke(score);
            }
        }

        public int Lives
        {
            get
            {
                return lives;
            }

            set
            {
                lives = value;
            }
        }

        private Rigidbody ballRb;
        void Awake()
        {
            scoreUpdate = new ScoreUpdateEvent();
            livesUpdate = new ScoreUpdateEvent();
            if (instance == null) instance = this;
        }
        void Start()
        {
            if (ballRb == null)
            {
                ballRb = GameObject.FindWithTag("Ball").GetComponent<Rigidbody>();

            }
            
        }

        public void GameOver()
        {
            if (deathAudio == null)
            {
                deathAudio = GetComponent<AudioSource>();

            }
            deathAudio.clip = AudioManager.Instance.DeathSound;
            deathAudio.Play();
            UIManager.Instance.GameOverPanel.SetActive(true);
            SpawnManager.Instance.Reset();

        }

        public void Restart()
        {
            Score = 0;
            Lives = 3;
            PlayerMovementController playerMover = GameObject.FindObjectOfType<PlayerMovementController>();
            playerMover.transform.position = new Vector3(95f, -75f, -1f);
            GameObject.FindWithTag("Ball").transform.parent = playerMover.transform;
            GameObject.FindWithTag("Ball").transform.localPosition = new Vector3(0f, 5f, 0f);
            ballRb.isKinematic = true;
            SpawnManager.Instance.Restart();
            UIManager.Instance.GameOverPanel.SetActive(false);

        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ballRb.isKinematic == false) return;
                ballRb.gameObject.transform.parent = null;
                ballRb.isKinematic = false;
                ballRb.AddForce(new Vector3(0.0f, startingForce, 0.0f));
            }
        }
    }
}
