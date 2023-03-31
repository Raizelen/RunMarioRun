using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip menuBG, gameBg;

    public List<AudioClip> audioClips;

    public AudioSource bgSource , gameAudioSource;


    public void PlayAudio(int index)
    {
        gameAudioSource.PlayOneShot(audioClips[index]);
    }

    public void PlayGameBG()
    {
        bgSource.Stop();
        bgSource.PlayOneShot(gameBg);
    }




}
