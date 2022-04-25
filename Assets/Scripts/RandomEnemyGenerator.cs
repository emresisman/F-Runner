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
            int randomPosX;
            do
            {
                randomPosX = Random.Range(pathStartPosition, pathStartPosition + (pathLength * 2));
            } while (!IsOddNumber(randomPosX) || _spawnedEnemyPositions.Contains(randomPosX));
            _spawnedEnemyPositions.Add(randomPosX);
            return new Vector3(randomPosX, 2, 0);
        }

        private bool IsOddNumber(int number)
        {
            return number % 2 == 1 ? true : false;
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