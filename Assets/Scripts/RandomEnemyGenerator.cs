using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class RandomEnemyGenerator : MonoBehaviour
    {
        #region Singleton
        private static RandomEnemyGenerator _instance;
        public static RandomEnemyGenerator Instance { get => _instance; }
        #endregion

        [SerializeField] private GameObject _enemyPrefab;
        private List<int> _spawnedEnemyPositions = new List<int>();

        private void Start()
        {
            RandomTileGenerator.Instance.WhenPathCreated += NewPathCreated;
            _instance = this;
        }

        private void NewPathCreated(int pathLength, int pathStartPosition)
        {
            int _maxEnemyCount = NumberOfEnemiesToSpawn(0, MaxEnemyCountOnOnePath(pathLength));
            for(int i = 0; i < _maxEnemyCount; i++)
            {
                SpawnEnemies(PositionOfEnemiesToSpawn(pathLength, pathStartPosition));
            }
            _spawnedEnemyPositions.Clear();
        }

        private int MaxEnemyCountOnOnePath(int pathLength)
        {
            return (pathLength + 1) / 2;
        }

        private int NumberOfEnemiesToSpawn(int minCount, int maxCount)
        {
            return Random.Range(minCount, maxCount);
        }

        private Vector3Int PositionOfEnemiesToSpawn(int pathLength, int pathStartPosition)
        {
            int randomPosX = Random.Range(pathStartPosition, pathStartPosition + pathLength);
            while (_spawnedEnemyPositions.Contains(randomPosX))
            {
                randomPosX = Random.Range(pathStartPosition, pathStartPosition + pathLength);
            }

            return new Vector3Int(randomPosX, 1, 0);
        }

        private void SpawnEnemies(Vector3Int spawnPosition)
        {
            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}