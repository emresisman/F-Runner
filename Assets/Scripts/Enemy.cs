using System.Collections;
using UnityEngine;

namespace FRunner
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Death()
        {
            _animator.SetBool("Death", true);
            gameObject.layer = 2;
        }

        private void OnBecameInvisible()
        {
            EnemyPool.Instance.AddEnemyToPool(this.gameObject);
        }

        public void DeathEvent()
        {
            EnemyPool.Instance.AddEnemyToPool(this.gameObject);
        }
    }
}