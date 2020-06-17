using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    [SerializeField] private GameObject[] _tetrominoes;
    [SerializeField] private GameObject[] _spawnSlots;
    [SerializeField] private PauseMenu _pauseMenu;

    private void Start()
    {
        //   _pauseMenu.SaveState += SaveTetromino;
    }

    private void OnDisable()
    {
    //    _pauseMenu.SaveState -= SaveTetromino;
    }

    public void CreateTetrominoes(List<GameObject> liveTetrominoes) 
    {
        for (int i = 0; i < _spawnSlots.Length; i++)
        {
            GameObject tetromino = Instantiate(GetRandomTetromino(), _spawnSlots[i].transform.position, Quaternion.identity);
            tetromino.transform.parent = _spawnSlots[i].transform;
            liveTetrominoes.Add(tetromino);
        }
    }

    // Add more random
    private GameObject GetRandomTetromino()
    {
        return _tetrominoes[Random.Range(0, _tetrominoes.Length)];
    }

    private void SaveTetromino()
    {
    /*    foreach (Transform slot in transform)
        {
            if (slot.childCount != 0)
            {
                PlayerPrefs.SetString("Slot1", "");
            }
            else
            {
                PlayerPrefs.SetString("");
            }
        }*/
    }
}
