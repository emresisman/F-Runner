using System.Collections;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private void Death()
        {
            EnemyPool.Instance.AddEnemyToPool(this.gameObject);
        }

        private void OnBecameInvisible()
        {
            EnemyPool.Instance.AddEnemyToPool(this.gameObject);
        }
    }
}