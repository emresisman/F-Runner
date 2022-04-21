using System.Collections;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public enum MovementStates
    {
        Running,
        Jumping,
        Diving
    }

    public class Player : MonoBehaviour
    {
        private float _speed;
        private float _deltaSpeed;
        private MovementStates _movementStates;

        public float Speed { get => _speed; }
        public float DeltaSpeed 
        {
            set
            {
                _deltaSpeed = value;
                UpdateSpeed();
            }
        }

        private void Start()
        {
            DeltaSpeed = 0.02f;
            StartCoroutine(IncreaseSpeed());
        }

        private void Update()
        {
            if (PlayerReachEndOfPath())
            {
                RandomTileGenerator.Instance.CreateNewScreenTiles();
            }
        }

        private void FixedUpdate()
        {
            transform.position = new Vector3(transform.position.x + _deltaSpeed, 0, 0);
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
    }
}