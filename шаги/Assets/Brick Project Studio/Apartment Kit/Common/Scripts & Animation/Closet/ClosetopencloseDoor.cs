using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class ClosetopencloseDoor : MonoBehaviour
	{

		public Animator Closetopenandclose;
		public bool open;
		public Transform Player;

        [Header("Door Sounds")]
        public AudioSource doorAudioSource;
        public AudioClip doorOpenSound;
        public AudioClip doorCloseSound;
        [Range(0, 1)] public float doorVolume = 0.8f;


        void Start()
		{
			open = false;
		}

		void OnMouseOver()
		{
			{
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

		IEnumerator opening()
		{
			print("you are opening the door");
			Closetopenandclose.Play("ClosetOpening");
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
			Closetopenandclose.Play("ClosetClosing");
            if (doorAudioSource != null && doorCloseSound != null)
            {
                doorAudioSource.PlayOneShot(doorCloseSound, doorVolume);
            }
            open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}