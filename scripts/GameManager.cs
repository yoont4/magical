using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState { playing, won, lost, pause };

public class GameManager : MonoBehaviour {

    public KeyCode restartKey;
    public KeyCode giveupKey;
    public KeyCode pauseKey;
    public string gameScene;
    public string gameoverScene;
    private GameManager GM;
    private GameState gameState;
    private int loseCount;

	// Use this for initialization
	void Start () {
        gameState = GameState.playing;
	}
	
	// Update is called once per frame
	void Update () {
        // I set "O" to be a placeholder that manually fail the game
        // Based on the observation of another simular game, 
        //I think there should be a "give up" button so that 
        // player can escape from unexpected dead-lock situation
        if (Input.GetKey(giveupKey))
        {
            setGameOver();
        }
        else if (Input.GetKeyUp(pauseKey)) {
            setPause();
        }
        if (gameState == GameState.lost || gameState == GameState.won)
        {
            if (Input.GetKey(restartKey))
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
            }
        }
	}

    /*
     * toggle game state between pause and playing, if the game is in other states, do nothing 
     */
    void setPause()
    {
        if (gameState == GameState.playing) 
        {
            gameState = GameState.pause;
            Time.timeScale = 0.0f;
        } else if (gameState == GameState.pause) {
            gameState = GameState.playing;
            Time.timeScale = 1.0f;
        }
    }

    public void setGameOver() {
        if (gameState != GameState.lost)
        {
            Debug.Log("I am here");
            Time.timeScale = 0.0f;
            Debug.Log("you lose");
            gameState = GameState.lost;
            SceneManager.LoadScene(gameoverScene, LoadSceneMode.Additive);
        }  
    } 
}
