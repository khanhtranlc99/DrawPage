using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;

public class ToolsTest : SerializedMonoBehaviour
{
    public GameObject[] toolsTranform;
    public List<GameObject> toolsConvert;
    public GameObject spritePrefap;
    public GameObject levelData;
    public GameObject layer_Dot_Line;
    public GameObject layer_Color;
    public GameObject currentLevelData;
    public GameObject dotCheckCollider;
    [Header("==============================")]
    public GameObject tempSpirte;
    public GameObject dot;
    public List<Color> lsColorRandom;
    public bool shameShapeStroke;

  
    public Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera.main, out var result);
        return result;
    }


    public List<Layer_Dot_Line> templayerDotLine;
    public List<Sprite> lsTempSpriteLine;
 
    [Button]
    private void SetUp()
    {

         if(currentLevelData == null)
        {
            currentLevelData = Instantiate(levelData);
            templayerDotLine = new List<Layer_Dot_Line>();
            lsTempSpriteLine = new List<Sprite>();
          
        }
        shameShapeStroke = CheckDotShape;
        for (int i = 0 ; i < toolsTranform.Length; i++  )
        {
            if (toolsTranform[i].name.Contains("Dot")  )
            {
                RectTransform rectTransform = toolsTranform[i].GetComponent<RectTransform>();
                var layerDot = Instantiate(layer_Dot_Line, currentLevelData.transform.GetChild(0).transform).GetComponent<Layer_Dot_Line>();
                layerDot.name = "LayerDot_Line";
                layerDot.layerDot.sprite = toolsTranform[i].GetComponent<Image>().sprite;
                layerDot.layerDot.gameObject.AddComponent<PolygonCollider2D>();
                layerDot.layerDot.transform.position = GetWorldPositionOfCanvasElement(rectTransform);
                HandleTest(layerDot.layerDot.gameObject, layerDot.parentColliderCheck);
                layerDot.parentColliderCheck.position = layerDot.layerDot.gameObject.transform.position;
                layerDot.parentColliderCheck.parent = layerDot.layerDot.transform;            
                layerDot.layerLine.transform.position = layerDot.layerDot.transform.position;
                layerDot.layerLine.GetComponent<SpriteRenderer>().color = Color.black;
                currentLevelData.GetComponent<LevelData>().lsAllLayerInLevel.Add(layerDot);
                templayerDotLine.Add(layerDot);
                toolsTranform[i].SetActive(false);
                layerDot.layerLine.gameObject.SetActive(true);

             //   layerDot.layerDot.gameObject.SetActive(true);
            }
            if (toolsTranform[i].name.Contains("Sample"))
            {
                currentLevelData.GetComponent<LevelData>().iconLevel = toolsTranform[i].GetComponent<Image>().sprite;
            }    


        }
        for (int i = 0; i < toolsTranform.Length; i++)
        {
            
            if (toolsTranform[i].name.Contains("Shape"))
            {
                RectTransform rectTransformShape = toolsTranform[i].GetComponent<RectTransform>();
                var layerColor = Instantiate(layer_Color, currentLevelData.transform.GetChild(0).transform).GetComponent<Layer_Color>();
                layerColor.name = "LayerDot_Color";
                layerColor.layerColor.sprite = toolsTranform[i].GetComponent<Image>().sprite;
                layerColor.layerColor.transform.position = GetWorldPositionOfCanvasElement(rectTransformShape);
                layerColor.layerArena.GetComponent<SpriteMask>().sprite = toolsTranform[i].GetComponent<Image>().sprite;
                layerColor.layerArena.transform.position = layerColor.layerColor.transform.position;

                var templistColor = new List<Color>();
                var templistColorDone = new List<Color>();
                templistColor.Add(layerColor.color_1);
                templistColor.Add(layerColor.color_2);
                templistColor.Add(layerColor.color_3);

                var strColor = toolsTranform[i].name;
                int hashIndex = strColor.IndexOf("#");
            
                if (hashIndex != -1 && hashIndex < strColor.Length - 1)
                {
                 
                    string charactersAfterHash = strColor.Substring(hashIndex + 1);
                    var random = Random.Range(0, templistColor.Count);
                    templistColor[random] = HexToColor(charactersAfterHash);
                    templistColorDone.Add(templistColor[random]);
                    templistColor.RemoveAt(random);

                    for(int j = 0; j < templistColor.Count; j ++)
                    {
                        templistColor[j] = GetColorRandom(HexToColor(charactersAfterHash));
                        templistColorDone.Add(templistColor[j]);
                    }


                }
                layerColor.color_1 = templistColorDone[0];
                layerColor.color_2 = templistColorDone[1];
                layerColor.color_3 = templistColorDone[2];



                toolsTranform[i].SetActive(false);
                currentLevelData.GetComponent<LevelData>().lsAllLayerInLevel.Add(layerColor);
             

            }
        }

        for (int i = 0; i < toolsTranform.Length; i++)
        {
            if (toolsTranform[i].name.Contains("Stroke"))
            {
                 
                  
                    lsTempSpriteLine.Add(toolsTranform[i].GetComponent<Image>().sprite);
                    toolsTranform[i].SetActive(false);
                 
            }
        }

        for (int i = 0; i < lsTempSpriteLine.Count; i++)
        {
            templayerDotLine[i].layerLine.sprite = lsTempSpriteLine[i];
        }

     

        if(shameShapeStroke)
        {
            HandleDuplicate();
        }    
        


    }


    public void HandleDuplicate()
    {
        var tempLayerColor = new List<Layer_Color>();
        var tempLayerDot_Line = new List<Layer_Dot_Line>();
        var temp = currentLevelData.GetComponent<LevelData>();
        foreach (var item in temp.lsAllLayerInLevel)
        {
            if(item.layerType == LayerType.Layer_Color)
            {
                tempLayerColor.Add(item.GetComponent<Layer_Color>());
            }
            if (item.layerType == LayerType.Layer_Dot_Line)
            {
                tempLayerDot_Line.Add(item.GetComponent<Layer_Dot_Line>());
            }
        }
        for(int i = 0; i < tempLayerDot_Line.Count; i ++)
        {
            CopyDot(tempLayerDot_Line[i].parentColliderCheck.gameObject, tempLayerColor[i]);
        }


    }    
    void CopyDot(GameObject param, Layer_Color layer_Color)
    {
        var temp = param;
        var copy = Instantiate(temp, layer_Color.layerColor.transform);
        var listDot = copy.GetComponentsInChildren<DotCheckComplete>();
        foreach(var item in listDot)
        {
            layer_Color.lsTarget.Add(item);
        }    


    }
    Color HexToColor(string hex)
    {
        Color color = new Color();

        // Loại bỏ dấu # (nếu có)
        hex = hex.Replace("#", "");

        // Chuyển đổi từ hex sang giá trị RGB
        if (ColorUtility.TryParseHtmlString("#" + hex, out color))
        {
            return color;
        }
        else
        {
            // Trả về màu trắng nếu không thành công
            Debug.LogWarning("Không thể chuyển đổi mã màu hex.");
            return Color.white;
        }
    }

    public Color GetColorRandom(Color param)
    {
        lsColorRandom.Shuffle();
        foreach (var item in lsColorRandom)
        {
            if(item != param)
            {
                return item;
            }
        }
        return Color.white;
    }    

    private void HandleTest(GameObject param, Transform param2)
    {
      
        PolygonCollider2D temp = param.GetComponent<PolygonCollider2D>() ;
        for (int i = 0; i < temp.pathCount; i++)
        {
            Vector2[] temp_2 = new Vector2[0];
            temp_2 = temp.GetPath(i);
            foreach (Vector2 point in temp_2)
            {
              var dotplus =  Instantiate(dot, new Vector3(point.x, point.y, 0), Quaternion.identity).GetComponent<DotCheckComplete>();
                dotplus.transform.parent = param2.transform;
                param.GetComponentInParent<Layer_Dot_Line>().lsTarget.Add(dotplus);
                break;
            }


        }
      
        Debug.LogError("HandleTest " + temp.pathCount);
    }
    
    public bool CheckDotShape
    {
        get
        {
            var tempDot = 0;
            var tempShape = 0;
            for (int i = 0; i < toolsTranform.Length; i++)
            {
                if (toolsTranform[i].name.Contains("Dot"))
                {
                    tempDot++;
                }
                if (toolsTranform[i].name.Contains("Shape"))
                {
                    tempShape++;
                }
            }
            Debug.LogError("tempDot " + tempDot + " tempShape " + tempShape);
 
            if (tempDot == tempShape)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
    }
    [Button]
    public void HandleOffLayerLine()
    {
        var temp = currentLevelData.GetComponent<LevelData>();
        foreach (var item in temp.lsAllLayerInLevel)
        {
            if (item.layerType == LayerType.Layer_Dot_Line)
            {
                item.GetComponent<Layer_Dot_Line>().layerLine.gameObject.SetActive(false);
            }
            
        }
     
    }
}
