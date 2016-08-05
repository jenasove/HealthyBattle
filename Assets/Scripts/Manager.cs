using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour 
{
	public static GameObject character;
	public static CharacterController characterController;

	void Awake()
	{
		characterController = GameObject.FindObjectOfType<CharacterController> ();
		character = characterController.gameObject;
	}
}
