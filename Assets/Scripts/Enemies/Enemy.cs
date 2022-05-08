using System.Collections;
using UnityEngine;

namespace FRunner.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CapsuleCollider2D _enemyCollider;

        private void OnEnable()
        {
            _enemyCollider.enabled = true;
        }

        public void Death()
        {
            _animator.SetBool("Death", true);
            _enemyCollider.enabled = false;
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