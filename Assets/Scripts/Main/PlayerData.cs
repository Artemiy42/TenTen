using UnityEngine;

namespace TenTen
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "TenTen/Create Player Data")]
    public class PlayerData : ScriptableObject
    {
        public int CurrentScore;
        public int BestScore;
    }
}