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
        private float _deltaSpeed;
        public float JumpForce = 0.5f;
        public float CollisionOverlapRadius = 0.1f;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _groundLayer;

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
            DeltaSpeed = 0.02f;
            StartCoroutine(IncreaseSpeed());
        }

        private void Update()
        {
            _movementSM.CurrentState.HandleInput();
            _movementSM.CurrentState.LogicUpdate();

            if (PlayerReachEndOfPath())
            {
                RandomTileGenerator.Instance.CreateNewScreenTiles();
            }
        }

        private void FixedUpdate()
        {
            _movementSM.CurrentState.PhysicsUpdate();
        }

        private void UpdateSpeed()
        {
            _speed = _deltaSpeed * 50;
        }

        IEnumerator IncreaseSpeed()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                DeltaSpeed = _deltaSpeed + 0.01f;
            }
        }

        private bool PlayerReachEndOfPath()
        {
            if(Vector3.Distance(transform.position, RandomTileGenerator.Instance.CurrentHorizontalPosition) < 40f)
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
            return Physics2D.OverlapCircleAll(point, 0.1f, _groundLayer).Length > 0;
        }
    }
}