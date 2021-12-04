using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    Animator cellAnimator;
    public int cellContent; 

    private void Start()
    {
        cellAnimator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (GameManager.playerTurn && cellContent == 0)
        {
            GameManager.playerTurn = false;
            cellContent = 1;
            GameFieldController.freeCells.Remove(this);
            cellAnimator.Play("Cross");
        }
    }
}
