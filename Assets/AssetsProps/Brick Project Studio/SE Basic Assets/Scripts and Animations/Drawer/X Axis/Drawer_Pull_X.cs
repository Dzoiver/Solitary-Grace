using GM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{

	public class Drawer_Pull_X : MonoBehaviour
	{

		public Animator pull_01;
		public bool open;
		private Transform Player;
        [SerializeField] AudioSource audio;
        [SerializeField] AudioClip openClip;
        [SerializeField] AudioClip closeClip;

        void Start()
		{
			open = false;
			Player = GameFuncs.PlayerScript.gameObject.transform;
		}

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 2f)
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
			pull_01.Play("openpull_01");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			pull_01.Play("closepush_01");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}