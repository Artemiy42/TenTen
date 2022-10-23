using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TenTen;

public class JsonConverterTest
{
    [Test]
    public void WhenSerializeCell_AndDeserializeCell_ThenCellsNeedBeEquals()
    {
        // Arrange.
        var cell = new Cell();
        cell.TetrominoType = TetrominoType.BigL1;

        // Act.
        string serializeCell = JsonConvert.SerializeObject(cell);
        var deserializeCell = JsonConvert.DeserializeObject<Cell>(serializeCell);
        
        // Assert.
        Assert.AreEqual(cell.TetrominoType, deserializeCell.TetrominoType);
    }

    [Test]
    public void WhenSerializeCellArray_AndDeserializeBack_ThenArraysNeedBeEquals()
    {
        // Arrange.
        var cellArray = new Cell[] {new(), new(), new()};
        cellArray[0].TetrominoType = TetrominoType.BigL1;
        cellArray[1].TetrominoType = TetrominoType.BigL2;
        cellArray[2].TetrominoType = TetrominoType.BigL3;
        
        // Act.
        string serializeCells = JsonConvert.SerializeObject(cellArray);
        var deserializeCells = JsonConvert.DeserializeObject<Cell[]>(serializeCells);

        // Assert.
        if (deserializeCells == null)
        {
            Assert.Fail("DeserializeCells == null");
        }

        for (int i = 0; i < deserializeCells.Length; i++)
        {
            var cell = cellArray[i];
            var deserializeCell = deserializeCells[i];
            Assert.AreEqual(cell.TetrominoType, deserializeCell.TetrominoType);
        }
    }

    [Test]
    public void WhenSerializeICellArrayWithCellType_AndDeserializeBack_ThenArraysNeedBeEquals()
    {
        // Arrange.
        var cellArray = new List<Cell> {new Cell(), new Cell(), new Cell()};
        cellArray[0].TetrominoType = TetrominoType.BigL1;
        cellArray[1].TetrominoType = TetrominoType.BigL2;
        cellArray[2].TetrominoType = TetrominoType.BigL3;
        
        // Act.
        string serializeCells = JsonConvert.SerializeObject(cellArray);
        var deserializeCells = JsonConvert.DeserializeObject<List<Cell>>(serializeCells);

        // Assert.
        if (deserializeCells == null)
        {
            Assert.Fail("DeserializeCells == null");
        }

        for (int i = 0; i < deserializeCells.Count; i++)
        {
            var cell = cellArray[i];
            var deserializeCell = deserializeCells[i];
            Assert.AreEqual(cell.TetrominoType, deserializeCell.TetrominoType);
        }
    }
    
    [Test]
    public void WhenSerializeBoard_AndDeserializeBack_ThenBoardsNeedBeEquals()
    {
        // Arrange.
        Board<Cell> board = new Board<Cell>(3, 1);
        board[0, 0].TetrominoType = TetrominoType.BigL1;
        board[1, 0].TetrominoType = TetrominoType.BigL2;
        board[2, 0].TetrominoType = TetrominoType.BigL3;
        
        // Act.
        string serializeBoard = JsonConvert.SerializeObject(board);
        var deserializeBoard = JsonConvert.DeserializeObject<Board<Cell>>(serializeBoard);

        // Assert.
        if (deserializeBoard == null)
        {
            Assert.Fail("deserializeBoard == null");
        }

        for (int i = 0; i < deserializeBoard.Width; i++)
        {
            var cell = board[i, 0];
            var deserializeCell = board[i, 0];
            Assert.AreEqual(cell.TetrominoType, deserializeCell.TetrominoType);
        }
    }
}
