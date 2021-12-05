using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public Animator cellAnimator;
    /// <summary>
    /// Содержимое клетки (0 - пусто, 1 - крест, 2 - ноль)
    /// </summary>
    public int cellContent;
    public int xID;
    public int yID;

    private void Start()
    {
        cellAnimator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (!GameManager.main.gameOver && GameManager.main.crossTurn && cellContent == 0)
        {            
            CheckPlayerWin();
            cellContent = 1;
            GameFieldController.freeCells.Remove(this);
            cellAnimator.Play("Cross");
            GameManager.main.crossTurn = false;
        }
    }

    private void CheckPlayerWin()
    {
        int[] playerCellsInLine = new int[4];

        CountCellContent(xID, yID);

        for (int i = 0; i < 4; i++)
        {
            if (playerCellsInLine[i] == GameFieldController.fieldSize - 1)
            {
                GameManager.main.crossWin = true; //fix
            }
        }

        void CountCellContent(int currentCellXID, int currentCellYID)
        {
            for (int i = 0; i < GameFieldController.fieldSize; i++)
            {
                //Горизонтальная линия
                if (GameFieldController.cells[i, currentCellYID].cellContent == 1)
                {
                    playerCellsInLine[0]++;
                }

                //Вертикальная линия
                if (GameFieldController.cells[currentCellXID, i].cellContent == 1)
                {
                    playerCellsInLine[1]++;
                }

                if (currentCellXID == currentCellYID)
                {
                    //Наклонная линия (/)
                    if (GameFieldController.cells[i, i].cellContent == 1)
                    {
                        playerCellsInLine[2]++;
                    }
                }

                if (GameFieldController.fieldSize - 1 - currentCellXID == currentCellYID)
                {
                    //Наклонная линия (\)
                    if (GameFieldController.cells[GameFieldController.fieldSize - 1 - i, i].cellContent == 1)
                    {
                        playerCellsInLine[3]++;
                    }
                }
            }
        }
    }

    private void PassTurnToZero()
    {
        GameManager.main.zeroTurn = true;
    }

    private void PassTurnToCross()
    {
        GameManager.main.crossTurn = true;
    }
}
