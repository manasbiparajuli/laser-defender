using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	private float health = 150f;
	[SerializeField] GameObject projectile;
	[SerializeField] float projectileSpeed;
	[SerializeField] float shotsFrequency;
	[SerializeField] AudioClip fireSound;
	[SerializeField] AudioClip playerDeathSound;

	private int scoreValue = 10;
	private ScoreKeeper scoreKeeper;

	private void Start()
	{
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	private void Update()
	{
		float probabilityOfFiring = Time.deltaTime * shotsFrequency;

		if (Random.value < probabilityOfFiring)
		{
			Fire();
		}
	}

	private void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Projectile missile = collision.gameObject.GetComponent<Projectile>();

		if (missile != null)
		{
			health -= missile.GetDamage();
			missile.Hit();

			if (health <= 0)
			{
				Die();
			}
		}
	}

	private void Die()
	{
		AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);
		scoreKeeper.Score(scoreValue);
		Destroy(gameObject);
	}
}