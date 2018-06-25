using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//Must inherit Class Game
//

public class Ghost : Game
{
    private const float OFFSET = 3.0f;
    private const float SPAWN_OFFSET = 1.0f;
    private const float END_OFFSET = 1.5f;
    private float appearTime;
    private bool isDead;
    public GameObject ghost;
    private List<GameObject> ghosts = new List<GameObject>();
    private AudioSource audio;

    //must override update and write your game
    protected void Start() {
        appearTime = Random.Range(.0f, GetTimeLength()-END_OFFSET);
        isDead = false;
        audio = GetComponent<AudioSource>();
    }
    protected override void Update() {
        base.Update();
        //
        //
        //Write Your Game After
        if (GetTimePass() >= appearTime && isDead == false) {
            audio.Play();
            ghosts.Add(spawnGhost());
            isDead = true;
        }
        if (isDead) {
            if (GetTimePass()+SPAWN_OFFSET < GetTimeLength() - END_OFFSET) {
                appearTime = Random.Range(GetTimePass()+SPAWN_OFFSET, GetTimeLength() - END_OFFSET);
                isDead = false;
            }
            
        }
        if (GetTimeRemain() < 0.01f) {
            if (ghosts.Count == 0)
                isCleared = true;
        }

    }

    private GameObject spawnGhost() {
        Vector3 newPos=gameObject.transform.position +new Vector3(Random.Range(-OFFSET, OFFSET), Random.Range(-OFFSET*0.5f, OFFSET*1.5f),0);
        newPos.z = 0.0f;
        return GameObject.Instantiate(ghost,newPos,new Quaternion(0.0f,0.0f,0.0f,0.0f));
    }

    void OnMouseDown() {
        if (ghosts.Count!=0) {
            audio.Stop();
            for (int i=0;i<ghosts.Count;i++) {
                ghosts[i].SetActive(false);
                ghosts.RemoveAt(i);
            }
        }
    }
}
