using System.Linq;
using UnityEngine;

class ColorParse
{
    public static Color StringRGBToColor(string colorInString)
    {
        string[] splitString = colorInString.Split(';');

        float[] splitInts = splitString.Select(item => float.Parse(item)).ToArray();

        return new Color(splitInts[0], splitInts[1], splitInts[2]);
    }
}
