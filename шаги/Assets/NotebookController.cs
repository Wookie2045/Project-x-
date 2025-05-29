using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotebookController : MonoBehaviour
{
    [Header("Settings")]
    public string notebookSceneName = "NotebookScene"; // Имя сцены с блокнотом
    public KeyCode toggleKey = KeyCode.Tab;            // Клавиша открытия
    public bool pauseGameWhileOpen = true;             // Ставить ли игру на паузу

    private bool isNotebookOpen = false;
    private string previousScene;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleNotebook();
        }
    }

    public void ToggleNotebook()
    {
        if (isNotebookOpen)
        {
            CloseNotebook();
        }
        else
        {
            OpenNotebook();
        }
    }

    void OpenNotebook()
    {
        // Запоминаем текущую сцену
        previousScene = SceneManager.GetActiveScene().name;

        // Загружаем сцену блокнота
        SceneManager.LoadScene(notebookSceneName, LoadSceneMode.Additive);

        // Ставим игру на паузу если нужно
        if (pauseGameWhileOpen)
        {
            Time.timeScale = 0f;
        }

        // Разблокируем курсор
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isNotebookOpen = true;
    }

    void CloseNotebook()
    {
        // Выгружаем сцену блокнота
        SceneManager.UnloadSceneAsync(notebookSceneName);

        // Возвращаем нормальную скорость игры
        Time.timeScale = 1f;

        // Блокируем курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isNotebookOpen = false;
    }
}
