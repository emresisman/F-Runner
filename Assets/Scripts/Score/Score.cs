using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        public void UpdateScore(float _speed)
        {
            _score += (int)(_speed * 5f) - (int)((_speed * 10f) % 10);
            if (_score > _highScore) UpdateHighScore();
            //UIManager.Instance.UpdateScoreText(_score);
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
            SetHighScore();
        }
    }
}
