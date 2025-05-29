using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTextTrigger : MonoBehaviour
{
    [Header("Text Settings")]
    public string notebookSceneName = "NotebookScene";
    public string textObjectName; // ��� ������� Text � ��������
    public string message; // ����� ��� �����������

    private bool triggered = false;
    private GameObject notebookText;

    void Start()
    {
        // ��������� ����� �������� ����������
        if (!SceneManager.GetSceneByName(notebookSceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(notebookSceneName, LoadSceneMode.Additive);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            ShowTextInNotebook();
        }
    }

    void ShowTextInNotebook()
    {
        Scene notebookScene = SceneManager.GetSceneByName(notebookSceneName);
        GameObject[] rootObjects = notebookScene.GetRootGameObjects();

        foreach (GameObject root in rootObjects)
        {
            // ���� ������ Text ������ �� �����
            Transform textTransform = root.transform.Find(textObjectName);
            if (textTransform != null)
            {
                notebookText = textTransform.gameObject;
                notebookText.SetActive(true);

                // ������������� �����
                var textComponent = notebookText.GetComponent<UnityEngine.UI.Text>();
                if (textComponent != null)
                {
                    textComponent.text = message;
                }
                break;
            }
        }
    }
}