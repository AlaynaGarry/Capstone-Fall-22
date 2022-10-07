using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cainos;

/// <summary>
/// Base Code From Professor Maple During classes here at Neumont
/// Edits Made by yours truly Alayna Garry
/// </summary>

public class GameManager : MonoBehaviour
{
    [Header("Instance")]
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

    [Header("Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawn;

    [Header("Game UI")]
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject instructionsScreen;
    [SerializeField] GameObject inGameScreen;

    //[Header("Delegate & Event")]
    public delegate void GameEvent();

    public event GameEvent startGameEvent;
    public event GameEvent stopGameEvent;

    [Header("Start Info")]
    public State state = State.GAME;
    float stateTimer = 0;
    float restartTimer = 0;

    private void Update()
    {
        stateTimer -= Time.deltaTime;
        switch (state)
        {
            case State.TITLE:
                //Title Screen
                OnStartTitle();

                //Reset Game Values

                break;
            case State.INTRO:
                //Instruction Screen
                instructionsScreen.SetActive(true);
                
                // Control Screen

                //Change State
                state = State.PLAYER_START;
                break;
            case State.PLAYER_START:
                //Unsure what this does
                startGameEvent?.Invoke();
                break;
            case State.GAME:
                //Activate Game UI
                    //Health **STRETCH**
                    //Objectives **STRETCH**
                    //Stamina **STRETCH**

                //Win & Loose Condition
                //Win: Befriend 3 People
                //Lose: Make 1 Enemy
                break;
            case State.GAMEWIN:
                if (stateTimer <= 0)
                {
                    state = State.TITLE;
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

    /// <summary>
    /// Clickable Buttons / Actions
    /// </summary>
    public void OnStartGame()
    {
        state = State.INTRO;

        titleScreen.SetActive(false);
    }

    public void OnSkipIntro()
    {
        state = State.PLAYER_START;
    }

    public void OnIntroContinue()
    {
        state = State.GAME;
        instructionsScreen.SetActive(false);
    }

    /// <summary>
    /// Ease of Access Methods
    /// </summary>
    public void OnStartIntro()
    {
        instructionsScreen.SetActive(true);
    }

    public void OnStartTitle()
    {
        titleScreen.SetActive(true);

        stopGameEvent?.Invoke();
    }

    /// <summary>
    /// Other Game Loop Methods
    /// </summary>
    public void OnPlayerDead()
    {
        state = State.GAMEOVER;
        restartTimer = 0;
        
        if (restartTimer >= 5f)
        {
            restartTimer += Time.deltaTime;
        }
    }
}