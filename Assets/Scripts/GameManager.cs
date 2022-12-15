using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int timeToEnd;
    bool gamePaused = false;
    bool endGame = false;
    bool win = false;

    public int points = 0;

    public int redKey = 0;
    public int greenKey = 0;
    public int goldKey = 0;

    public AudioClip resumeClip;
    public AudioClip pauseClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    public MusicManager musicManager;
    bool lessTime = false;

    AudioSource audioSource;

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
        }
        else if (color == KeyColor.Green)
        {
            greenKey++;
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
        }
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freez, 1);
    }

    void PickUpCheck()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log($"Time: {timeToEnd}");
            Debug.Log($"Keys red: {redKey}, green: {greenKey}, gold: {goldKey}");
            Debug.Log($"Points: {points}");
        }
    }

    void Start()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

        if(timeToEnd <= 0)
        {
            timeToEnd = 100;
        }

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Stopper", 2, 1);
    }

    void Update()
    {
        PauseCheck();
        PickUpCheck();
    }

    public void PlayClip(AudioClip playClip)
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    void Stopper()
    {
        timeToEnd--;
        if (timeToEnd <= 0)
        {
            timeToEnd = 0;
            endGame = true;
        }
        if (endGame)
        {
            EndGame();
        }

        if(timeToEnd < 20 && !lessTime)
        {
            LessTimeOn();
            lessTime = true;
        }

        if(timeToEnd > 20 && lessTime)
        {
            LessTimeOff();
            lessTime = false;
        }
    }

    void PauseGame()
    {
        musicManager.OnPauseGame();
        PlayClip(pauseClip);
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
        gamePaused = true;
    }

    void ResumeGame()
    {
        musicManager.OnResumeGame();
        PlayClip(resumeClip);
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void LessTimeOn()
    {
        musicManager.PitchThis(1.58f);
    }

    public void LessTimeOff()
    {
        musicManager.PitchThis(1f);
    }

    void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void EndGame()
    {
        CancelInvoke("Stopper");
        if (win)
        {
            PlayClip(winClip);
            Debug.Log("You Win!!! Reload?");
        }
        else
        {
            PlayClip(loseClip);
            Debug.Log("You Lose!!! Reload?");
        }
    }
}
