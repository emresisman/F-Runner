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

        private List<int> _spawnedEnemyPositions = new List<int>();

        private void Start()
        {
            _instance = this;
            RandomTileGenerator.Instance.WhenPathCreated += NewPathCreated;
        }

        public void NewPathCreated(int pathLength, int pathStartPosition)
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
            return Random.Range(minCount, maxCount + 1);
        }

        private Vector3 PositionOfEnemiesToSpawn(int pathLength, int pathStartPosition)
        {
            int randomPosX = Random.Range(pathStartPosition, pathStartPosition + pathLength);
            while (_spawnedEnemyPositions.Contains(randomPosX))
            {
                randomPosX = Random.Range(pathStartPosition, pathStartPosition + pathLength);
            }

            return new Vector3(randomPosX + 0.5f, 2, 0);
        }

        private void SpawnEnemies(Vector3 spawnPosition)
        {
            GameObject spawnedObject;
            EnemyPool.Instance.GetNewEnemy(out spawnedObject);
            spawnedObject.SetActive(true);
            spawnedObject.transform.parent = null;
            spawnedObject.transform.position = spawnPosition;
        }
    }
}