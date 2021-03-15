using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ASCIILevelLoader : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    // the number of goals in the scene
    int goalNumber;
    
    public GameObject player;
    public GameObject wall;
    public GameObject goal;

    public string levelFileName;

    public GameObject level;

    public int currentLevel = 0;

    // load level when level changes
    public int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            // reset the goal number to -1 so that it won't equal to the activated number directly
            GameManager.instance.GoalNum = -1;
            LoadLevel();
            GameManager.instance.GoalNum = goalNumber;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
        GameManager.instance.GoalNum = goalNumber;
    }

    // Update is called once per frame
    void Update()
    {
        //reset the level by pressing R
        if (Input.GetKey(KeyCode.R))
        {
            LoadLevel();
            GameManager.instance.GoalNum = goalNumber;
        }
    }

    void LoadLevel()
    {
        // when load a new level, destroy all the existing game objects
        Destroy(level);
        level = new GameObject("Level");

        // reset the goal number
        goalNumber = 0;
        
        Debug.Log("goal number: " + goalNumber);
       
        // load the level file accordingly
        string current_file_path = Application.dataPath +
                                   "/Levels/" +
                                   levelFileName.Replace(
                                       "Num",
                                       currentLevel + "");

        string[] fileLines = File.ReadAllLines(current_file_path);

        for (int y = 0; y < fileLines.Length; y++)
        {
            string lineText = fileLines[y];

            char[] characters = lineText.ToCharArray();

            for (int x = 0; x < characters.Length; x++)
            {
                char c = characters[x];

                GameObject newObj;

                switch (c)
                {
                    case 'p':
                        newObj = Instantiate<GameObject>(player);
                        break;
                    case 'w':
                        newObj = Instantiate<GameObject>(wall);
                        break;
                    case '&':
                        newObj = Instantiate<GameObject>(goal);
                        // count the number of goals
                        goalNumber++;
                        break;
                    default:
                        newObj = null;
                        break;
                }

                if (newObj != null)
                {
                    // set the game object level as all objects parent
                    newObj.transform.parent = level.transform;
                    newObj.transform.position =
                        new Vector3(x + xOffset, -y + yOffset, 0);
                }
            }
        }
    }
}
