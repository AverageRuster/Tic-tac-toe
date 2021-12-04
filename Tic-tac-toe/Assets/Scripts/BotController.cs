using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    /// <summary>
    /// Приоритетная клетка для бота
    /// </summary>
    public static CellController priorityCell;

    private void Update()
    {
        BotTurn();
    }

    private void BotTurn()
    {
        if (GameManager.botTurn)
        {
            CalculateCriorityCell();
            priorityCell.cellContent = 2;
            GameFieldController.freeCells.Remove(priorityCell);
            GameManager.botTurn = false;
        }
    }

    /// <summary>
    /// Расчет приоритетной для бота клетки
    /// </summary>
    private void CalculateCriorityCell()
    {
        priorityCell = null;
        int maxCellPriority = 0;

        foreach (CellController cell in GameFieldController.freeCells)
        {
            int[] playerCellsInLine = new int[4];
            int[] botCellsInLine = new int[4];
            CountCellContent(1, 1); //fix


            int currentCellPriority = 0;

            for (int i = 0; i < 4; i++)
            {
                if (botCellsInLine[i] == GameFieldController.fieldSize - 1)
                {
                    priorityCell = cell;
                    break;
                }
                else if (playerCellsInLine[i] == GameFieldController.fieldSize - 1)
                {
                    priorityCell = cell;
                    break;
                }
                else
                {
                    if (playerCellsInLine[i] == 0)
                    {
                        currentCellPriority += botCellsInLine[i];
                    }

                    if (botCellsInLine[i] == 0)
                    {
                        currentCellPriority += playerCellsInLine[i];
                    }
                }
            }

            if (priorityCell != null)
            {
                break;
            }
            else if (currentCellPriority > maxCellPriority)
            {
                priorityCell = cell;
                maxCellPriority = currentCellPriority;
            }
        }

        void CountCellContent(int currentCellXPos, int currentCellYPos)
        {
            for (int i = 0; i < GameFieldController.fieldSize; i ++)
            {
                //Горизонтальная линия
                if (GameFieldController.cells[i, currentCellYPos].cellContent == 1)
                {
                    //playerCellsInLine[0]++;
                }
                else if (GameFieldController.cells[i, currentCellYPos].cellContent == 2)
                {
                    //botCellsInLine[0]++;
                }

                //Вертикальная линия
                if (GameFieldController.cells[currentCellXPos, i].cellContent == 1)
                {
                    //playerCellsInLine[1]++;
                }
                else if (GameFieldController.cells[currentCellXPos, i].cellContent == 2)
                {
                    //botCellsInLine[1]++;
                }

                /*
                //Наклонная линия (/)
                if (GameFieldController.cells[i, i].cellContent == 1)
                {
                    //playerCellsInLine[2]++;
                }
                else if (GameFieldController.cells[i, i].cellContent == 2)
                {
                    //botCellsInLine[2]++;
                }

                //Наклонная линия (\)
                if (GameFieldController.cells[GameFieldController.fieldSize - i, GameFieldController.fieldSize - i].cellContent == 1)
                {
                    //playerCellsInLine[3]++;
                }
                else if (GameFieldController.cells[GameFieldController.fieldSize - i, GameFieldController.fieldSize - i].cellContent == 2)
                {
                    //botCellsInLine[3]++;
                }
                */
            }
        }
    }
}
