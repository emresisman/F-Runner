using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FRunner.UI;

namespace FRunner
{
    public class Score : MonoBehaviour
    {
        #region Singleton
        private static Score _instance;
        public static Score Instance { get => _instance; }
        #endregion

        [SerializeField] private ScoreSO _scoreSO;
        private int _score;
        private int _highScore;

        private void Start()
        {
            _instance = this;
            _score = 0;
            GetHighScore();
            UIManager.Instance.UpdateHighScoreText(_highScore);
            UIManager.Instance.UpdateScoreText(_score);
        }

        public void UpdateScore(float speed)
        {
            _score += (int)(speed * 5f) - (int)((speed * 5f) % 10);
            if (_score > _highScore) UpdateHighScore();
            UIManager.Instance.UpdateScoreText(_score);
        }

        private void GetHighScore()
        {
            _highScore = _scoreSO.HighScore;
        }

        private void SetHighScore()
        {
            _scoreSO.HighScore = _highScore;
        }

        private void UpdateHighScore()
        {
            _highScore = _score;
            UIManager.Instance.UpdateHighScoreText(_highScore);
            SetHighScore();
        }
    }
}
