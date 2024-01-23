using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ShapeHeadPenType
{
    InFront,
    Left,
    Right,
}


public class Pen : MonoBehaviour
{
    public SpriteRenderer colorBodyPen;
    public SpriteRenderer colorHeadPen;

    public SpriteRenderer  shapeHeadPen;
    public List<HeadPenType> lsHeadPenTypes;
    public HeadPenType headPenType(ShapeHeadPenType param)
    {
        foreach(var item in lsHeadPenTypes)
        {
            if(item.shapeHeadPenType == param)
            {
                return item;
            }
        }
        return null;
    }

    public Transform posSpawnDot;
    public bool spawnDot;
    [Header("=====Draw=====")]
    Vector2 mousePos;
    Vector2 objPos;

    public PenFollow controllerDot;
    public GameObject postMouse;
    public GameObject eraserFollow;
    public GameObject parentBodyPen;
    public Transform postMouseDown;
    public Transform postMouseUp;
    public Vector3 firstPost;
    public Vector3 secondPost;

    public void Init()
    {
     
    }

    public void Update()
    {
        if (GamePlayController.Instance.playerContain.penController.LockMovePen)
        {
            return;
        }
        if (GamePlayController.Instance.gameScene.lockFollowInUI)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
            {
                mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                objPos = Camera.main.ScreenToWorldPoint(mousePos);
                if(controllerDot == null)
                {
                    controllerDot = Instantiate(eraserFollow, objPos, Quaternion.identity).GetComponent<PenFollow>();
                    controllerDot.Init(this);
                    controllerDot.target = postMouse.transform;
                }    
            
                this.transform.position = new Vector3(objPos.x, objPos.y + 0.5f, 0);
                firstPost = objPos;
                HandlePenIn();
                if(GamePlayController.Instance.playerContain.levelData.colorCurrentLayer != null)
                {
                GamePlayController.Instance.playerContain.levelData.colorCurrentLayer.HandleOffLayerArena();
                }    
            }
            if (Input.GetMouseButton(0))
            {
               if (controllerDot == null)
             {
                controllerDot = Instantiate(eraserFollow, objPos, Quaternion.identity).GetComponent<PenFollow>();
                controllerDot.Init(this);
                  controllerDot.target = postMouse.transform;
              }
            if (GamePlayController.Instance.playerContain.levelData.colorCurrentLayer != null)
            {
                GamePlayController.Instance.playerContain.levelData.colorCurrentLayer.HandleOffLayerArena();
            }
            mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                objPos = Camera.main.ScreenToWorldPoint(mousePos);
                postMouse.transform.position = objPos;
                this.transform.position = new Vector3(objPos.x, objPos.y + 0.5f, 0);
                secondPost = objPos;

                if(secondPost.x > firstPost.x)
                {
                   shapeHeadPen.sprite = headPenType(ShapeHeadPenType.Left).shapeHeadPen;
                  shapeHeadPen.transform.position = headPenType(ShapeHeadPenType.Left).posShapeHeadPen.position;
                colorHeadPen.transform.position = headPenType(ShapeHeadPenType.Left).postColorHeadPen.position;
                colorHeadPen.transform.localEulerAngles = headPenType(ShapeHeadPenType.Left).postColorHeadPen.localEulerAngles;
                firstPost = secondPost;
              
               
                }
              if (secondPost.x < firstPost.x)
                {
                shapeHeadPen.sprite = headPenType(ShapeHeadPenType.Right).shapeHeadPen;
                shapeHeadPen.transform.position = headPenType(ShapeHeadPenType.Right).posShapeHeadPen.position;
                colorHeadPen.transform.position = headPenType(ShapeHeadPenType.Right).postColorHeadPen.position;
                colorHeadPen.transform.localEulerAngles = headPenType(ShapeHeadPenType.Right).postColorHeadPen.localEulerAngles;
                firstPost = secondPost;
            
            }
         
             }
 
            if(Input.GetMouseButtonUp(0))
           {
            shapeHeadPen.sprite = headPenType(ShapeHeadPenType.InFront).shapeHeadPen;
            shapeHeadPen.transform.position = headPenType(ShapeHeadPenType.InFront).posShapeHeadPen.position;
            colorHeadPen.transform.position = headPenType(ShapeHeadPenType.InFront).postColorHeadPen.position;
            colorHeadPen.transform.localEulerAngles = headPenType(ShapeHeadPenType.InFront).postColorHeadPen.localEulerAngles;
            HandlePenOut();
            }    
    }
    public void HandleMoveInFrontHeadPen()
    {
        shapeHeadPen.sprite = headPenType(ShapeHeadPenType.InFront).shapeHeadPen;
        shapeHeadPen.transform.position = headPenType(ShapeHeadPenType.InFront).posShapeHeadPen.position;
        colorHeadPen.transform.position = headPenType(ShapeHeadPenType.InFront).postColorHeadPen.position;
    }
    public void HandlePenIn()
    {
        this.transform.DOKill();
        this.transform.DOMove(postMouseDown.transform.position, 0.3f);
      


    }
    public void HandlePenOut()
    {
        this.transform.DOKill();
        this.transform.DOMove(postMouseUp.transform.position, 0.3f);

    }
    public void RemoveControDot()
    {   
        controllerDot.DisableDot();  
    }
    public void OffControDot()
    {
        controllerDot.DisableDot();
        controllerDot = null;
    }
    public void ChangeColor(Color color)
    {
        colorBodyPen.color = color;
        colorHeadPen.color = color;
        colorBodyPen.color -= new Color32(0,80,0,0);
    }    
}
[System.Serializable]
public class HeadPenType
{
    public ShapeHeadPenType shapeHeadPenType;
    public Sprite shapeHeadPen;
    public Transform posShapeHeadPen;
    public Transform postColorHeadPen;
}