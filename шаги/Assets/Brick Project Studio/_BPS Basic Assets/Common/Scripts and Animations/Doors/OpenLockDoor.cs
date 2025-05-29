using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class OpenLockDoor : MonoBehaviour
    {

        public Animator openandclose;
        public bool open;
        public Transform Player;
        [SerializeField] Animator DoorAnimator;
        [SerializeField] bool Closed;

        [Header("Door Sounds")]
        public AudioSource doorAudioSource;
        public AudioClip doorOpenSound;
        public AudioClip doorCloseSound;
        [Range(0, 1)] public float doorVolume = 0.8f;

        void Start()
        {
            open = false;
        }

        /*public void TryOpen()
        {
            if (!Closed)
            {
                if (DoorAnimator.GetBool("interact") == false)
                {
                    DoorAnimator.SetBool("interact", true);
                }
                else
                {
                    DoorAnimator.SetBool("interact", false);
                }
            }
        }*/

        public void Unlock()
        {
            Closed = false;
        }

        public void TryOpen()
        {
            {
                if (!Closed)
                {
                    if (DoorAnimator.GetBool("interact") == false)
                    {
                        DoorAnimator.SetBool("interact", true);
                    }
                    else
                    {
                        DoorAnimator.SetBool("interact", false);
                    }
                
                
                
                    if (Player)
                    {
                        float dist = Vector3.Distance(Player.position, transform.position);
                        if (dist < 15)
                        {
                            if (open == false)
                            {
                                if (Input.GetKeyDown(KeyCode.E))
                                {
                                    StartCoroutine(opening());
                                }
                            }
                            else
                            {
                                if (open == true)
                                {
                                    if (Input.GetKeyDown(KeyCode.E))
                                    {
                                        StartCoroutine(closing());
                                    }
                                }

                            }

                        }
                    }
                }

            }

        }

        IEnumerator opening()
        {
            print("you are opening the door");
            openandclose.Play("Opening");
            if (doorAudioSource != null && doorOpenSound != null)
            {
                doorAudioSource.PlayOneShot(doorOpenSound, doorVolume);
            }
            open = true;
            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the door");
            openandclose.Play("Closing");
            if (doorAudioSource != null && doorCloseSound != null)
            {
                doorAudioSource.PlayOneShot(doorCloseSound, doorVolume);
            }
            open = false;
            yield return new WaitForSeconds(.5f);
        }


    }
}