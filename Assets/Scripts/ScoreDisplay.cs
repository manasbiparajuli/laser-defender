using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
	private TextMeshProUGUI textMesh;

	// Use this for initialization
	void Start ()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
		textMesh.text = ScoreKeeper.score.ToString();

		ScoreKeeper.Reset();
	}

	// Update is called once per frame
	void Update ()
	{
	}
}
