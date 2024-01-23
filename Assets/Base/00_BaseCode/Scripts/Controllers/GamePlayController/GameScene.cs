using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    public Text tvLevel;
    public Image iconLevel;
    public Button btnCompleteLayer;
    public Button btnRedo;
    public GameObject panelChooseColor;
    public Button btnChooseColor_1;
    public Button btnChooseColor_2;
    public Button btnChooseColor_3;
    public Image Imgcolor_1;
    public Image Imgcolor_2;
    public Image Imgcolor_3;

    public Color color_1;
    public Color color_2;
    public Color color_3;
    public bool lockFollowInUI;

    public Button btnNext;
    public GameObject panelTempWin;
    public CanvasGroup canvasGroup;
    public bool clickChooseColor;
    public void Init()
    {
        tvLevel.text = "Level " + UseProfile.CurrentLevel;
        btnCompleteLayer.onClick.AddListener(HandleCompleteLayer);
        btnChooseColor_1.onClick.AddListener(HandleCompleteColorLayer_1);
        btnChooseColor_2.onClick.AddListener(HandleCompleteColorLayer_2);
        btnChooseColor_3.onClick.AddListener(HandleCompleteColorLayer_3);
        btnRedo.onClick.AddListener(GamePlayController.Instance.playerContain.levelData.HandleRedoBooster);
        btnNext.onClick.AddListener(delegate { SceneManager.LoadScene("GamePlay"); });
        iconLevel.sprite = GamePlayController.Instance.playerContain.levelData.iconLevel;
        clickChooseColor = false;
    }

    public void HandleCompleteColorLayer_1()
    {
        if(clickChooseColor)
        {
            return;
        }
        clickChooseColor = true;
        var temp = GamePlayController.Instance.playerContain.levelData;
        temp.colorCurrentLayer.HandleChooseColorForColorLayer(color_1);
        temp.colorCurrentLayer.HandleTouchLayer();
        HandleOnOffpanelChooseColor(false);
        Debug.LogError("HandleCompleteColorLayer_1");
    }
    public void HandleCompleteColorLayer_2()
    {
        if (clickChooseColor)
        {
            return;
        }
        clickChooseColor = true;
        var temp = GamePlayController.Instance.playerContain.levelData;
        temp.colorCurrentLayer.HandleChooseColorForColorLayer(color_2);
        temp.colorCurrentLayer.HandleTouchLayer();
        HandleOnOffpanelChooseColor(false);
        Debug.LogError("HandleCompleteColorLayer_2");
    }
    public void HandleCompleteColorLayer_3()
    {
        if (clickChooseColor)
        {
            return;
        }
        clickChooseColor = true;
        var temp = GamePlayController.Instance.playerContain.levelData;
        temp.colorCurrentLayer.HandleChooseColorForColorLayer(color_3);
        temp.colorCurrentLayer.HandleTouchLayer();
        HandleOnOffpanelChooseColor(false);
        Debug.LogError("HandleCompleteColorLayer_2");
    }

    public void  HandleSetColor(Color param1, Color param2, Color param3)
    {
        color_1 = param1;
        color_2 = param2;
        color_3 = param3;
        Imgcolor_1.color = param1;
        Imgcolor_2.color = param2;
        Imgcolor_3.color = param3;
       
    }
    public void HandleCompleteLayer()
    {     
        GamePlayController.Instance.playerContain.levelData.HandleNextLayer();
        HandleOnOffBtnCompletLayer(false);
      
    }  
    
    public void HandleOnOffBtnCompletLayer(bool param)
    {
        btnCompleteLayer.gameObject.SetActive(param);
    }

    public void HandleOnOffpanelChooseColor(bool param)
    {
        panelChooseColor.SetActive(param);
        if(param == true)
        {
            clickChooseColor = false;
        }    
        Debug.LogError("HandleOnOffpanelChooseColor");
    }
    public void OnEnterUI()
    {
        lockFollowInUI = true;
    }
    public void OnExitUI()
    {
        lockFollowInUI = false;
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
     
    }
    public void HandleShowPopupWin()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(delegate {
            UseProfile.CurrentLevel += 1;
            panelTempWin.SetActive(true);
        });
       
    }
}
