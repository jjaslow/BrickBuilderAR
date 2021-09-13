using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BrickManager : MonoBehaviour
{

    public static BrickManager Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Brick> placedBricks;
    public List<Levels> placedLevels;

    private void Start()
    {
        placedBricks = new List<Brick>();
        placedLevels = new List<Levels>();
    }

    public void AddBrickToList(Brick newbrick)
    {
        newbrick.name = "Brick " + placedBricks.Count.ToString();

        placedBricks.Add(newbrick);
        UpdateBrickLevel(newbrick);
    }


    void UpdateBrickLevel(Brick newbrick)
    {
        int myLevel = newbrick.level;
        float mySize = .1f * ((float)newbrick.size + 1);

        //first brick on a level
        if (!placedLevels.Any(level => level.level == myLevel))
        {
            newbrick.finalPosition = placedBricks[0].transform.position + (.1f * myLevel * Vector3.up);

            Levels newbrickLevels = new Levels
            {
                level = myLevel,
                leftEdge = newbrick.finalPosition.x - (mySize / 2),
                rightEdge = newbrick.finalPosition.x + (mySize / 2),
            };

            placedLevels.Add(newbrickLevels);
            return;
        }

        //to the right
        if(newbrick.transform.position.x > placedBricks[0].transform.position.x)
        {
            newbrick.finalPosition = placedBricks[0].transform.position;
            newbrick.finalPosition.x = placedLevels[myLevel].rightEdge + (mySize / 2);
            newbrick.finalPosition += (.1f * myLevel * Vector3.up);
            placedLevels[myLevel].rightEdge += mySize;

        }

        //to the left
        if (newbrick.transform.position.x < placedBricks[0].transform.position.x)
        {
            newbrick.finalPosition = placedBricks[0].transform.position;
            newbrick.finalPosition.x = placedLevels[myLevel].leftEdge - (mySize / 2);
            newbrick.finalPosition += (.1f * myLevel * Vector3.up);
            placedLevels[myLevel].leftEdge -= mySize;
        }

    }

    [System.Serializable]
    public class Levels
    {
        public int level;
        public float leftEdge;
        public float rightEdge;
    }


}


