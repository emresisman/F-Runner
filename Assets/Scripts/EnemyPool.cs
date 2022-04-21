using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class EnemyPool : MonoBehaviour
    {
        #region Singleton
        private static EnemyPool _instance;
        public static EnemyPool Instance { get => _instance; }
        #endregion

        [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
        [SerializeField] private GameObject _enemyPrefab;
        
        private void Start()
        {
            _instance = this;
            FillPool(50);
        }

        public void GetNewEnemy(out GameObject objectWillSpawn)
        {
            objectWillSpawn = RemoveObjectFromPool();
            //UpdateEnemyForPath(objectWillSpawn);
            _enemyList.RemoveAt(0);
        }

        public void AddEnemyToPool(GameObject enemy)
        {
            UpdateEmemyForPool(enemy);
            _enemyList.Add(enemy);
        }

        private void UpdateEmemyForPool(GameObject enemy)
        {
            enemy.SetActive(false);
            enemy.transform.position = this.transform.position;
            enemy.transform.parent = this.transform;
        }

        private void UpdateEnemyForPath(GameObject enemy)
        {
            enemy.SetActive(true);
            enemy.transform.parent = null;
        }

        private void FillPool(int count)
        {
            GameObject enemy;
            for (int i = 0; i < count; i++)
            {
                enemy = Instantiate(_enemyPrefab, Vector3.zero, Quaternion.identity);
                AddEnemyToPool(enemy);
            }
        }

        private GameObject RemoveObjectFromPool()
        {
            if (_enemyList.Count > 0) return _enemyList[0];
            else return null;
        }
    }
}