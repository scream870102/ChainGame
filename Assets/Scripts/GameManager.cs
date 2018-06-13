using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    const int FIRST_SCENE_INDEX = 1;
    const int BASIC_SCORE = 10;
    [HideInInspector]
    public static GameManager instance = null;
    [HideInInspector]
    public static int G_score = 0;
    private int lifePoint;
    [SerializeField]
    private List<string> subGameSet=new List<string> ();
    private int sceneCount;


    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //Get all  scenes in building 
        //And set their name to subGameSet
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for(int i =0;i<sceneCount;i++)
            subGameSet.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        lifePoint = 10;
    }


// Use this for initialization
void Start () {
        G_score = 0;

    }


    public void LoadGame(string currentGame,bool isCleared) {
        Debug.LogError(lifePoint.ToString() + isCleared+"  "+G_score);
        if (isCleared)
            G_score += BASIC_SCORE;
        else if (!isCleared)
            lifePoint--;


        string nextGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        while (nextGame==currentGame) {
            //Debug.Log(currentGame+"inWhile"+nextGame);
            nextGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        }
       //Debug.LogWarning(nextGame);
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextGame);
        //while (!asyncLoad.isDone) {
        //    Debug.Log("Loading" + nextGame);
        //    //return;
        //}
        SceneManager.LoadScene(nextGame);
    }


    public void LoadInitGame() {
        string initGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        SceneManager.LoadScene(initGame);
    }



    public static int GetLifePoint() {
        int temp = instance.lifePoint;
        return temp;
    }
}
