using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static Action GameEnd;
        public static Action GamePause;


        public GameObject Gameplay, InGameUI, PauseUI, SettingsUI, MainMenuUI;
        public TMP_Text   Score;

        public Player           player;
        public CircleController circleController;

        private bool _firstTouch = false;
        private int  _score      = 0;

        private void OnEnable()
        {
            GameEnd += OnGameEndCalled;
        }

        private void OnGameEndCalled()
        {
            Gameplay.SetActive(false);
            InGameUI.SetActive(false);
        }

        public void StartGame()
        {
            player.StartGame();
            circleController.SpawnCircle();
        }

        public void Preview()
        {
            _score     = 0;
            Score.text = _score.ToString();
            Gameplay.SetActive(true);
            InGameUI.SetActive(true);
            player.Preview();
            circleController.SpawnCircle();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_firstTouch)
                {
                    _firstTouch = true;
                    StartGame();
                }
            }
        }

        private void Start()
        {
            Preview();
        }

        public void PauseGame()
        {
            PauseUI.SetActive(true);
            player.PauseGame();
        }

        public void ResumeGame()
        {
            PauseUI.SetActive(false);
            player.ResumeGame();
        }

        public void MainMenu()
        {
            Gameplay.SetActive(false);
            InGameUI.SetActive(false);
            PauseUI.SetActive(false);
            SettingsUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }
    }
}