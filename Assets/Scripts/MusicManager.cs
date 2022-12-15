using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    double pauseClipTime = 0;
    public AudioClip[] clips;
    int actualClip = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    private void Update()
    {
        if(audioSource.time >= clips[actualClip].length)
        {
            actualClip++;
            if(actualClip > clips.Length - 1)
            {
                actualClip = 0;
            }
            audioSource.clip = clips[actualClip];
            audioSource.Play();
        }
    }

    public void OnPauseGame()
    {
        pauseClipTime = audioSource.time;
        audioSource.Pause();
    }

    public void OnResumeGame()
    {
        audioSource.PlayScheduled(pauseClipTime);
        pauseClipTime = 0;
    }

    public void PitchThis(float pitch)
    {
        audioSource.pitch = pitch;
    }
}
