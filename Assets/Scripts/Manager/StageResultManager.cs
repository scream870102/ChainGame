using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResultManager : MonoBehaviour {
    private const float DISPLAY_TIME = 3.0f;
    private float timer;
    public GameObject successful;
    public GameObject fail;
	// Use this for initialization
	void Start () {
        if (GameManager.instance.isCleared) {
            successful.gameObject.SetActive(true);
            fail.gameObject.SetActive(false);
        }
        else if (!GameManager.instance.isCleared) {
            successful.gameObject.SetActive(false);
            fail.gameObject.SetActive(true);

        }
        timer = DISPLAY_TIME;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            GameManager.instance.LoadGame();
        }
		
	}
}
