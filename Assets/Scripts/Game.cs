using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    protected enum Difficult
    {
        EASY,
        NORMAL,
        HARD
    }


    // Please use difficult by GetDifficult() and test it in editor's inspector.
    [SerializeField, Header("Test")]
    Difficult difficult;

    enum TimeLength
    {
        SHORT = 5,
        MIDDLE = 7,
        LONG = 9
    }

    // Please set timeLength in editor's inspector.
    [SerializeField, Header("Game Setting")]
    TimeLength timeLength = TimeLength.SHORT;

    protected bool isCleared;

    float limitTime;

    // Please override this method and must call base.Awake();
    protected virtual void Awake() {
        limitTime = (float)timeLength;
        limitTime += Time.time;
        isCleared = false;
    }

    // Please override this method and must call base.Update();
    protected virtual void Update() {
        if (Time.time > limitTime) {
            //Debug.LogWarning(isCleared);
            GameManager.instance.LoadGame(gameObject.scene.name, isCleared);
        }
    }

    protected Difficult GetDifficult() {
        return difficult;
    }

}

