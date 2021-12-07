using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    public static GameFieldController main;

    /// <summary>
    /// Клетка
    /// </summary>
    [SerializeField] private GameObject cell;

    /// <summary>
    /// Все клетки
    /// </summary>
    public CellController[,] cells = new CellController[GameManager.fieldSize, GameManager.fieldSize];

    /// <summary>
    /// Свободные клетки
    /// </summary>
    public List<CellController> freeCells = new List<CellController>();

    private void Start()
    {
        main = this;
        SpawnCells();
    }

    /// <summary>
    /// Создает поле
    /// </summary>
    private void SpawnCells()
    {
        for (int i = 0; i < GameManager.fieldSize; i++)
        {
            for (int j = 0; j < GameManager.fieldSize; j++)
            {
                //Крайняя точка спавна клетки
                float border = (float)(GameManager.fieldSize - 1) / 2;

                //Указываем позицию спавна текущей клетки
                Vector3 cellSpawnPosition = new Vector3(i - border, j - border);

                //Создаем клетку
                GameObject currentCell = Instantiate(cell, cellSpawnPosition, cell.transform.rotation);

                //Добавляем ее в массив клеток
                cells[i, j] = currentCell.GetComponent<CellController>();

                //Запоминаем ее ID
                cells[i, j].xID = i;
                cells[i, j].yID = j;

                //Добавляем ее в список пустых клеток
                freeCells.Add(cells[i, j]);
            }
        }       
    }
}
