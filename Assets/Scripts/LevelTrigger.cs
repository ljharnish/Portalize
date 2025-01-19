using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
	public string nextScene;

	private void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene(nextScene);
	}

	public void Load()
	{
		SceneManager.LoadScene(nextScene);
	}
}
