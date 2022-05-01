using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman.Assets.Scripts
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Camera _playerCamera;

        private void Update()
        {
            _playerCamera.transform.position = new Vector3(_player.transform.position.x + 15f, 5f, -20f);
        }
    }
}
