using UnityEngine;
using System.Collections;

public class FireRandom : MonoBehaviour 
{
	private GameObject fire;
	private ParticleSystem aux;
	private bool auxFire;

	void Start()
	{
		fire = GameObject.FindGameObjectWithTag ("Fire");
		aux = GetComponent<ParticleSystem> ();
		auxFire = true;
	}

	void Update()
	{
		if (auxFire == true)
			StartCoroutine ("StopFire");
		else
			StartCoroutine ("StartFire");
	}


	IEnumerator StopFire()
	{
		aux.Stop ();
		fire.GetComponent<BoxCollider> ().enabled = false;
		yield return new WaitForSeconds (10);
		auxFire = false;
	}

	IEnumerator StartFire()
	{
		aux.Play ();
		fire.GetComponent<BoxCollider> ().enabled = true;
		yield return new WaitForSeconds (10);
		auxFire = true;
	}
}
