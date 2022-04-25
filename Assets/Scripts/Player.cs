using System.Collections;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        public StateMachine _movementSM;
        public RunningState _running;
        public DivingState _diving;
        public JumpingState _jumping;

        private float _speed;
        private float _runningSpeed = 0.7f;
        private float _deltaSpeed;
        public float JumpForce = 5.5f;
        public float DiveForce = 10f;
        public float CollisionOverlapRadius = 0.1f;

        [SerializeField] private Vector2 _capsuleSize;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Animator _animator;

        public float Speed { get => _speed; }
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

        IEnumerator IncreaseSpeed()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                DeltaSpeed = _deltaSpeed + 0.01f;
            }
        }

        public bool PlayerReachEndOfPath()
        {
            if(Vector3.Distance(transform.position, RandomTileGenerator.Instance.CurrentHorizontalPosition) < 20f)
            {
                return true;
            }
            return false;
        }

        public void ApplyImpulse(Vector2 force)
        {
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        public bool CheckCollisionOverlap(Vector2 point)
        {
            return Physics2D.OverlapCapsuleAll(point, _capsuleSize, CapsuleDirection2D.Vertical, 0, _groundLayer).Length > 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log(collision.GetContact(0).normal);
                if (isEnemyUnderThePlayer(collision.GetContact(0).normal))
                {
                    collision.gameObject.GetComponent<Enemy>().Death();
                    ResetVelocity();
                    _movementSM.ChangeState(_jumping);
                }
            }
        }

        private void ResetVelocity()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private bool isEnemyUnderThePlayer(Vector2 point)
        {
            if (point.y >= 0.8f) return true;
            else return false;
        }

        public void SetAnimationBool(int param, bool value)
        {
            _animator.SetBool(param, value);
        }
    }
}