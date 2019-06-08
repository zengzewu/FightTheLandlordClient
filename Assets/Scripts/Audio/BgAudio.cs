using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAudio : AudioBase {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Bind(AudioEvent.PLAY_BACKGROUND,AudioEvent.SET_VOLUME,AudioEvent.STOP_BACKGROUND);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case AudioEvent.SET_VOLUME:
                SetVolume((float)message);
                break;
            case AudioEvent.STOP_BACKGROUND:
                Stop();
                break;
            case AudioEvent.PLAY_BACKGROUND:
                PlayBg();
                break;
            default:
                break;
        }
    }

    [SerializeField]
    private AudioSource audioSource;

    private void PlayBg()
    {
        audioSource.Play();
    }

    private void Stop()
    {
        audioSource.Stop();
    }

    private void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
