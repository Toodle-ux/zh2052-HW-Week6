using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ASCIILevelLoader : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    int goalNumber;
    
    public GameObject player;
    public GameObject wall;
    public GameObject goal;

    public string levelFileName;

    //public GameObject level;
    
    // Start is called before the first frame update
    void Start()
    {
        goalNumber = 0;
        LoadLevel();
        GameManager.instance.GoalNum = goalNumber;
        Debug.Log("goal number: " + goalNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevel()
    {
        //Destroy(level);
        //level = new GameObject("Level");

        string current_file_path = Application.dataPath +
                                   "/Levels/" +
                                   levelFileName;

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
                    //if (!newObj.name.Contains("Player"))
                    //{
                        //newObj.transform.parent = level.transform;
                    //}

                    newObj.transform.position =
                        new Vector3(x + xOffset, -y + yOffset, 0);
                }
            }
        }
    }
}
