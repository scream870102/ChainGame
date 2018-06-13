using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public enum GameState
    {
        START,
        LOADING,
        PLAYING,
        DEAD,
        DISPLAY_RESULT,
        WAIT,
        RETRY
    }
    const int FIRST_SCENE_INDEX = 2;
    const int BASIC_SCORE = 10;
    const int INIT_LIFE_POINT = 1;

    public GameState gameState;
    [HideInInspector]
    public static GameManager instance = null;
    [HideInInspector]
    public int G_score = 0;
    public Difficult G_difficult = Difficult.EASY;

    [SerializeField]
    private List<string> subGameSet = new List<string>();
    private int lifePoint;
    private int sceneCount;


    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

       
    }


// Use this for initialization
    void Start () {
        //Get all  scenes in building 
        //And set their name to subGameSet
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
            subGameSet.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        lifePoint = INIT_LIFE_POINT;
        G_score = 0;
        gameState = GameState.START;
    }


    void Update() {
        switch (gameState) {
            case GameState.START:
                break;
            case GameState.LOADING:
                LoadInitGame();
                break;
            case GameState.PLAYING:
                break;
            case GameState.DEAD:
                SetResult();
                break;
            case GameState.DISPLAY_RESULT:
                SetScore();
                break;
            case GameState.WAIT:
                break;
            case GameState.RETRY:
                Init();
                break;
            default:
                return;

        }

    }


    private void Init() {
        lifePoint = INIT_LIFE_POINT;
        G_score = 0;
        gameState = GameState.START;
        G_difficult = Difficult.EASY;
        SceneManager.LoadScene("Main");
    }


    public void LoadGame(string currentGame,bool isCleared) {
        if (lifePoint <= 0)
            gameState = GameState.DEAD;

        if (isCleared)
            G_score += BASIC_SCORE;
        else if (!isCleared)
            lifePoint--;
        string nextGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        while (nextGame==currentGame) {
            nextGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        }
        SceneManager.LoadScene(nextGame);
    }


    private void LoadInitGame() {
        string initGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        gameState = GameState.PLAYING;
        SceneManager.LoadScene(initGame);
    }


    public static int GetLifePoint() {
        int temp = instance.lifePoint;
        return temp;
    }


    private void SetResult() {
        SceneManager.LoadScene("Result");
        while (SceneManager.GetActiveScene().name != "Result") {
            return;
        }
        gameState = GameState.DISPLAY_RESULT;
    }


    private void SetScore() {
        GameObject resultManagerObject = GameObject.FindGameObjectWithTag("Result");
        
        if (resultManagerObject != null) {
            ResultManager resultManager = resultManagerObject.GetComponent<ResultManager>();
            Text scoreText = resultManager.scoreText;
            scoreText.text = "Score:" + G_score;
        }
        else {
            Debug.LogWarning("Not found the ScoreText");
        }
        gameState = GameState.WAIT;
    }




}
