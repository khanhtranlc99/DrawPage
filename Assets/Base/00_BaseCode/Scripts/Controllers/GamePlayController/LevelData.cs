using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum LayerType
{
    Layer_Dot_Line,
    Layer_Color
}
public class LevelData : MonoBehaviour
{
    public Sprite iconLevel;
    public List<LayerBase> lsAllLayerInLevel;
  /*  [HideInInspector]*/ public LayerBase currentLayer;
    [HideInInspector] public LayerBase colorCurrentLayer;


    public LayerBase GetLayerNext
    {
        get
        {
            foreach(var item in lsAllLayerInLevel)
            {
                if(!item.isComplete)
                {
                    return item;
                }
            }
            return null;
        }
    }
    public bool NextIsColorFinishLayer(LayerBase param)
    {
        for (int i = 0; i < lsAllLayerInLevel.Count; i++)
        {
            if (lsAllLayerInLevel[i] == param)
            {
              if(i == lsAllLayerInLevel.Count -1)
                {
                    return true;
                }    
            }
        }
        return false;
    }
    public LayerBase GetLayerRedo
    {
        get
        {
           for(int i = 0; i < lsAllLayerInLevel.Count; i ++)
            {
                if(lsAllLayerInLevel[i] == currentLayer)
                {
                    if(lsAllLayerInLevel[i - 1] == null)
                    {
                        return null;
                    }
                    else
                    {
                        return lsAllLayerInLevel[i - 1];
                    }                 
                }
            }
            return null;
        }
    }



    public void Init ()
    {
        currentLayer = GetLayerNext;
        currentLayer.Init();

    }

    public void HandleNextLayer()
    {

        currentLayer = GetLayerNext;
        if(currentLayer != null)
        {
            currentLayer.Init();
            if (currentLayer.layerType == LayerType.Layer_Color)
            {
                colorCurrentLayer = currentLayer;
                GamePlayController.Instance.playerContain.penController.MoveInOutPen(false);
            }
         
         
            //   GamePlayController.Instance.playerContain.penController.MoveInOutPen(true);
        }
        else
        {
            Debug.LogError("Win");
        }
    }
    public void HandleRedoBooster()
    {
        var layer = GetLayerRedo;
       if (layer != null)
        {
            //if(GamePlayController.Instance.playerContain.penController.isPenIn)
            //{
            //    GamePlayController.Instance.playerContain.penController.MoveInOutPen(false);
            //}    
            //currentLayer.HandleRedo();         
            currentLayer.HandleRedo();
            currentLayer = layer;
            currentLayer.Init();
        }
    }
 
}
 
