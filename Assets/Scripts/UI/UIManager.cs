using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FRunner.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        private static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }
        #endregion

        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        [SerializeField] private TMP_Text _coinText;


        private void Start()
        {
            _instance = this;
        }

        public void UpdateScoreText(float score)
        {
            _scoreText.text = "Score : " + score.ToString();
        }        
        
        public void UpdateHighScoreText(float highScore)
        {
            _highScoreText.text = "High Score : " + highScore.ToString();
        }

        public void UpdateCoinText(int coinValue)
        {
            _coinText.text = coinValue.ToString();
        }

        public void StartButtonClick()
        {

        }

        public void ExitButtonClick()
        {

        }
    }
}
