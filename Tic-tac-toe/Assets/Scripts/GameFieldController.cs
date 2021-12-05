using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    /// <summary>
    ///  Размер поля (3 - стандартный, 5 - максимальный)
    /// </summary>
    public static int fieldSize = 3;

    /// <summary>
    /// Клетка
    /// </summary>
    [SerializeField] GameObject cell;

    /// <summary>
    /// Все клетки
    /// </summary>
    public static CellController[,] cells;

    /// <summary>
    /// Свободные клетки
    /// </summary>
    public static List<CellController> freeCells;

   

    private void Start()
    {
        freeCells = new List<CellController>();
        cells = new CellController[fieldSize, fieldSize];
        SpawnFigures();
    }

    private void SpawnFigures()
    {
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                Vector3 cellSpawnPosition = new Vector3(i - 1, j - 1); //fix
                GameObject currentCell = Instantiate(cell, cellSpawnPosition, cell.transform.rotation);
                cells[i, j] = currentCell.GetComponent<CellController>();
                cells[i, j].xID = i;
                cells[i, j].yID = j;
                freeCells.Add(cells[i, j]);
            }
        }       
    }
}
