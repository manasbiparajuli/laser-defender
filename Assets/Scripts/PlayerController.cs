using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float projectileSpeed;
	[SerializeField] float fireRate;
	[SerializeField] float playerHealth;
	[SerializeField] GameObject laserProjectile;

	[SerializeField] AudioClip fireSound;

	private float xMin;
	private float xMax;
	private float padding = 1.0f;

	private void Start()
	{
		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zDistance));

		xMin = leftmost.x  + padding;
		xMax = rightmost.x - padding;
	}

	void Fire()
	{
		Vector3 offset = new Vector3(0, 0.5f, 0);

		GameObject beam = Instantiate(laserProjectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);

		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			InvokeRepeating("Fire", 0.00001f, fireRate);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		// Restrict the player to the game space
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Projectile missile = collision.gameObject.GetComponent<Projectile>();

		if (missile != null)
		{
			playerHealth -= missile.GetDamage();
			missile.Hit();

			if (playerHealth <= 0)
			{
				Die();
			}
		}
	}

	private void Die()
	{
		GameObject.Find("Scene Loader").GetComponent<SceneLoader>().LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}