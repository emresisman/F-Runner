using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace emresisman
{
    public class RandomTileGenerator : MonoBehaviour
    {
        public const int SCREEN_SIZE_TILE_COUNT = 30;
        private int _createdTileCount;

        private float _playerSpeed;
        private Vector3Int _currentHorizontalPosition;
        
        public Tilemap _mainTileMap;
        public Tile _tile;


        private void Start()
        {
            _currentHorizontalPosition = new Vector3Int(0, 0, 0);
        }

        void SetTile(Tile _tile)
        {
            _mainTileMap.SetTile(_currentHorizontalPosition, _tile);
            GoNextPosition();
        }

        private void GoNextPosition()
        {
            _currentHorizontalPosition =
                new Vector3Int(_currentHorizontalPosition.x + 1, 0, 0);
                
        }

        private void CreateNewScreenTiles()
        {
            _createdTileCount = 0;
            while (_createdTileCount < SCREEN_SIZE_TILE_COUNT)
            {
                _createdTileCount += CreatePath() + CreateSpace();
            }
        }

        private int CreatePath()
        {
            int i = Random.Range(1, ((int)_playerSpeed) * 5);
            for (int k = 0; k < i; k++)
            {
                SetTile(_tile);
            }
            return i;
        }

        private int CreateSpace()
        {
            int i = Random.Range(1, ((int)_playerSpeed));
            for (int k = 0; k < i; k++)
            {
                SetTile(null);
            }
            return i;
        }

    }
}