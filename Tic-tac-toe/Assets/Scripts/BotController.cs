using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    /// <summary>
    /// Приоритетная клетка для бота
    /// </summary>
    public static CellController priorityCell;

    [SerializeField] bool botCross;

    private void Update()
    {
        BotTurn();
    }

    private void BotTurn()
    {
        if (!GameManager.main.gameOver)
        {
            if (botCross && GameManager.main.crossTurn)
            {
                CalculateCriorityCell();
                GameFieldController.freeCells.Remove(priorityCell);
                priorityCell.cellContent = 1;
                priorityCell.cellAnimator.Play("Cross");
                GameManager.main.crossTurn = false;
            }
            else if (!botCross && GameManager.main.zeroTurn)
            {
                CalculateCriorityCell();
                GameFieldController.freeCells.Remove(priorityCell);
                priorityCell.cellContent = 2;
                priorityCell.cellAnimator.Play("Zero");
                GameManager.main.zeroTurn = false;
            }
        }
    }

    /// <summary>
    /// Расчет приоритетной для бота клетки
    /// </summary>
    private void CalculateCriorityCell()
    {
        priorityCell = null;
        int maxCellPriority = 0;
        List<CellController> priorityCells = new List<CellController>();
        int[] crossesInLine;
        int[] zeroesInLine;
        bool priorityCellFound = false;
        foreach (CellController cell in GameFieldController.freeCells)
        {
            CountCellContent(cell.xID, cell.yID);

            int currentCellPriority = 0;

            for (int i = 0; i < 4; i++)
            {

                if (crossesInLine[i] == GameFieldController.fieldSize - 1)
                {
                    priorityCell = cell;

                    priorityCellFound = true;

                    break;
                }
                else if (zeroesInLine[i] == GameFieldController.fieldSize - 1)
                {
                    priorityCell = cell;

                    priorityCellFound = true;
                    
                    break;
                }
                else
                {
                    if (zeroesInLine[i] == 0)
                    {
                        currentCellPriority += crossesInLine[i];
                    }

                    if (crossesInLine[i] == 0)
                    {
                        currentCellPriority += zeroesInLine[i];
                    }
                }
            }

            if (priorityCellFound)
            {
                break;
            }
            else if (currentCellPriority == maxCellPriority)
            {
                priorityCells.Add(cell);
                priorityCell = priorityCells[Random.Range(0, priorityCells.Count)];
            }
            else if (currentCellPriority > maxCellPriority)
            {
                priorityCells = new List<CellController>();
                priorityCells.Add(cell);
                priorityCell = cell;
                maxCellPriority = currentCellPriority;
            }
        }        

        void CountCellContent(int currentCellXID, int currentCellYID)
        {
            crossesInLine = new int[4];
            zeroesInLine = new int[4];

            for (int i = 0; i < GameFieldController.fieldSize; i++)
            {
                //Горизонтальная линия
                if (GameFieldController.cells[i, currentCellYID].cellContent == 1)
                {
                    crossesInLine[0]++;
                }
                else if (GameFieldController.cells[i, currentCellYID].cellContent == 2)
                {
                    zeroesInLine[0]++;
                }

                //Вертикальная линия
                if (GameFieldController.cells[currentCellXID, i].cellContent == 1)
                {
                    crossesInLine[1]++;
                }
                else if (GameFieldController.cells[currentCellXID, i].cellContent == 2)
                {
                    zeroesInLine[1]++;
                }

                if (currentCellXID == currentCellYID)
                {
                    //Наклонная линия (/)
                    if (GameFieldController.cells[i, i].cellContent == 1)
                    {
                        crossesInLine[2]++;
                    }
                    else if (GameFieldController.cells[i, i].cellContent == 2)
                    {
                        zeroesInLine[2]++;
                    }
                }

                if (GameFieldController.fieldSize - 1 - currentCellXID == currentCellYID)
                {
                    //Наклонная линия (\)
                    if (GameFieldController.cells[GameFieldController.fieldSize - 1 - i, i].cellContent == 1)
                    {
                        crossesInLine[3]++;
                    }
                    else if (GameFieldController.cells[GameFieldController.fieldSize - 1 - i, i].cellContent == 2)
                    {
                        zeroesInLine[3]++;
                    }
                }
            }
        }
    }
}
