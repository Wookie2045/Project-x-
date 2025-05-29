using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptShrec : MonoBehaviour
{
    public GameObject hiddenObject; // Объект, который появится
    public AudioClip appearSound;    // Звук при появлении
    public float showTime = 3f;      // Время показа объекта (3 сек)

    private AudioSource audioSource;
    private bool isTriggered = false;

    void Start()
    {
        hiddenObject.SetActive(false); // Скрываем объект в начале
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
            hiddenObject.SetActive(true); // Показываем объект
            audioSource.PlayOneShot(appearSound); // Проигрываем звук

            Invoke("HideObject", showTime); // Прячем через 3 секунды
        }
    }

    void HideObject()
    {
        hiddenObject.SetActive(false);
        isTriggered = false; // Можно снова активировать
    }
}
