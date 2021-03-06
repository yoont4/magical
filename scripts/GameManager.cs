﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState { playing, won, lost, pause };

public class GameManager : MonoBehaviour {

    float minimumHeight = 0;  // If the caharacter falls below this height, set game over
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
                PlayerController.isAcceptingInput = true;
                Time.timeScale = 1.0f;
                SoundManager2.unpause();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Note: for some reason, this doesn't work -> SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
            }
        }
        if (transform.position.y < minimumHeight) { setGameOver(); }
	}

    /*
     * toggle game state between pause and playing, to be specific, 
     * it pauses / unpauses gameplay and sound
     */
    void setPause()
    {
        if (gameState == GameState.playing) 
        {
            gameState = GameState.pause;
            PlayerController.isAcceptingInput = false;
            Time.timeScale = 0.0f;
            SoundManager2.pause();
        } else if (gameState == GameState.pause) {
            gameState = GameState.playing;
            PlayerController.isAcceptingInput = true;
            Time.timeScale = 1.0f;
            SoundManager2.unpause();
        }
    }

    /*
     *  
     */
    public void setGameOver() {
        if (gameState != GameState.lost)
        {
            PlayerController.isAcceptingInput = false;
            Time.timeScale = 0.0f; 
            SoundManager2.terminate();
            Debug.Log("you lose");
            gameState = GameState.lost;
            SceneManager.LoadScene(gameoverScene, LoadSceneMode.Additive);
            //Application.LoadLevelAdditive(gameoverScene);
        }  
    } 
}
