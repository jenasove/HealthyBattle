using UnityEngine;
using System.Collections;
using System.Collections.Generic;
enum EnemyState {PATROL, SHOOTING}

public class EnemyController : MonoBehaviour 
{
	public Vector2 areaPatrol;
	public float patrolUpdate;

	public float damageEnemy = 0.02f;
	public RectTransform lifeBar;
	public float lifePoints = 10f;
	private float maxLifePoints;
	private float lifeIncrease = 10;

	private Vector3 randomDestination;
	private Vector3 startPosition;

	private GameObject player;
	private Animator enemyAnimator;
	private NavMeshAgent agent;
	private EnemyState currentState;

	void Start () 
	{
		maxLifePoints = lifePoints;
		player = GameObject.FindGameObjectWithTag ("Player");
		enemyAnimator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		startPosition = transform.position;

		currentState = EnemyState.PATROL;
		StartCoroutine ("Patrol");
	}

	void Update () 
	{
		if (Vector3.Distance (agent.transform.position, player.transform.position) < 2f)
			StartCoroutine ("Shooting");

		enemyAnimator.SetFloat ("speed", agent.velocity.magnitude);
	}

	IEnumerator Patrol()
	{
		randomDestination = startPosition + new Vector3 (Random.Range (-areaPatrol.x, areaPatrol.x), 0.0f, Random.Range (-areaPatrol.y, areaPatrol.y));
		agent.SetDestination (randomDestination);
		yield return new WaitForSeconds(patrolUpdate);
		if(currentState == EnemyState.PATROL)
			StartCoroutine("Patrol");
	}

	IEnumerator Shooting()
	{
		StopCoroutine ("Patrol");
		agent.enabled = false;
		enemyAnimator.SetTrigger ("shoot");
		transform.LookAt (Manager.character.transform.position);

		RaycastHit hit;
		if (Physics.Raycast (this.transform.position + this.transform.up * this.transform.localScale.y * 0.5f, this.transform.forward, out hit)) 
		{
			if (hit.transform == Manager.character.transform)
				Manager.characterController.ShootEnemy (damageEnemy);
		}
		yield return new WaitForSeconds (0);
	}

	public void Shoot(float damageCharacter)
	{
		lifePoints -= damageCharacter;
		lifeBar.localScale = new Vector3 (lifePoints / maxLifePoints, 1.0f, 1.0f);
		if (lifePoints <= 0) 
		{
			lifeBar.localScale = new Vector3 (0.0f, 1.0f, 1.0f);
			DieEnemy ();
		}
	}

	public void DieEnemy()
	{
		enemyAnimator.SetTrigger ("death");
		enemyAnimator.gameObject.GetComponent<Animator> ().enabled = false;
		agent.Stop ();
		Destroy (this.gameObject, 0);
		player.SendMessage ("LifeIncrease", lifeIncrease);
	}
}
