using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text timeText;
    public Text goldKeyText;
    public Text redKeyText;
    public Text greenKeyText;
    public Text crystalText;
    public Image snowFlake;

    public GameObject infoPanel;
    public Text pauseEnd;
    public Text reloadInfo;
    public Text useInfo;

    public void AddPoints(int points)
    {
        Debug.Log(points);
        this.points += points;
        crystalText.text = points.ToString();
    }

    public void AddTime(int addTime)
    {
        timeToEnd += addTime;
        timeText.text = timeToEnd.ToString();
    }

    public void AddKey(KeyColor color)
    {
        if (color == KeyColor.Gold)
        {
            goldKey++;
            goldKeyText.text = goldKey.ToString();
        }
        else if (color == KeyColor.Green)
        {
            greenKey++;
            greenKeyText.text = greenKey.ToString();
        }
        else if (color == KeyColor.Red)
        {
            redKey++;
            redKeyText.text = redKey.ToString();
        }
    }

    public void FreezTime(int freez)
    {
        CancelInvoke("Stopper");
        snowFlake.enabled = true;
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
        snowFlake.enabled = false;
        timeText.text = timeToEnd.ToString();
        infoPanel.SetActive(false);
        pauseEnd.text = "Pause";
        reloadInfo.text = "";
        SetUseInfo("");
        LessTimeOff();
        
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
        timeText.text = timeToEnd.ToString();
        snowFlake.enabled = false;
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
        if(!endGame)
        {
            PlayClip(pauseClip);
            musicManager.OnPauseGame();
            infoPanel.SetActive(false);
            Time.timeScale = 0f;
            gamePaused = true;
        }
    }

    void ResumeGame()
    {
        if(!endGame)
        {
            PlayClip(resumeClip);
            musicManager.OnResumeGame();
            infoPanel.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
        }
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
        infoPanel.SetActive(false);
        if (win)
        {
            PlayClip(winClip);
            pauseEnd.text = "You win!!!";
        }
        else
        {
            PlayClip(loseClip);
            pauseEnd.text = "You lose!!!";
        }
        reloadInfo.text = "Reload? Y/N";
    }

    public void SetUseInfo(string info)
    {
        useInfo.text = info;
    }
}
