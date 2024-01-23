using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public PenController penController;
    public CameraController cameraController;
    public EffectController effectController;
  
    public void Init()
    {
        LoadLevel();
       // levelData.Init();
        penController.Init();
        cameraController.Init();
    }

    public void LoadLevel()
    {
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL + UseProfile.CurrentLevel.ToString();
        levelData = Instantiate(Resources.Load<LevelData>(pathLevel));
        levelData.Init();
        Debug.LogError("LoadLevel " + pathLevel);
    }    
    

}
