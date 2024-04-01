using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioTracks;
    [SerializeField] private int currentTrack;
    [SerializeField] private bool audioCanBePlayed;



    void Start()
    {
        
    }

    private void Update()
    {
        if (audioCanBePlayed)
        {
            if (!audioTracks[currentTrack].isPlaying)
            {
                audioTracks[currentTrack].Play();
            }
            else
            {
                audioTracks[currentTrack].Stop();
            }
        }
    }

    public void PlayNewTrack (int newTrack)
    {
        audioTracks[currentTrack].Stop();
        currentTrack = newTrack;
        audioTracks[currentTrack].Play();
    }

    public void StopCurrentTrack ()
    {
        audioTracks[currentTrack].Stop();
    }

}
