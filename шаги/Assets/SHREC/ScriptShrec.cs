using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptShrec : MonoBehaviour
{
    public GameObject hiddenObject; // ������, ������� ��������
    public AudioClip appearSound;    // ���� ��� ���������
    public float showTime = 3f;      // ����� ������ ������� (3 ���)

    private AudioSource audioSource;
    private bool isTriggered = false;

    void Start()
    {
        hiddenObject.SetActive(false); // �������� ������ � ������
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            hiddenObject.SetActive(true); // ���������� ������
            audioSource.PlayOneShot(appearSound); // ����������� ����

            Invoke("HideObject", showTime); // ������ ����� 3 �������
        }
    }

    void HideObject()
    {
        hiddenObject.SetActive(false);
        isTriggered = false; // ����� ����� ������������
    }
}
