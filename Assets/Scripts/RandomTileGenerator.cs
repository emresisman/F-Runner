using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace emresisman
{
    public class RandomTileGenerator : MonoBehaviour
    {
        Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
        
        public Tilemap _mainTileMap;
        public Tile _tile;
        int _index = 0;
        

        private void Update()
        {
            SetTile(_tile);
            
        }

        void SetTile(Tile _tile)
        {
            _mainTileMap.SetTile(new Vector3Int(_index++, 0, 0), null);
        }
    }
}

