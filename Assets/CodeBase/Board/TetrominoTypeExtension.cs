using System;
using CodeBase.Board;

namespace CodeBase.Themes
{
    public static class TetrominoTypeExtension
    {
        public static ColorType ToColorType(this TetrominoType tetrominoType)
        {
            switch (tetrominoType)
            {
                case TetrominoType.None:
                    return ColorType.Slot;
                case TetrominoType.OneSquare: 
                    return ColorType.OneSquare;
                case TetrominoType.TwoHorizontal: 
                    return ColorType.TwoHorizontal;
                case TetrominoType.TwoVertical: 
                    return ColorType.TwoVertical;
                case TetrominoType.TwoSquare: 
                    return ColorType.TwoSquare;
                case TetrominoType.ThreeHorizontal: 
                    return ColorType.ThreeHorizontal;
                case TetrominoType.ThreeVertical: 
                    return ColorType.ThreeVertical;
                case TetrominoType.ThreeSquare: 
                    return ColorType.ThreeSquare;
                case TetrominoType.FourHorizontal: 
                    return ColorType.FourHorizontal;
                case TetrominoType.FourVertical: 
                    return ColorType.FourVertical;
                case TetrominoType.FiveHorizontal:
                    return ColorType.FiveHorizontal;
                case TetrominoType.FiveVertical:
                    return ColorType.FiveVertical;
                case TetrominoType.BigL1:
                    return ColorType.BigL1;
                case TetrominoType.BigL2:
                    return ColorType.BigL2;
                case TetrominoType.BigL3:
                    return ColorType.BigL3;
                case TetrominoType.BigL4:
                    return ColorType.SmallL4;
                case TetrominoType.SmallL1:
                    return ColorType.SmallL1;
                case TetrominoType.SmallL2:
                    return ColorType.SmallL2;
                case TetrominoType.SmallL3:
                    return ColorType.SmallL3;
                case TetrominoType.SmallL4:
                    return ColorType.SmallL4;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tetrominoType), tetrominoType, null);
            }
        }
    }
}