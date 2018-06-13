using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartBtnClick() {
        GameManager.instance.gameState=GameManager.GameState.LOADING;
    }

    public void RetryBtnClick() {
        GameManager.instance.gameState = GameManager.GameState.RETRY;
    }
}
