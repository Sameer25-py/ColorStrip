using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static Action GameEnd;
        public static Action GamePause;


        public GameObject Gameplay, InGameUI, PauseUI, SettingsUI, MainMenuUI, EndGameUI, FirstTouchUI;
        public TMP_Text   Score,HighScore;

        public Player           player;
        public CircleController circleController;

        private bool _firstTouch = false;
        private int  _score      = 0;

        private void OnEnable()
        {
            GameEnd                   += OnGameEndCalled;
            CircleController.Collided += OnCircleCollided;
        }

        private void OnCircleCollided()
        {
            _score         += 1;
            Score.text     =  _score.ToString();
            HighScore.text =  _score.ToString();
        }

        private void OnGameEndCalled()
        {
            Gameplay.SetActive(false);
            InGameUI.SetActive(false);
            EndGameUI.SetActive(true);
        }

        public void StartGame()
        {
            FirstTouchUI.SetActive(false);
            player.StartGame();
            circleController.SpawnCircle();
            _score         = 0;
            Score.text     = _score.ToString();
            HighScore.text = _score.ToString();
        }

        public void Preview()
        {
            _score         = 0;
            Score.text     = _score.ToString();
            HighScore.text = _score.ToString();
            MainMenuUI.SetActive(false);
            Gameplay.SetActive(true);
            InGameUI.SetActive(true);
            player.Preview();
            circleController.SpawnCircle();
            _firstTouch = false;
            FirstTouchUI.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(0))
            {
                if (!_firstTouch)
                {
                    _firstTouch = true;
                    StartGame();
                }
            }
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
            EndGameUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }
    }
}