using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour 
{
	public GameObject enemyPrefab;
	public float waitTime = 5;

	public RectTransform lifeBarSpawnPoint;
	public float lifePointsSP = 50f;
	private float maxLifePointsSP;

	void Start () 
	{
		maxLifePointsSP = lifePointsSP;
		InvokeRepeating ("CreateEnemy", 0f, waitTime);	
	}

	void CreateEnemy () 
	{
		Instantiate (enemyPrefab, transform.position, Quaternion.identity);
	}

	public void Shooting (float damageSP)
	{
		lifePointsSP -= damageSP;
		lifeBarSpawnPoint.localScale = new Vector3 (lifePointsSP / maxLifePointsSP, 1.0f, 1.0f);
		if (lifePointsSP <= 0f) 
		{
			lifeBarSpawnPoint.localScale = new Vector3 (0.0f, 1.0f, 1.0f);
			Destroy (this.gameObject, 0);
		}
	}
}
