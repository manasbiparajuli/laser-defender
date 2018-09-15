using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
	public static int score = 0;
	private TextMeshProUGUI textMesh;

	private void Start()
	{
		textMesh = GetComponent<TextMeshProUGUI>();
		Reset();
	}

	public void Score (int points)
	{
		score += points;
		textMesh.text = score.ToString();
	}

	public static void Reset()
	{
		score = 0;
	}
}
