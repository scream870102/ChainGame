using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{


    // Please use difficult by GetDifficult() and test it in editor's inspector.
    [SerializeField, Header("Test")]
    Difficult difficult;

    // Please set timeLength in editor's inspector.
    [SerializeField, Header("Game Setting")]
    TimeLength timeLength = TimeLength.SHORT;

    //If game cleared
    protected bool isCleared;
    //remaining time
    float limitTime;

    // Please override this method and must call base.Awake();
    protected virtual void Awake() {
        limitTime = (float)timeLength;
        limitTime += Time.time;
        isCleared = false;
    }

    // Please override this method and must call base.Update();
    protected virtual void Update() {
        difficult = GameManager.instance.G_difficult;
        if (Time.time > limitTime) {
            GameManager.instance.SendResult(gameObject.scene.name, isCleared);
        }
    }

    //Return difficult
    protected Difficult GetDifficult() {
        Difficult temp = difficult;
        return temp;
    }

    protected float GetTimeRemain() {
        return limitTime - Time.time;
    }

    protected float GetTimeLength() {
        return (float)timeLength;
    }

    protected float GetTimePass() {
        return (float)timeLength - GetTimeRemain();
    }
}

