using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager: MonoBehaviour
{
    /// <summary>
    /// Главный интерфейс главного меню
    /// </summary>
    [SerializeField] private GameObject mainUI;

    /// <summary>
    /// Интерфейс настроек игры
    /// </summary>
    [SerializeField] private GameObject gameSettingsUI;

    /// <summary>
    /// Кнопки выбора фигуры
    /// </summary>
    [SerializeField] private GameObject[] figureSelectButtons;

    /// <summary>
    /// Кнопки настройки размера поля
    /// </summary>
    [SerializeField] private Button[] fieldSizeButtons;

    /// <summary>
    /// Кнопка выбора крестиков
    /// </summary>
    [SerializeField] private Button setPlayerCrossButton;

    /// <summary>
    /// Кнопка выбора ноликов
    /// </summary>
    [SerializeField] private Button setPlayerZeroButton;

    /// <summary>
    /// Загружает игру
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    /// <summary>
    /// Включает или выключает кнопки выбора фигуры
    /// </summary>
    public void SwitchPlayerFigureButton()
    {
        if (GameManager.playerCross)
        {
            GameManager.playerCross = false;
            GameManager.botCrossActive = true;
            GameManager.botZeroActive = false;
            setPlayerZeroButton.interactable = false;
            setPlayerCrossButton.interactable = true;
        }
        else
        {
            GameManager.playerCross = true;
            GameManager.botCrossActive = false;
            GameManager.botZeroActive = true;
            setPlayerZeroButton.interactable = true;
            setPlayerCrossButton.interactable = false;
        }
    }

    /// <summary>
    /// Включает или выключает кнопки настройки размера поля
    /// </summary>
    public void SwitchFieldSizeButton(Button currentButton)
    {
        foreach (Button button in fieldSizeButtons)
        {
            button.interactable = true;
        }

        currentButton.interactable = false;
    }

    /// <summary>
    /// Открывает интерфейс настроек игры
    /// </summary>
    public void OpenGameSettingsUI()
    {
        mainUI.SetActive(false);
        gameSettingsUI.SetActive(true);
    }

    /// <summary>
    /// Открывает основной интерфейс главного меню
    /// </summary>
    public void OpenMainUI()
    {
        gameSettingsUI.SetActive(false);
        mainUI.SetActive(true);
        GameManager.botCrossActive = false;
        GameManager.botZeroActive = false;
    }

    /// <summary>
    /// Активирует или деактивирует выбор фигуры
    /// </summary>
    public void SetFigureSelectButtonsState(bool buttonState)
    {
        foreach (GameObject button in figureSelectButtons)
        {
            button.SetActive(buttonState);
        }
    }

    /// <summary>
    /// Устанавливает размер поля
    /// </summary>
    public void SetFieldSize(int size)
    {
        GameManager.fieldSize = size;
    }

    /// <summary>
    /// Загружает главное меню
    /// </summary>
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Настраивает PVP
    /// </summary>
    public void SelectPVP()
    {
        SetFigureSelectButtonsState(false);
        GameManager.playerCount = 2;
        GameManager.botCrossActive = false;
        GameManager.botZeroActive = false;
    }

    /// <summary>
    /// Настраивает PVB
    /// </summary>
    public void SelectPVB()
    {
        SetFigureSelectButtonsState(true);
        GameManager.playerCount = 1;
        if (GameManager.playerCross)
        {
            GameManager.botZeroActive = true;
            GameManager.botCrossActive = false;
        }
        else
        {
            GameManager.botZeroActive = false;
            GameManager.botCrossActive = true;
        }
    }

    /// <summary>
    /// Настраивает BVB
    /// </summary>
    public void SelectBVB()
    {
        SetFigureSelectButtonsState(false);
        GameManager.playerCount = 0;
        GameManager.botCrossActive = true;
        GameManager.botZeroActive = true;
    }
}
