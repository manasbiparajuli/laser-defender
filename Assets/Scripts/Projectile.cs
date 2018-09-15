using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float damage;

	public void Hit()
	{
		Destroy(gameObject);
	}

	public float GetDamage()
	{
		return damage;
	}
}
