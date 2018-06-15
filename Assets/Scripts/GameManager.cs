using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    //Enum of state machine
    public enum GameState
    {
        START,
        LOADING,
        PLAYING,
        RESULT,
        DEAD,
        DISPLAY_RESULT,
        WAIT,
        RETRY
    }
    //The first game in building
    const int FIRST_SCENE_INDEX = 3;
    //Basic score when every game cleared
    const int BASIC_SCORE = 10;
    //basic Life
    const int INIT_LIFE_POINT = 1;

    //ref of GameManager
    [HideInInspector]
    public static GameManager instance = null;
    //state machine
    public GameState gameState;
    //Total score
    [HideInInspector]
    public int G_score = 0;
    //Difficulty of every game
    public Difficult G_difficult = Difficult.EASY;
    //scene name of current game
    public string currentGame;
    //if current game is cleared
    public bool isCleared;

    //list to store every game
    [SerializeField]
    private List<string> subGameSet = new List<string>();
    private int lifePoint;
    //To store num of scene
    private int sceneCount;


    void Awake() {
        //check if instance is already exist if not make this unique
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    void Start () {
        //Get all  scenes in building 
        //And set their name to subGameSet
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
            subGameSet.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));

        //Init vars
        lifePoint = INIT_LIFE_POINT;
        G_score = 0;
        gameState = GameState.START;
    }


    //State machine
    void Update() {
        switch (gameState) {
            case GameState.START:
                break;
            case GameState.LOADING:
                LoadInitGame();
                break;
            case GameState.PLAYING:
                break;
            case GameState.RESULT:
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

    //Init Vars  if player press retry and load Main scene
    private void Init() {
        lifePoint = INIT_LIFE_POINT;
        G_score = 0;
        gameState = GameState.START;
        G_difficult = Difficult.EASY;
        SceneManager.LoadScene("Main");
    }


    //Load next game and check life
    public void LoadGame() {
        this.gameState = GameState.PLAYING;
        if (lifePoint <= 0)
            gameState = GameState.DEAD;
        string nextGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        while (nextGame == this.currentGame) {
            nextGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        }
        SceneManager.LoadScene(nextGame);
    }


    //Set result to var when game is finished and Load scene Stage Result
    public void SendResult(string currentGame, bool isCleared) {
        if (isCleared) {
            G_score += BASIC_SCORE;
        }
        else if (!isCleared) {
            lifePoint--;
        }
        this.currentGame = currentGame;
        this.isCleared = isCleared;
        SceneManager.LoadScene("StageResult");
        this.gameState = GameState.RESULT;
    }


    //Load First Game at random
    private void LoadInitGame() {
        string initGame = subGameSet[Random.Range(FIRST_SCENE_INDEX, subGameSet.Count)];
        gameState = GameState.PLAYING;
        SceneManager.LoadScene(initGame);
    }

    //Return lifePoint
    public int GetLifePoint() {
        int temp = instance.lifePoint;
        return temp;
    }

    //If life==0 then load Result scene
    private void SetResult() {
        SceneManager.LoadScene("Result");
        while (SceneManager.GetActiveScene().name != "Result") {
            return;
        }
        gameState = GameState.DISPLAY_RESULT;
    }

    //Set score text which in result scene
    private void SetScore() {
        GameObject resultManagerObject = GameObject.FindGameObjectWithTag("Result");
        
        if (resultManagerObject != null) {
            ResultManager resultManager = resultManagerObject.GetComponent<ResultManager>();
            Text scoreText = resultManager.scoreText;
            scoreText.text = "S c o r e : " + G_score;
        }
        else {
            Debug.LogWarning("Not found the ScoreText");
        }
        gameState = GameState.WAIT;
    }




}
