using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveToJson : MonoBehaviour
{

    public void Save()
    {
        SaveData data = new SaveData();
        data.bricks = new BrickForJson[BrickManager.Instance.placedBricks.Count];

        for(int x=0; x< data.bricks.Length; x++)
        {
            data.bricks[x] = new BrickForJson 
            {
            
                size = (int)BrickManager.Instance.placedBricks[x].size,
                color = ColorUtility.ToHtmlStringRGB(BrickManager.Instance.placedBricks[x].color),
                level = BrickManager.Instance.placedBricks[x].level,
                finalPositionX = BrickManager.Instance.placedBricks[x].finalPosition.x,
                finalPositionY = BrickManager.Instance.placedBricks[x].finalPosition.normalized.y,
                finalPositionZ = BrickManager.Instance.placedBricks[x].finalPosition.z
            };
        }

        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
        //System.IO.File.WriteAllText(Application.persistentDataPath + "/BrickSave.json", json);
    }

}




[System.Serializable]
public class SaveData
{
    public BrickForJson[] bricks;
}

[System.Serializable] 
public class BrickForJson
{
    public int size;
    public string color;
    public int level;
    public float finalPositionX;
    public float finalPositionY;
    public float finalPositionZ;
}