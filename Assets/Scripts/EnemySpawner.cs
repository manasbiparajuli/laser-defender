using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float gizmoWidth;
	[SerializeField] float gizmoHeight;
	[SerializeField] float speed;
	[SerializeField] float spawnDelay;

	private bool movingRight = false;
	private float xMax;
	private float xMin;
	
	// Use this for initialization
	void Start ()
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

		xMax = rightBoundary.x;
		xMin = leftBoundary.x;

		SpawnUntilFull();
	}

	private void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		
		if (freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		if (NextFreePosition())
		{
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gizmoWidth, gizmoHeight));
	}

	// Update is called once per frame
	void Update ()
	{
		if (movingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * gizmoWidth);
		float leftEdgeOfFormation = transform.position.x - (0.5f * gizmoWidth);

		if (leftEdgeOfFormation < xMin)
		{
			movingRight = !movingRight;
		}
		else if (rightEdgeOfFormation > xMax)
		{
			movingRight = false;
		}

		if (AllMembersDead())
		{
			SpawnUntilFull(); 
		}
	}

	Transform NextFreePosition()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}

	private bool AllMembersDead()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
}