using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FRunner
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Camera _playerCamera;

        private void Update()
        {
            _playerCamera.transform.position = new Vector3(_player.transform.position.x + 10f, 5f, -15f);
        }
    }
}
