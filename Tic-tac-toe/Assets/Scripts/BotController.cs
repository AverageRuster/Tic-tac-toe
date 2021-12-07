using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    /// <summary>
    /// Приоритетная клетка для бота
    /// </summary>
    public static CellController priorityCell;

    /// <summary>
    /// Бот ходит крестиками
    /// </summary>
    [SerializeField] private bool botCross;

    private void Update()
    {
        BotTurn();
    }

    /// <summary>
    /// Ход бота
    /// </summary>
    private void BotTurn()
    {
        if (!GameManager.main.gameOver)
        {
            if ((botCross && GameManager.main.crossTurn) || (!botCross && GameManager.main.zeroTurn))
            {
                CalculatePriorityCell();
                priorityCell.SetFigureInCell(botCross);

                //Удаляем клетку из списка свободных
                GameFieldController.main.freeCells.Remove(priorityCell);
            }
        }
    }

    /// <summary>
    /// Расчет приоритетной для бота клетки
    /// </summary>
    private void CalculatePriorityCell()
    {
        priorityCell = null;

        //Наибольший приоритет клетки (для сравнения)
        int maxCellPriority = 0;

        //Список приоритетных клеток
        List<CellController> priorityCells = new List<CellController>();

        //Количество крестиков в линии
        int[] crossesInLine;

        //Количество ноликов в линии
        int[] zeroesInLine;

        //Найдена ли приоритетная клетка
        bool priorityCellFound = false;

        //Приоритет для предотвращения победы соперника
        CellController preventionPriorityCell = null;

        //Перебираем все пустые клетки
        foreach (CellController cell in GameFieldController.main.freeCells)
        {
            CountCellContent(cell.xID, cell.yID);

            //Приоритет текущей клетки
            int currentCellPriority = 0;

            for (int i = 0; i < 4; i++)
            {
                //Содержимое бота
                int botContentInLine;
                
                //Содержимое соперника
                int enemyContentInLine;

                if (botCross)
                {
                    botContentInLine = crossesInLine[i];
                    enemyContentInLine = zeroesInLine[i];
                }
                else
                {
                    botContentInLine = zeroesInLine[i];
                    enemyContentInLine = crossesInLine[i];
                }

                //Если у бота собралась комбинация
                if (botContentInLine == GameManager.fieldSize - 1)
                {
                    priorityCell = cell;
                    priorityCellFound = true;

                    //Ставим ему победу
                    if (botCross)
                    {
                        GameManager.main.crossWin = true;
                    }
                    else
                    {
                        GameManager.main.zeroWin = true;
                    }

                    //Завершаем перебор
                    break;
                }

                //Если у соперника собирается комбинация
                else if (enemyContentInLine == GameManager.fieldSize - 1)
                {
                    preventionPriorityCell = cell;
                }

                //Иначе считаем приоритет
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

            //Если найдена приоритетная клетка
            if (priorityCellFound)
            {
                //Завершаем перебор
                break;
            }
            else if (preventionPriorityCell != null)
            {
                priorityCell = preventionPriorityCell;
            }

            //Иначе если текущий приоритет равен максимальному
            else if (currentCellPriority == maxCellPriority)
            {
                //Добавляем клетку в список
                priorityCells.Add(cell);

                //Выбираем случайную клетку из списка
                priorityCell = priorityCells[Random.Range(0, priorityCells.Count)];
            }

            //Иначе если текущий приоритет больше максимального
            else if (currentCellPriority > maxCellPriority)
            {
                //Очищаем список
                priorityCells = new List<CellController>();

                //Добавляем в него текущую клетку
                priorityCells.Add(cell);
                
                //Назначаем ее приоритетной
                priorityCell = cell;

                //Меняем максимальный приоритет
                maxCellPriority = currentCellPriority;
            }
        }

        //Считаем содержимое в линиях
        void CountCellContent(int currentCellXID, int currentCellYID)
        {
            //Крестики в линии
            crossesInLine = new int[4];

            //Нолики в линии
            zeroesInLine = new int[4];

            //Проходимся по линиям
            for (int i = 0; i < GameManager.fieldSize; i++)
            {
                //Горизонтальная линия
                if (GameFieldController.main.cells[i, currentCellYID].cellContent == 1)
                {
                    crossesInLine[0]++;
                }
                else if (GameFieldController.main.cells[i, currentCellYID].cellContent == 2)
                {
                    zeroesInLine[0]++;
                }

                //Вертикальная линия
                if (GameFieldController.main.cells[currentCellXID, i].cellContent == 1)
                {
                    crossesInLine[1]++;
                }
                else if (GameFieldController.main.cells[currentCellXID, i].cellContent == 2)
                {
                    zeroesInLine[1]++;
                }

                if (currentCellXID == currentCellYID)
                {
                    //Наклонная линия (/)
                    if (GameFieldController.main.cells[i, i].cellContent == 1)
                    {
                        crossesInLine[2]++;
                    }
                    else if (GameFieldController.main.cells[i, i].cellContent == 2)
                    {
                        zeroesInLine[2]++;
                    }
                }

                if (GameManager.fieldSize - 1 - currentCellXID == currentCellYID)
                {
                    //Наклонная линия (\)
                    if (GameFieldController.main.cells[GameManager.fieldSize - 1 - i, i].cellContent == 1)
                    {
                        crossesInLine[3]++;
                    }
                    else if (GameFieldController.main.cells[GameManager.fieldSize - 1 - i, i].cellContent == 2)
                    {
                        zeroesInLine[3]++;
                    }
                }
            }
        }
    }
}
