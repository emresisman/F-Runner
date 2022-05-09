using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRunner
{
    [CreateAssetMenu(fileName = "Score", menuName = "Score/ScoreScriptableObject", order = 1)]
    public class ScoreSO : ScriptableObject
    {
        public int HighScore;
    }
}
