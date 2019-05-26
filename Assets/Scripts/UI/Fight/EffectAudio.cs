using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAudio : AudioBase {

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        Bind(AudioEvent.PLAY_EFFECTAUDIO);
    }

    public override void Execute(int eventCode, object message)
    {
        switch(eventCode)
        {
            case AudioEvent.PLAY_EFFECTAUDIO:
                PlayEffect(message.ToString());
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="effectAuioName"></param>
    private void PlayEffect(string effectAuioName)
    {       
        AudioClip ac = Resources.Load<AudioClip>("Sound/"+effectAuioName);
        audioSource.Stop();        
        audioSource.clip = ac;
        audioSource.Play();
    }
}
