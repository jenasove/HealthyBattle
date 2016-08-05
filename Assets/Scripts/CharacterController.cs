using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour 
{
	public float speed = 2f;
	public float damage = 2f;

	public GameObject WinPanel;
	public Text coinsLabel;
	private int coins;

	public RectTransform lifeBarCharacter;
	public float lifePointsCharacter = 1000f;
	private float maxLifePointsCharacter;

	private Animator characterAnimator;
	private RaycastHit hit;

	public AudioClip[] soundFX;
	private AudioSource audioSource;

	void Start ()
	{
		maxLifePointsCharacter = lifePointsCharacter;
		characterAnimator = GetComponent<Animator> ();

		audioSource = GetComponent<AudioSource> ();

		DataManager.instance.LoadData ();
		coins = DataManager.instance.items;
	}

	void Update ()
	{
		characterAnimator.SetFloat ("speed", Input.GetAxis ("Vertical"));
		transform.position += (transform.forward * Input.GetAxis ("Vertical") * speed * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.RightArrow))
			transform.Rotate (new Vector3 (0, 10f, 0));

		if (Input.GetKeyDown (KeyCode.LeftArrow))
			transform.Rotate (new Vector3 (0, -10f, 0));

		if (Input.GetKeyDown (KeyCode.RightControl)) 
		{
			characterAnimator.SetTrigger ("jump");
			transform.Translate (new Vector3 (0f, 1f));
			audioSource.PlayOneShot (soundFX [0]);
		}
		
		if (Input.GetKeyDown(KeyCode.Space))//GetMouseButtonDown(0)) 
		{
			characterAnimator.SetTrigger ("shoot");
			if (Physics.Raycast (this.transform.position + this.transform.up * this.transform.localScale.y, this.transform.forward, out hit)) 
			{
				hit.transform.SendMessage ("ShootCharacter", damage);
				hit.transform.SendMessage ("ShootingCharacter", damage);
				audioSource.PlayOneShot (soundFX [1]);
			}
		}
		coinsLabel.text = "" + coins;
	}

	public void ShootEnemy(float damageEnemy)
	{
		lifePointsCharacter -= damageEnemy;
		print (lifePointsCharacter);
		lifeBarCharacter.localScale = new Vector3 (lifePointsCharacter / maxLifePointsCharacter, 1.0f, 1.0f);
		if (lifePointsCharacter <= 0f) 
		{
			lifeBarCharacter.localScale = new Vector3 (0.0f, 1.0f, 1.0f);
			DieCharacter ();
		}
	}

	void OnCollisionEnter (Collision collisionObject)
	{
		if (collisionObject.transform.tag == "Floor") 
		{
			DieCharacter ();
		}
	}

	void OnTriggerEnter(Collider colliderObject)
	{
		if (colliderObject.transform.tag == "Fire") 
		{
			DieCharacter ();
		} 

		if (colliderObject.transform.tag == "Corn")
		{
			Destroy (colliderObject.gameObject);
			coins++;
			DataManager.instance.items = coins;
			DataManager.instance.SaveData ();	
			audioSource.PlayOneShot (soundFX [2]);
		}

		if (colliderObject.transform.tag == "Win") 
		{
			Destroy (colliderObject.gameObject);
			characterAnimator.enabled = false;
			WinPanel.SetActive (true);
		}
	}

	public void DieCharacter()
	{
		characterAnimator.SetTrigger ("death");
		StartCoroutine ("Reload");
	}

	public void LifeIncrease (float lifeIncrease)
	{
		print ("IncrementoVida: "+lifeIncrease);
		lifePointsCharacter += lifeIncrease;
	}

	IEnumerator Reload()
	{
		characterAnimator.GetComponent<Animator>().enabled = false;
		yield return new WaitForSeconds (0);
		SceneManager.LoadScene ("Start");
	}
}
