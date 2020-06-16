using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{
    [SerializeField] private GameObject[] _tetrominoes;
    [SerializeField] private int[] _percent;
    [SerializeField] private int _max;
    [SerializeField] private PauseMenu _pauseMenu;

    void Start()
    {
        CreateTetrominoes();
    //   _pauseMenu.SaveState += SaveTetromino;
    }

    void Update()
    {
        if (!hasTetrominoes())
        {
            CreateTetrominoes();
        }
    }

    private void OnDisable()
    {
    //    _pauseMenu.SaveState -= SaveTetromino;
    }

    private void CreateTetrominoes() 
    {
        foreach (Transform slot in transform)
        {
            GameObject tetromino = Instantiate(GetRandomTetromino(), slot.position, Quaternion.identity);
            tetromino.transform.Rotate(Vector3.forward, Random.Range(0, 4) * 90);
            tetromino.transform.parent = slot;
        }
    }

    // Add more random
    private GameObject GetRandomTetromino()
    {
        /*int percent = Random.Range(0, _max);
        
        for (int i = 0; i < _tetrominoes.Length; i++)
        {
            if (percent <= _percent[i])
            {
                return _tetrominoes[i];
            }
        }*/

        return _tetrominoes[Random.Range(0, _tetrominoes.Length)];
    }

    private bool hasTetrominoes()
    {
        foreach (Transform slot in transform)
        {
            if (slot.childCount != 0)
            {
                return true;
            }
        }

        return false;
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
