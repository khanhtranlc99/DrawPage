using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class Layer_Color : LayerBase
{
  
    public SpriteRenderer layerColor;
    public GameObject layerArena;
    public int countTarget;
    public List<DotCheckComplete> lsTarget;
    public Color color_1;
    public Color color_2;
    public Color color_3;
    public SpriteRenderer layerPlus;

    [Header("=======================AfterMoveCame=============")]
    public bool isMoveAfterChooseColor;
    [ShowIf("isMoveAfterChooseColor", true)] public Transform postAfterChooseColor;
    public bool isZoomAfterChooseColor;
    [ShowIf("isZoomAfterChooseColor", true)] public float zoomValueAfterChooseColor;
    private Color tempColor;
    
    public override void Init()
    {
        if (isMove && isZoom)
        {
            GamePlayController.Instance.playerContain.cameraController.ZoomCame(zoomValue, delegate {

                GamePlayController.Instance.playerContain.cameraController.MoveCame(postMove.transform.position, delegate { SetUp(); });
            });
            return;
        }
        if (isMove)
        {
            GamePlayController.Instance.playerContain.cameraController.MoveCame(postMove.transform.position, delegate { SetUp(); });
            return;
        }

        if (isZoom)
        {
            GamePlayController.Instance.playerContain.cameraController.ZoomCame(zoomValue, delegate { SetUp(); });
            return;
        }
        if (!isMove && !isZoom)
        {
            SetUp();
        }
    }
    public void SetUp()
    {
        GamePlayController.Instance.playerContain.penController.currentPen.RemoveControDot();
        layerType = LayerType.Layer_Color;
        layerArena.gameObject.SetActive(true);
        isComplete = false;
        countTarget = 0;
        foreach (var item in lsTarget)
        {
            item.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }
        GamePlayController.Instance.gameScene.HandleOnOffpanelChooseColor(true);
        GamePlayController.Instance.gameScene.HandleSetColor(color_1, color_2, color_3);
        FadeMask();
       if(layerPlus != null)
        {
            layerPlus.color = new Color32(0, 0, 0, 0);
        }
    }
    private void FadeMask()
    {
        layerArena.GetComponentInChildren<SpriteRenderer>().DOColor(new Color32(0, 0, 0, 0), 0.75f).OnComplete(delegate {

            layerArena.GetComponentInChildren<SpriteRenderer>().DOColor(new Color32(255, 255, 255, 255), 0.75f).OnComplete(delegate {
                FadeMask();
            });
        } );
    }


    public override void CheckComplete()
    {
        countTarget += 1;
     
        if (countTarget >= lsTarget.Count)
        {
            isComplete = true;
            layerColor.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            if(layerPlus != null)
            {
                layerPlus.DOColor(new Color32(255,255,255,255),1);
            }
            if (GamePlayController.Instance.playerContain.levelData.NextIsColorFinishLayer(this))
            {
                 
                GamePlayController.Instance.playerContain.penController.currentPen.OffControDot();
                GamePlayController.Instance.playerContain.effectController.SpawnEmoji();      
                GamePlayController.Instance.playerContain.penController.MoveInOutPen(false, 0, delegate {

                    GamePlayController.Instance.gameScene.HandleShowPopupWin();
                });
            }    
            else
            {
                GamePlayController.Instance.gameScene.HandleOnOffBtnCompletLayer(true);
                GamePlayController.Instance.playerContain.effectController.SpawnEmoji();
            }    
      
        }

    }
    public override void HandleTouchLayer()
    {

        if (isMoveAfterChooseColor && isZoomAfterChooseColor)
        {

            GamePlayController.Instance.playerContain.cameraController.MoveCame(postAfterChooseColor.transform.position, delegate {

                GamePlayController.Instance.playerContain.cameraController.ZoomCame(zoomValueAfterChooseColor, delegate { HandleTouchChooeseColor(); });

                });
 
            return;
        }
        if (isMoveAfterChooseColor)
        {
            GamePlayController.Instance.playerContain.cameraController.MoveCame(postAfterChooseColor.transform.position, delegate { HandleTouchChooeseColor(); });
            return;
        }

        if (isZoomAfterChooseColor)
        {
            GamePlayController.Instance.playerContain.cameraController.ZoomCame(zoomValueAfterChooseColor, delegate { HandleTouchChooeseColor(); });
            return;
        }

        if (!isMoveAfterChooseColor && !isZoomAfterChooseColor)
        {
            HandleTouchChooeseColor();
        }

        void HandleTouchChooeseColor()
        {
      
            layerColor.gameObject.SetActive(true);

            GamePlayController.Instance.playerContain.penController.MoveInPen(layerArena.gameObject.transform.position, 0, delegate {
                var controDot = GamePlayController.Instance.playerContain.penController.currentPen.controllerDot;
                if (!controDot.gameObject.activeSelf)
                {
                    controDot.gameObject.SetActive(true);
                }

            });
        }
    }

    public override void HandleOffLayerArena()
    {
        layerColor.color = tempColor;
        layerArena.GetComponentInChildren<SpriteRenderer>().DOKill();
        layerArena.gameObject.SetActive(false);
    }
      
    public override void HandleChooseColorForColorLayer(Color color32)
    {
        tempColor = color32;
        GamePlayController.Instance.playerContain.penController.currentPen.ChangeColor(color32);
    }

    public override void HandleRedo()
    {
        if (GamePlayController.Instance.gameScene.panelChooseColor.activeSelf == true)
        {
            GamePlayController.Instance.gameScene.HandleOnOffpanelChooseColor(false);
         

        }
        //else
        //{
        //    GamePlayController.Instance.playerContain.penController.MoveInOutPen(false);
        //}
        GamePlayController.Instance.playerContain.penController.MoveInOutPen(true);
        GamePlayController.Instance.playerContain.penController.currentPen.RemoveControDot();
        layerArena.gameObject.SetActive(false);
        layerColor.gameObject.SetActive(false);

    }
}
