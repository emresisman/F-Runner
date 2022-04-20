using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace emresisman.Assets.Scripts
{
    public delegate void WhenPathCreated(int pathLength, int pathStartPosition);

    public class RandomTileGenerator : MonoBehaviour
    {
        #region Singleton
                private static RandomTileGenerator _instance;
                public static RandomTileGenerator Instance { get => _instance;}
        #endregion

        public event WhenPathCreated WhenPathCreated;

        public const int SCREEN_SIZE_TILE_COUNT = 50;
        private int _createdTileCount;

        private float _playerSpeed;
        private Vector3Int _currentHorizontalPosition;
        
        public Tilemap _mainTileMap;
        public Tile _tile;

        private void Start()
        {
            _playerSpeed = 2.8f;
            _instance = this;
            _currentHorizontalPosition = new Vector3Int(0, 0, 0);
            CreateNewScreenTiles();
        }

        void SetTile(Tile _tile, int _tileLength)
        {
            for (int i = 0; i < _tileLength; i++)
            {
                _mainTileMap.SetTile(_currentHorizontalPosition, _tile);
                GoNextPosition();
            }
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
            int _pathLength = Random.Range(1, ((int)_playerSpeed) * 5 + 1);
            SetTile(_tile, _pathLength);
            WhenPathCreated.Invoke(_pathLength, _currentHorizontalPosition.x);
            return _pathLength;
        }

        private int CreateSpace()
        {
            int _spaceLength = Random.Range(1, ((int)_playerSpeed) + 1);
            SetTile(null, _spaceLength);
            return _spaceLength;
        }

    }
}