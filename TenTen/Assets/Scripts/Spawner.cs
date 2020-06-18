using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _tetrominoes;
    [SerializeField] private GameObject[] _spawnSlots;

    public void CreateTetrominoes(List<GameObject> liveTetrominoes) 
    {
        for (int i = 0; i < _spawnSlots.Length; i++)
        {
            GameObject tetromino = Instantiate(GetRandomTetromino(), _spawnSlots[i].transform.position, Quaternion.identity);
            tetromino.transform.parent = _spawnSlots[i].transform;
            liveTetrominoes.Add(tetromino);
        }
    }

    public GameObject CreateTetrominoByName(string name, int spawnNumber)
    {
        GameObject tetromino = null;

        for (int i = 0; i < _tetrominoes.Length; i++)
        {
            if (_tetrominoes[i].name == name)
            {
                tetromino = Instantiate(_tetrominoes[i], _spawnSlots[spawnNumber].transform.position, Quaternion.identity);
                tetromino.transform.parent = _spawnSlots[spawnNumber].transform;
                break;
            }
        }

        return tetromino;
    }

    private GameObject GetRandomTetromino()
    {
        return _tetrominoes[Random.Range(0, _tetrominoes.Length)];
    }
}
