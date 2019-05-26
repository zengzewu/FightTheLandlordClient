using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : ManagerBase
{

    public static SceneMgr Instance;

    private void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        Add(SceneEvent.LOAD_SCENE, this);
    }

    private Action LoadSceneComplete;

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (LoadSceneComplete != null)
            LoadSceneComplete();
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case SceneEvent.LOAD_SCENE:
                {
                    SceneMsg sceneMsg = message as SceneMsg;
                    LoadScene(sceneMsg);
                }
                break;

            default:
                break;
        }
    }

    private void LoadScene(SceneMsg sceneMsg)
    {        
        LoadSceneComplete = sceneMsg.LoadComplete;
        SceneManager.LoadScene(sceneMsg.SceneIndex);
    }
}
