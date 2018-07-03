using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {

    public static MainGameController S;

    public GameObject player;

	void Awake () {
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
        S = this;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LevelComplete () {
		int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex + 1;
		if (SceneManager.sceneCountInBuildSettings > nextSceneIndex) {
			SceneManager.LoadScene (nextSceneIndex);
		}
	}
}
