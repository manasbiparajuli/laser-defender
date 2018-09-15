// Audio Manager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	static MusicPlayer instance = null;

	[SerializeField] AudioClip startClip;
	[SerializeField] AudioClip gameClip;
	[SerializeField] AudioClip endClip;

	private AudioSource music;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			// Avoid duplicate music playing on the background
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			// Keep the background music until the user quits from the game
			GameObject.DontDestroyOnLoad(gameObject);

			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		music.Stop();

		if (level == 0)
		{
			music.clip = startClip;
		}
		else if (level == 1)
		{
			music.clip = gameClip;
		}
		if (level == 2)
		{
			music.clip = endClip;
		}
		else
		{
			music.clip = startClip;
		}
		music.loop = true;
		music.Play();
	}
}