using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandler : MonoBehaviour {


    //Call when start button click
    public void StartBtnClick() {
        GameManager.instance.gameState=GameManager.GameState.LOADING;
    }

    //Call when Retry Button click
    public void RetryBtnClick() {
        GameManager.instance.gameState = GameManager.GameState.RETRY;
    }
}
