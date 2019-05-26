using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMsg  {

	public int SceneIndex { get; set; }
    public string SceneName { get; set; }
    public  Action LoadComplete { get; set; }

    public SceneMsg()
    {
        SceneIndex = -1;
        SceneName = null;
        LoadComplete = null;
    }

    public SceneMsg(int sceneIndex,string sceneName,Action loadComplete)
    {
        this.SceneIndex = sceneIndex;
        this.SceneName = sceneName;
        this.LoadComplete = loadComplete;
    }
}
