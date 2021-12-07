using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private Animator cellAnimator;

    /// <summary>
    /// Содержимое клетки (0 - пусто, 1 - крест, 2 - ноль)
    /// </summary>
    public int cellContent;

    /// <summary>
    /// ID по оси X
    /// </summary>
    [HideInInspector] public int xID;

    /// <summary>
    /// ID по оси Y
    /// </summary>
    [HideInInspector] public int yID;

    private void OnMouseDown()
    {
        if (!GameManager.main.gameOver &&                                       //Если игра не закончена
            cellContent == 0 &&                                                 //Если клетка пустая
            GameManager.playerCount > 0 &&                                      //Если в игре есть игроки
            (GameManager.playerCross && GameManager.main.crossTurn) ||          //Если крестики могут ходить
            (!GameManager.playerCross && GameManager.main.zeroTurn))            //Если нолики могут ходить
        {
            SetFigureInCell(GameManager.playerCross);

            CheckPlayerWin();

            if (GameManager.playerCount == 2)
            {
                GameManager.playerCross = !GameManager.playerCross;
            }

            //Убираем клетку из списка свободных
            GameFieldController.main.freeCells.Remove(this);
        }
    }

    /// <summary>
    /// Проверяет условия победы игрока
    /// </summary>
    private void CheckPlayerWin()
    {
        //Количество клеток игрока в линии
        int[] playerCellsInLine = new int[4];

        CountCellContent(xID, yID);

        for (int i = 0; i < 4; i++)
        {
            //Если нашли собранную линию
            if (playerCellsInLine[i] == GameManager.fieldSize)
            {
                if (GameManager.playerCross)
                {
                    GameManager.main.crossWin = true;
                }
                else
                {
                    GameManager.main.zeroWin = true;
                }
            }
        }

        //Считаем содержимое клеток во всех линиях
        void CountCellContent(int currentCellXID, int currentCellYID)
        {
            int targetCellContent;

            //Если игрок ходит крестиками
            if (GameManager.playerCross)
            {
                //Ищем крестики
                targetCellContent = 1;
            }
            else
            {
                //Иначе ищем нолики
                targetCellContent = 2;
            }

            for (int i = 0; i < GameManager.fieldSize; i++)
            {
                //Горизонтальная линия
                if (GameFieldController.main.cells[i, currentCellYID].cellContent == targetCellContent)
                {
                    playerCellsInLine[0]++;
                }

                //Вертикальная линия
                if (GameFieldController.main.cells[currentCellXID, i].cellContent == targetCellContent)
                {
                    playerCellsInLine[1]++;
                }

                if (currentCellXID == currentCellYID)
                {
                    //Наклонная линия (/)
                    if (GameFieldController.main.cells[i, i].cellContent == targetCellContent)
                    {
                        playerCellsInLine[2]++;
                    }
                }

                if (GameManager.fieldSize - 1 - currentCellXID == currentCellYID)
                {
                    //Наклонная линия (\)
                    if (GameFieldController.main.cells[GameManager.fieldSize - 1 - i, i].cellContent == targetCellContent)
                    {
                        playerCellsInLine[3]++;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Размещает содержимое в клетку
    /// </summary>
    public void SetFigureInCell(bool cross)
    {
        //Крестик
        if (cross && GameManager.main.crossTurn)
        {
            cellContent = 1;
            cellAnimator.Play("Cross");
            GameManager.main.crossTurn = false;
        }
        //Нолик
        else if (!cross && GameManager.main.zeroTurn)
        {
            cellContent = 2;
            cellAnimator.Play("Zero");
            GameManager.main.zeroTurn = false;
        }
    }

    /// <summary>
    /// Передает ход нолику
    /// </summary>
    private void PassTurnToZero()
    {
        GameManager.main.zeroTurn = true;
    }

    /// <summary>
    /// Передает ход крестику
    /// </summary>
    private void PassTurnToCross()
    {
        GameManager.main.crossTurn = true;
    }

    /// <summary>
    /// Проверяет условия окончания игры
    /// </summary>
    private void CheckGameEnd()
    {
        if (GameManager.main.crossWin || GameManager.main.zeroWin || GameFieldController.main.freeCells.Count == 0)
        {
            GameManager.main.SetGameEnd();
        }
    }
}
