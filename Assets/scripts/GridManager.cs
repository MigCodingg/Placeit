using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

[System.Serializable]
    public class BoxData
    {
    public Vector2 position;
    public int maxPushes;
    }
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private Vector2 _playerStartPos = new Vector2(0, 0);

    [SerializeField] private List<BoxData> _boxesData;

    private Dictionary<Vector2, Tile> _tiles;
    private Dictionary<Vector2, GameObject> _boxes;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        _boxes = new Dictionary<Vector2, GameObject>();

        // 🔲 Generate tiles
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

       foreach (var boxData in _boxesData)
        {
            SpawnBox(boxData.position, boxData.maxPushes);
        }

        // 🧍 Spawn player
        Instantiate(_playerPrefab, _playerStartPos, Quaternion.identity);
    }

    void SpawnBox(Vector2 pos, int maxPushes)
        {
    var boxObj = Instantiate(_boxPrefab, pos, Quaternion.identity);

    var box = boxObj.GetComponent<Box>();
    box.SetMaxPushes(maxPushes);

    _boxes[pos] = boxObj;
        }

    // 🔍 Tile checks
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    // 📦 Box checks
    public bool HasBoxAtPosition(Vector2 pos)
    {
        return _boxes.ContainsKey(pos);
    }

    public GameObject GetBoxAtPosition(Vector2 pos)
    {
        if (_boxes.TryGetValue(pos, out var box)) return box;
        return null;
    }

    public void MoveBox(Vector2 from, Vector2 to)
    {
        if (_boxes.TryGetValue(from, out var box))
        {
            _boxes.Remove(from);
            _boxes[to] = box;
        }
    }
}