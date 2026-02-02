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
					if (dist < 3f)
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