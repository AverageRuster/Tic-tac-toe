using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public bool crossTurn = true;
    public bool zeroTurn = false;

    public bool crossWin = false;
    public bool zeroWin = false;
    public bool gameOver = false;

    [SerializeField] GameObject botCross;

    private string result;

    private void Start()
    {
        main = this;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            CheckGameEnd();
        }
    }

    void CheckGameEnd()
    {
        if (!gameOver)
        {
            if (crossWin)
            {
                gameOver = true;

                result = "Cross Wins";
            }
            else if (zeroWin)
            {
                gameOver = true;

                result = "Zero Wins";
            }
            else if (GameFieldController.freeCells.Count == 0)
            {
                gameOver = true;

                result = "Tie";
            }
        }
    }

    public void StartPVP()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void StartPVB()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void StartBVB()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
