using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GridManager : MonoBehaviour
{

[System.Serializable]
    public class BoxData
    {
    public Vector2 position;
    public int maxPushes;
    public Color color;
    }
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Color color;
    [SerializeField] private GameObject _boxPrefab;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private List<Vector2> _trapPositions;
    [SerializeField] private GameObject _trapPrefab;
    [SerializeField] private Vector2 _playerStartPos = new Vector2(0, 0);

    [SerializeField] private List<BoxData> _boxesData;

    [SerializeField] private List<GoalData> _goalsData;
    [SerializeField] private Color _goalColor = Color.green;

    private Dictionary<Vector2, Tile> _tiles;
    private Dictionary<Vector2, GameObject> _boxes;

        [System.Serializable]
    public class GoalData
    {
        public Vector2 position;
        public Color color;
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        _boxes = new Dictionary<Vector2, GameObject>();

        // 🕳 Spawn traps
        foreach (var pos in _trapPositions)
        {
            if (_trapPrefab != null)
                Instantiate(_trapPrefab, pos, Quaternion.identity);
        }

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

        
        foreach (var goal in _goalsData)
    {
        if (_tiles.TryGetValue(goal.position, out var tile))
        {
            tile.SetColor(goal.color);
        }
    }

        // 📦 Spawn boxes
        foreach (var boxData in _boxesData)
        {
            SpawnBox(boxData.position, boxData.maxPushes, boxData.color);
        }

        // 🧍 Spawn player
        Instantiate(_playerPrefab, _playerStartPos, Quaternion.identity);
    }

    void SpawnBox(Vector2 pos, int maxPushes, Color color)
    {
        var boxObj = Instantiate(_boxPrefab, pos, Quaternion.identity);

        var box = boxObj.GetComponent<Box>();
        box.SetMaxPushes(maxPushes);
        box.SetColor(color);

        _boxes[pos] = boxObj;
    }


    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public bool HasBoxAtPosition(Vector2 pos)
    {
        return _boxes.ContainsKey(pos);
    }

    public GameObject GetBoxAtPosition(Vector2 pos)
    {
        if (_boxes.TryGetValue(pos, out var box)) return box;
        return null;
    }

    public bool IsTrapPosition(Vector2 pos)
    {
        return _trapPositions.Contains(pos);
    }

        public bool AreAllGoalsFilled()
    {
        foreach (var goal in _goalsData)
        {
            if (!_boxes.ContainsKey(goal.position))
                return false;
        }
        return true;
    }
        public void MoveBox(Vector2 from, Vector2 to)
    {
        if (_boxes.TryGetValue(from, out var box))
        {
            _boxes.Remove(from);

            // 🕳 Trap check
            if (IsTrapPosition(to))
            {
                Destroy(box);
            }
            else
            {
                _boxes[to] = box;
            }

           
            if (AreAllGoalsFilled())
            {
                LoadNextScene();
            }
        }
    }
        void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}