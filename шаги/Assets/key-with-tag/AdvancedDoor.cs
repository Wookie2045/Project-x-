using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedDoor : MonoBehaviour
{
    [Header("Door Components")]
    public Animator DoorAnimator;
    public Transform Player;

    [Header("Lock Settings")]
    public bool isLocked = true;
    public string requiredKeyTag = "Key";

    [Header("Door Sounds")]
    public AudioSource doorAudioSource;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip doorLockedSound;
    [Range(0, 1)] public float doorVolume = 0.8f;

    private bool isOpen = false;
    private bool inInteractionRange = false;

    void Update()
    {
        // ѕроверка рассто€ни€ до игрока
        if (Player)
        {
            float dist = Vector3.Distance(Player.position, transform.position);
            inInteractionRange = dist < 5f; // 3 метра - дистанци€ взаимодействи€
        }
    }

    public void TryOpen()
    {
        if (isLocked)
        {
            PlaySound(doorLockedSound);
            return;
        }

        ToggleDoorState();
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log("Door unlocked!");
    }

    private void ToggleDoorState()
    {
        isOpen = !isOpen;
        DoorAnimator.SetBool("Interact", isOpen);
        PlaySound(isOpen ? doorOpenSound : doorCloseSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (doorAudioSource != null && clip != null)
        {
            doorAudioSource.PlayOneShot(clip, doorVolume);
        }
    }

    // ƒл€ взаимодействи€ через луч (из PlayerKey)
    public void HandleDoorInteraction()
    {
        if (inInteractionRange)
        {
            if (isLocked)
            {
                PlaySound(doorLockedSound);
            }
            else
            {
                ToggleDoorState();
            }
        }
    }
}
