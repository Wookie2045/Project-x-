using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotebookController : MonoBehaviour
{
    [Header("Settings")]
    public string notebookSceneName = "NotebookScene"; // ��� ����� � ���������
    public KeyCode toggleKey = KeyCode.Tab;            // ������� ��������
    public bool pauseGameWhileOpen = true;             // ������� �� ���� �� �����

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
        // ���������� ������� �����
        previousScene = SceneManager.GetActiveScene().name;

        // ��������� ����� ��������
        SceneManager.LoadScene(notebookSceneName, LoadSceneMode.Additive);

        // ������ ���� �� ����� ���� �����
        if (pauseGameWhileOpen)
        {
            Time.timeScale = 0f;
        }

        // ������������ ������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isNotebookOpen = true;
    }

    void CloseNotebook()
    {
        // ��������� ����� ��������
        SceneManager.UnloadSceneAsync(notebookSceneName);

        // ���������� ���������� �������� ����
        Time.timeScale = 1f;

        // ��������� ������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isNotebookOpen = false;
    }
}
