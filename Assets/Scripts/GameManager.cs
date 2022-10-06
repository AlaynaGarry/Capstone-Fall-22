using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cainos;

public class GameManager : MonoBehaviour
{

    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum State
    {
        TITLE,
        PLAYER_START,
        INTRO,
        GAME,
        GAMEOVER,
        GAMEWIN
    }

    [Header ("Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawn;

    [Header("Game UI")]
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject instructionsScreen;
    [SerializeField] GameObject inGameScreen;
   
    public delegate void GameEvent();

    public event GameEvent startGameEvent;
    public event GameEvent stopGameEvent;

    public State state = State.GAME;
    float stateTimer;

    private void Update()
    {
        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.TITLE:
                OnStartTitle();
                break;
            case State.PLAYER_START:
                startGameEvent?.Invoke();

                state = State.INTRO;
                break;
            case State.INTRO:
                instructionsScreen.SetActive(true);
                break;
            case State.GAME:

                
                break;
            case State.GAMEWIN:
                if (stateTimer <= 0)
                {

                }
                break;
            case State.GAMEOVER:
                if (stateTimer <= 0)
                {
                    state = State.TITLE;
                }

                break;
            default:
                break;
        }
    }

    public void OnStartGame()
    {
        state = State.PLAYER_START;

        foreach (GameObject enemy in enemies) {
            enemy.SetActive(true);
        }

        titleScreen.SetActive(false);

    }

    public void OnIntroContinue()
    {
        state = State.GAME;
        instructionsScreen.SetActive(false);
    }

    public void OnPlayerDead()
    {
       
    }

    public void OnStartTitle()
    {
        titleScreen.SetActive(true);
        
        stopGameEvent?.Invoke();
    }
}