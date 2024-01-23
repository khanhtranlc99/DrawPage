using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class Layer_Dot_Line : LayerBase
{

  /*  [HideInInspector]*/ public int countTarget;
    public List<DotCheckComplete> lsTarget;
    public SpriteRenderer layerDot;
    public SpriteRenderer layerLine;
    public Transform parentColliderCheck;
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
  
        if(!isMove && !isZoom)
        {
            SetUp();
        }     
        
    
    }
    public void SetUp()
    {

        layerType = LayerType.Layer_Dot_Line;
        layerLine.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        layerDot.gameObject.SetActive(true);
        layerLine.gameObject.SetActive(true);
        isComplete = false;
        countTarget = 0;
        foreach (var item in lsTarget)
        {
            item.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        }

        layerDot.color = new Color32(0, 0, 0, 0);
        layerDot.DOColor(Color.black, 1).OnComplete(delegate {
            var controDot = GamePlayController.Instance.playerContain.penController.currentPen.controllerDot;
            if (controDot == null)
            {
                return;
            }
            if (!controDot.gameObject.activeSelf)
            {
                controDot.gameObject.SetActive(true);
            }
        });
        foreach(var item in lsTarget )
        {
            item.isCheck = false;
        }    

        if (!GamePlayController.Instance.playerContain.penController.isPenIn)
        {
            GamePlayController.Instance.playerContain.penController.MoveInPen(layerDot.transform.position, 0.75f);
        }
        else
        {
         
        }
      
    }
    private void FadeMask()
    {
        layerDot.DOColor(new Color32(0, 0, 0, 0), 1).OnComplete(delegate {

            layerDot.DOColor(new Color32(255, 255, 255, 255), 1).OnComplete(delegate {
                FadeMask();
            });
        });
    }


    public override void CheckComplete()
    {
        countTarget += 1;
        if (countTarget >= lsTarget.Count)
        {
            isComplete = true;
            layerDot.gameObject.SetActive(false);
            layerDot.DOKill();
            layerLine.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            GamePlayController.Instance.playerContain.levelData.HandleNextLayer();
            GamePlayController.Instance.playerContain.penController.currentPen.RemoveControDot();
            GamePlayController.Instance.playerContain.effectController.SpawnEmoji();

        }
    }
    public override void HandleRedo()
    {
        GamePlayController.Instance.playerContain.penController.currentPen.RemoveControDot();
        layerDot.gameObject.SetActive(false);
        layerLine.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        layerLine.gameObject.SetActive(false);
        isComplete = false;
        countTarget = 0;
    }

}
