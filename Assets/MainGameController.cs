using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {

    public static MainGameController S;

    public GameObject player;

	// Use this for initialization
	void Start () {
        S = this;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnWithDelay(float time) {
        Invoke("RespawnPlayer", time);
    }

    private void RespawnPlayer() {
        player.transform.position = Player.respawnLocation;
        player.SetActive(true);
    }
}
