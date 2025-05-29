using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class AutoDoor : MonoBehaviour
    {
        [Header("Door Components")]
        public Animator openandclose;
        public Transform Player;
        public Collider doorPhysicalCollider; // Основной коллайдер
        public Collider doorTriggerCollider;  // Триггер для обнаружения прохода

        [Header("Door States")]
        [SerializeField] private bool open = false;
        [SerializeField] private bool isLocked = false;

        [Header("Door Settings")]
        public float autoCloseDistance = 3f;
        public bool doorLockedAfterUse = true;
        public float interactionDistance = 2f;

        [Header("Sound Settings")]
        public AudioSource doorAudioSource;
        public AudioClip doorOpenSound;
        public AudioClip doorCloseSound;
        public AudioClip doorLockedSound;
        [Range(0, 1)] public float doorVolume = 0.8f;

        void Start()
        {
            // Автонастройка коллайдеров если не заданы вручную
            if (doorPhysicalCollider == null)
                doorPhysicalCollider = GetComponent<Collider>();

            if (doorTriggerCollider == null)
            {
                Collider[] colliders = GetComponents<Collider>();
                foreach (Collider col in colliders)
                {
                    if (col.isTrigger && col != doorPhysicalCollider)
                    {
                        doorTriggerCollider = col;
                        break;
                    }
                }
            }

            open = false;
            isLocked = false;
        }

        void Update()
        {
            // Автоматическое закрытие двери
            if (open && !IsPlayerInTrigger() && Player != null)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist > autoCloseDistance)
                {
                    StartCoroutine(closing());
                    if (doorLockedAfterUse)
                    {
                        LockDoor();
                    }
                }
            }
        }

        void OnMouseOver()
        {
            if (Player == null) return;
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist > interactionDistance) return;
                {
                    if (isLocked)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            PlayLockedSound();
                        }
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!open)
                        {
                            StartCoroutine(opening());
                        }
                        else
                        {
                            StartCoroutine(closing());
                        }
                    }
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Можно добавить логику при входе в триггер
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && open)
            {
                if (doorLockedAfterUse)
                {
                    LockDoor();
                }
                StartCoroutine(closing());
            }
        }

        private bool IsPlayerInTrigger()
        {
            if (doorTriggerCollider == null) return false;
            Collider[] colliders = Physics.OverlapBox(doorTriggerCollider.bounds.center, doorTriggerCollider.bounds.extents);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }

        private void LockDoor()
        {
            isLocked = true;
            PlayLockedSound();
        }

        private void PlayLockedSound()
        {
            if (doorAudioSource != null && doorLockedSound != null)
            {
                doorAudioSource.PlayOneShot(doorLockedSound, doorVolume);
            }
        }

        IEnumerator opening()
        {
            print("Opening the door");
            openandclose.Play("Opening");

            // Отключаем физический коллайдер при открытии
            if (doorPhysicalCollider != null)
                doorPhysicalCollider.enabled = false;

            if (doorAudioSource != null && doorOpenSound != null)
            {
                doorAudioSource.PlayOneShot(doorOpenSound, doorVolume);
            }

            open = true;
            yield return new WaitForSeconds(0.5f);
        }

        IEnumerator closing()
        {
            print("Closing the door");
            openandclose.Play("Closing");

            // Включаем физический коллайдер при закрытии
            if (doorPhysicalCollider != null)
                doorPhysicalCollider.enabled = true;

            if (doorAudioSource != null && doorCloseSound != null)
            {
                doorAudioSource.PlayOneShot(doorCloseSound, doorVolume);
            }

            open = false;
            yield return new WaitForSeconds(0.5f);
        }

        // Визуализация зоны взаимодействия в редакторе
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactionDistance);

            if (doorTriggerCollider != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(doorTriggerCollider.bounds.center, doorTriggerCollider.bounds.size);
            }
        }
    }
}
