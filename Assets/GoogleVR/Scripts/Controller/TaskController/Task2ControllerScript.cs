using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Task2ControllerScript : MonoBehaviour {

    public GameObject player;
    public Transform playerTransform;
    private List<KeyValuePair<float, float>> controlPoints=new List<KeyValuePair<float, float>>();
    private bool isFinished = false;
    private GUIStyle instructionNameStyle;
    private GUIStyle instructionTextStyle;
    private int result;
    private float finishTime;
    // Use this for initialization
    void Start () {
        //Debug.Log("start");
        controlPoints.Add(new KeyValuePair<float, float>(30.327f, 106.3134f));
        controlPoints.Add(new KeyValuePair<float, float>(101.3981f, 106.05f));
        controlPoints.Add(new KeyValuePair<float, float>(97.173f, 25.377f));
        controlPoints.Add(new KeyValuePair<float, float>(19.011f, 15.802f));
        instructionNameStyle = new GUIStyle();
        instructionNameStyle.fontSize = 14;
        instructionNameStyle.normal.textColor = Color.white;

        instructionTextStyle = new GUIStyle();
        instructionTextStyle.fontSize = 14;
        instructionTextStyle.normal.textColor = Color.green;

    }

    // Update is called once per frame
    void Update () {
        UpdatePlayerPosition();
	}
    void UpdatePlayerPosition()
    {
        //Debug.Log("update");
        Vector3 position = player.transform.position;
        //Debug.Log(position.ToString());
        
        
        if (!isFinished)
        {
            
            for(int i = 0; i < 4; i++)
            {
                if (inRadius(position.x, position.z, i))
                {
                    result = i;
                    isFinished = true;
                    finishTime = Time.time;
                    //Debug.Log("finish");
                    
                    break;
                }
            }
            
        }
        
        
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(90, 250, 300, 100));
        if(!isFinished)
        {
            ShowSingleInstruction("Найдите север и идите к соответствующему флагу.");
            ShowSingleInstruction("Текущее время:полдень ");

        }
        else
        {
            string resStr;
            if (result == 0)
            {
                resStr = "Задание успешно завершено";

            }
            else
            {
                resStr = "Вы допустили ошибку";
            }
            ShowSingleInstruction(resStr);
//Debug.Log(finishTime + " " + Time.time);
            if(Time.time-finishTime>5.0f)
            {

                SceneManager.LoadScene(0);
            }
        }
        GUILayout.EndArea();
    }
    bool inRadius(float x, float z,int i)
    {
        float x1 = controlPoints[i].Key;
        float z1 = controlPoints[i].Value;
        double dist = Mathf.Sqrt(Mathf.Pow(x1 - x, 2) + Mathf.Pow(z1 - z, 2));
        //Debug.Log(i + " " + dist.ToString());
        return dist < 5.0;
    }
    private void ShowSingleInstruction(string instructionName)
    {
        GUILayout.BeginHorizontal("box");
        GUILayout.Label(instructionName, instructionNameStyle);
        //GUILayout.Label(instructionText, instructionTextStyle);
        GUILayout.EndHorizontal();
    }
}
