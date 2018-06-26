using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    [SerializeField, Header("Setting")]
    private float ACCEL_TIME;
    [SerializeField, Header("Setting")]
    private float MAX_SPEED;
    private float timeLeft;
    private Vector3 movement;
    private Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        timeLeft = ACCEL_TIME;
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            movement = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0.0f);
            timeLeft = ACCEL_TIME;
        }
    }
    void FixedUpdate() {
        rb.AddForce(movement * MAX_SPEED);
    }
}
