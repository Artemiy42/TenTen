using UnityEngine;

namespace CodeBase.Themes
{
    [CreateAssetMenu(fileName = "Theme Data", menuName = "Theme/Create new theme data")]
    public class ThemeData : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<ColorType, Color> _colors;
        
        public Color GetColor(ColorType colorType)
        {
            return _colors[colorType];
        }
    }
}