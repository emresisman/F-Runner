using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FRunner
{
    public delegate void WhenPathCreated(int pathLength, int pathStartPosition);

    public class RandomTileGenerator : MonoBehaviour
    {
        public event WhenPathCreated WhenPathCreated;
        public const int SCREEN_SIZE_TILE_COUNT = 25;

        #region Singleton
                private static RandomTileGenerator _instance;
                public static RandomTileGenerator Instance { get => _instance;}
        #endregion

        public Vector3Int CurrentHorizontalPosition { get => _currentHorizontalPosition; }
        [SerializeField] private Player _player;
        [SerializeField] private Tilemap _mainTileMap;
        [SerializeField] private Tile _tile;

        private int _createdTileCount;
        private Vector3Int _currentHorizontalPosition;
        
        private void Start()
        {
            _instance = this;
            _currentHorizontalPosition = new Vector3Int(0, 0, 0);
            CreateStartingScreenTiles(); 
            StartCoroutine(WaitOneSecond());
        }

        IEnumerator WaitOneSecond()
        {
            yield return new WaitForSeconds(0.1f);
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

        private void CreateStartingScreenTiles()
        {
            SetTile(_tile, 5);
            SetTile(null, 1);
        }


        public void CreateNewScreenTiles()
        {
            _createdTileCount = 0;
            while (_createdTileCount < SCREEN_SIZE_TILE_COUNT)
            {
                _createdTileCount += CreatePath() + CreateSpace();
            }
        }

        private int CreatePath()
        {
            int _pathLength = Random.Range(1, ((int)_player.Speed) * 2 + 1);
            SetTile(_tile, _pathLength);
            WhenPathCreated?.Invoke(_pathLength, (_currentHorizontalPosition.x - _pathLength) * 2);
            return _pathLength;
        }

        private int CreateSpace()
        {
            int _spaceLength = Random.Range(1, ((int)_player.Speed) + 1);
            SetTile(null, _spaceLength);
            return _spaceLength;
        }

    }
}