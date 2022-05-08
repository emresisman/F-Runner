using System.Collections;
using UnityEngine;
using FRunner.States;
using FRunner.Enemies;

namespace FRunner
{
    public class Player : MonoBehaviour
    {
        public StateMachine _movementSM;
        public RunningState _running;
        public DivingState _diving;
        public JumpingState _jumping;
        public DeathState _death;

        private bool _isDead = false;
        private float _speed;
        private float _runningSpeed = 0.7f;
        private float _deltaSpeed;
        private float _normalGravity = 2f;
        private float _fallGravity = 4f;
        private Vector2 _playerVelocity;
        public float JumpForce = 5.5f;
        public float DiveForce = 10f;
        public float CollisionOverlapRadius = 0.1f;

        [SerializeField] private Vector2 _capsuleSize;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Animator _animator;
        
        public float Speed { get => _speed; }
        
        public Vector2 PlayerVelocity 
        {
            get 
            { 
                return _playerVelocity;
            }
            set
            {
                _playerVelocity = value;
                UpdateVelocity();
            }
        }
        
        public float DeltaSpeed 
        {
            get
            {
                return _deltaSpeed;
            }
            set
            {
                _deltaSpeed = value;
                UpdateSpeed();
            }
        }

        private void Start()
        {
            _movementSM = new StateMachine();

            _running = new RunningState(this, _movementSM);
            _jumping = new JumpingState(this, _movementSM);
            _diving = new DivingState(this, _movementSM);
            _death =  new DeathState(this, _movementSM);

            _movementSM.Initialize(_running);
            DeltaSpeed = 0.03f;
            StartCoroutine(IncreaseSpeed());
        }

        private void Update()
        {
            _movementSM.CurrentState.HandleInput();
            _movementSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _movementSM.CurrentState.PhysicsUpdate();
        }

        private void UpdateSpeed()
        {
            _speed = _deltaSpeed * 50;
            _runningSpeed = 0.7f + _deltaSpeed * 2;
            _animator.SetFloat("RunningSpeed", Mathf.Clamp(_runningSpeed, 0.7f, 2f));
        }

        private void UpdateVelocity()
        {
            _rigidbody.velocity = _playerVelocity;
            if (_rigidbody.velocity.y >= 0)
            {
                SwitchJumpGravity();
            }
            else
            {
                SwitchFallGravity();
            }
        }

        IEnumerator IncreaseSpeed()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                DeltaSpeed = _deltaSpeed + 0.01f;
            }
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapBoxAll(transform.position, _capsuleSize, 0, _groundLayer).Length > 0;
        }

        public bool PlayerReachEndOfPath()
        {
            if(Vector3.Distance(transform.position, RandomTileGenerator.Instance.CurrentHorizontalPosition * 2) < 40f)
            {
                return true;
            }
            return false;
        }

        public void ApplyDiveForce(Vector2 force)
        {
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        public void ApplyImpulse()
        {
            PlayerVelocity = Vector2.up * JumpForce;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //if gameobject layer is InteractableEnemy
            if (collision.gameObject.layer == 6 && !_isDead)
            {
                if (isEnemyUnderThePlayer(collision.GetContact(0).normal))
                {
                    collision.gameObject.GetComponent<Enemy>().Death();
                    ResetVelocity();
                    _movementSM.ChangeState(_jumping);
                }
                else
                {
                    _animator.SetTrigger("Death");
                }
            }
        }

        private void ResetVelocity()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private bool isEnemyUnderThePlayer(Vector2 point)
        {
            if (point.y >= 0.95f) return true;
            else return false;
        }

        public void SetAnimationBool(int param, bool value)
        {
            _animator.SetBool(param, value);
        }

        public void SwitchJumpGravity()
        {
            _rigidbody.gravityScale = _normalGravity;
        }

        public void SwitchFallGravity()
        {
            _rigidbody.gravityScale = _fallGravity;
        }

        public void Die()
        {
            _isDead = true;
            _movementSM.ChangeState(_death);
        }
    }
}