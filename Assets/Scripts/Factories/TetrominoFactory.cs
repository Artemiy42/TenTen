using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Factories
{
    public class TetrominoFactory : MonoBehaviour
    {
        [Serializable]
        private class TetrominoPrefab
        {
            public TetrominoType Type;
            public Tetromino Prefab;
        }

        [SerializeField] private List<TetrominoPrefab> _tetrominoList;

        private Dictionary<TetrominoType, Tetromino> _tetrominoes = new Dictionary<TetrominoType, Tetromino>();

        public void Start()
        {
            foreach (TetrominoPrefab tetromino in _tetrominoList)
            {
                _tetrominoes[tetromino.Type] = tetromino.Prefab;
            }
        }

        public Tetromino Get(TetrominoType tetrominoType)
        {
            return Instantiate(_tetrominoes[tetrominoType]);
        }
    }
}