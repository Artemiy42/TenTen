using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Grid : MonoBehaviour, ISaveable
{
    [SerializeField] private GameObject _slot;
    [SerializeField] private Spawner _spawnTetromino;

    private static readonly int _height = 10;
    private static readonly int _width = 10;

    private GameObject[,] _slots;
    private int[,] _hasBlock;

    private List<int> _lines = new List<int>();
    private List<int> _columns = new List<int>();

    public event UnityAction<int> BlockAdded;
    public event UnityAction<int> ClearLines;

    private void Start()
    {
        _slots = new GameObject[_height, _width];
        _hasBlock = new int[_height, _width];
        CreateGrid();
        SaveLoad.Instance().Load();
    }

    private void OnEnable()
    {
        SaveLoad.Instance().AddToList(this);
    }

    private void CreateGrid()
    {
        for (int y = 0; y < _height; ++y)
        {
            for (int x = 0; x < _width; ++x)
            {
                GameObject tile = Instantiate(_slot, gameObject.transform.position, Quaternion.identity);
                tile.transform.parent = gameObject.transform;

                float posX = x;
                float posY = y;

                tile.transform.position = new Vector2(transform.position.x + posX, transform.position.y + posY);
                _slots[x, y] = tile;
                _hasBlock[x, y] = 0; 
            }
        }
    }

    public void AddTetrominoToGrid(GameObject tetromino)
    {
        foreach (Transform block in tetromino.transform.GetComponentsInChildren<Transform>().Skip(1))
        {
            int roundedX = GetIndexByCoord(block.position.x);
            int roundedY = GetIndexByCoord(block.position.y);

            _slots[roundedX, roundedY].GetComponent<SpriteRenderer>().color = block.gameObject.GetComponent<SpriteRenderer>().color;
            _hasBlock[roundedX, roundedY] = 1;
        }

        tetromino.SetActive(false);
        CheckForLinesAndColumns();
        BlockAdded.Invoke(tetromino.transform.childCount);
    }

    public bool CanAddToGrid(GameObject tetromino)
    {
        foreach (Transform block in tetromino.transform)
        {
            int roundedX = GetIndexByCoord(block.position.x);
            int roundedY = GetIndexByCoord(block.position.y);

            if (roundedX < 0 || roundedX >= _width || roundedY < 0 || roundedY >= _height)
            {
                return false;
            }

            if (_hasBlock[roundedX, roundedY] != 0)
            {
                return false;
            }
        }

        return true;
    }

    private int GetIndexByCoord(float coord)
    {
        int index;

        index = Mathf.RoundToInt(coord);
        
        return index;
    }

    public bool CanAddTetromino(GameObject tetromino)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (CanAddTetrominoByPostion(tetromino.transform, x, y))
                {
                    Debug.Log("Can add tetromino by pos: " + new Vector2(x, y));
                    return true;
                }
            }
        }

        return false;
    }

    private bool CanAddTetrominoByPostion(Transform tetromino, int x, int y)
    {
        foreach (Transform piece in tetromino.transform)
        {
            float posX = x + piece.localPosition.x;
            float posY = y + piece.localPosition.y;
            Debug.Log("x = " + x + " y = " + y);

            int roundedX = GetIndexByCoord(posX);
            int roundedY = GetIndexByCoord(posY);

            if (roundedX < 0 || roundedX >= _width || roundedY < 0 || roundedY >= _height)
            {
                return false;
            }

            if (_hasBlock[roundedX, roundedY] != 0)
            {
                return false;
            }
            
            Debug.Log("Can add x = " + x + " y = " + y + " | piece = " + piece.localPosition + " | posX = " + posX.ToString("f3") + ", posY = " + posY.ToString("f3") + " | IdX = " + roundedX + ", IdY = " + roundedY);
        }

        return true;
    }

    private void CheckForLinesAndColumns()
    {
        for (int i = 0; i < _height; i++)
        {
            if (HasLine(i))
            {
                _lines.Add(i);
                Debug.Log("Find line " + i);
            }

            if (HasColumn(i))
            {
                _columns.Add(i);
                Debug.Log("Find column " + i);
            }
        }

        foreach (int index in _lines)
        {
            DeleteLine(index);
        }

        foreach (int index in _columns)
        {
            DeleteColumn(index);
        }

        ClearLines.Invoke(_lines.Count + _columns.Count);

        _lines.Clear();
        _columns.Clear();
    }

    private bool HasLine(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (_hasBlock[x, y] == 0)
            {
                return false;
            }
        }

        Debug.Log("Find line #" + y);
        return true;
    }

    private bool HasColumn(int x)
    {
        for (int y = 0; y < _height; y++)
        {
            if (_hasBlock[x, y] == 0)
            {
                return false;
            }
        }

        Debug.Log("Find column #" + x);
        return true;
    }

    private void DeleteLine(int y)
    {
        StringBuilder str = new StringBuilder();

        for (int x = 0; x < _width; x++)
        {
            if (_hasBlock[x, y] != 0)
            {
                str.Append("(" + x + "," + y + ") ");
                _slots[x, y].GetComponent<SpriteRenderer>().color = _slot.GetComponent<SpriteRenderer>().color;
                _hasBlock[x, y] = 0;
            }
        }

        Debug.Log("Delete line " + str);
    }

    private void DeleteColumn(int x)
    {
        StringBuilder str = new StringBuilder();

        for (int y = 0; y < _height; y++)
        {
            if (_hasBlock[x, y] != 0)
            {
                str.Append("(" + x + "," + y + ") ");
                _slots[x, y].GetComponent<SpriteRenderer>().color = _slot.GetComponent<SpriteRenderer>().color;
                _hasBlock[x, y] = 0;
            }
        }

        Debug.Log("Delete column " + str);
    }

    public void Save()
    {
        for (int y = 0; y < _width; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Color color = _slots[x, y].GetComponent<SpriteRenderer>().color;
                string strColor = color.r + ";" + color.g + ";" + color.b;
                Debug.Log("Slot" + x + y + " " + strColor);
                PlayerPrefs.SetString("Slot" + x.ToString() + y, strColor);
                PlayerPrefs.SetInt("HasBlock" + x.ToString() + y, _hasBlock[x,y]);
            }
        }
    }

    public void Load()
    {
        for (int y = 0; y < _width; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                string strColor = PlayerPrefs.GetString("Slot" + x.ToString() + y);
                Debug.Log("Slot" + x + y + " " + strColor);
                _slots[x, y].GetComponent<SpriteRenderer>().color = ColorParse.StringRGBToColor(strColor);
                _hasBlock[x, y] = PlayerPrefs.GetInt("HasBlock" + x.ToString() + y);
            }
        }
    }
}
