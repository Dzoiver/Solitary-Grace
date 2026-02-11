using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class Drawer_Pull_Z : MonoBehaviour
	{

		public Animator pull;
		public bool open;
		public Transform Player;
		[SerializeField] AudioSource audio;
		[SerializeField] AudioClip openClip;
        [SerializeField] AudioClip closeClip;

        void Start()
		{
			open = false;
			//audio = GetComponent<AudioSource>();
		}

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 3f)
					{
						if (open == false)
						{
							if (Input.GetKeyDown(KeyCode.E))
							{
								audio.PlayOneShot(openClip);
                                StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetKeyDown(KeyCode.E))
								{
                                    audio.PlayOneShot(closeClip);
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
			pull.Play("openpull");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			pull.Play("closepush");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}