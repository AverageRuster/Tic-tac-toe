using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    /// <summary>
    ///  Размер поля (3 - стандартный, 5 - максимальный)
    /// </summary>
    public static int fieldSize = 3;

    /// <summary>
    /// Количество игроков
    /// </summary>
    public static int playerCount = 1;

    /// <summary>
    /// Ход крестиков
    /// </summary>
    public bool crossTurn = true;
    
    /// <summary>
    /// Ход ноликов
    /// </summary>
    public bool zeroTurn = false;

    /// <summary>
    /// Победа крестиков
    /// </summary>
    public bool crossWin = false;

    /// <summary>
    /// Побуда ноликов
    /// </summary>
    public bool zeroWin = false;

    /// <summary>
    /// Конец игры
    /// </summary>
    public bool gameOver = false;

    /// <summary>
    /// Игрок играет крестиками
    /// </summary>
    public static bool playerCross = true;

    /// <summary>
    /// Включать ли бота с ноликами
    /// </summary>
    public static bool botCrossActive = false;

    /// <summary>
    /// Включать ли бота с крестиками
    /// </summary>
    public static bool botZeroActive = false;

    /// <summary>
    /// Бот с крестиками
    /// </summary>
    [SerializeField] private GameObject botCross;

    /// <summary>
    /// Бот с ноликами
    /// </summary>
    [SerializeField] private GameObject botZero;

    /// <summary>
    /// Внутриигровое меню
    /// </summary>
    [SerializeField] private GameObject gameplayMenu;

    /// <summary>
    /// Результат игры
    /// </summary>
    [SerializeField] private TextMeshProUGUI resultText;

    private void Start()
    {
        main = this;

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            fieldSize = 3;
            playerCount = 1;
            playerCross = true;
            botCrossActive = false;
            botZeroActive = false;
        }

        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            //Включаем ботов
            if (botCrossActive)
            {
                botCross.SetActive(true);
            }
            if (botZeroActive)
            {
                botZero.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Завершает игру
    /// </summary>
    public void SetGameEnd()
    {
        //Выводим результат
        if (crossWin)
        {
            resultText.text = "Cross Wins";
        }
        else if (zeroWin)
        {
            resultText.text = "Zero Wins";
        }
        else if (GameFieldController.main.freeCells.Count == 0)
        {
            resultText.text = "Tie";
        }

        //Включаем меню
        gameplayMenu.SetActive(true);
        gameOver = true;
    }
}
