using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {

    public static MainGameController S;

    public GameObject playerPrefab;

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
        GameObject player = GameObject.Instantiate(playerPrefab, Player.respawnLocation, Quaternion.identity);
        GetComponent<FollowCam>().player = player;
    }
}
