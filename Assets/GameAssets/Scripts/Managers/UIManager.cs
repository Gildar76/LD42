using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace GildarGaming.LD42
{
	public class UIManager : MonoBehaviour
	{
        private static UIManager instance;
        [SerializeField]
        GameObject menuPanel;
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        GameObject gameOverPanel;
        [SerializeField]
        Slider volumeSlider;

        public ScoreUpdateEvent scoreUpdate;

        public static UIManager Instance
        {
            get
            {
                return instance;
            }


        }

        public GameObject GameOverPanel
        {
            get
            {
                return gameOverPanel;
            }

            set
            {
                gameOverPanel = value;
            }
        }

        public GameObject MenuPanel
        {
            get
            {
                return menuPanel;
            }

            set
            {
                menuPanel = value;
            }
        }

        void Start()
        {
            instance = this;
            scoreText.text = "0";
            GameManager.Instance.scoreUpdate.AddListener(OnScoreUpdate);
            MenuPanel.SetActive(true);
            GameOverPanel.SetActive(false);

        }


        void OnScoreUpdate(int newScore)
        {
            scoreText.text = newScore.ToString();

        }

        void OnDisable()
        {
            GameManager.Instance.scoreUpdate.RemoveListener(OnScoreUpdate);
        }
        
        public void OnVolumeChange()
        {
            float volume = volumeSlider.value;
            AudioManager.Instance.SetVolume(volume);
        }

        public void OnStartClick()
        {
            
            GameManager.Instance.Restart();
            MenuPanel.SetActive(false);
        }

	}
}
