using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControllerScript : MonoBehaviour {
    public GameObject player;
    public AudioClip finishedSound;
    public AudioSource successSoundSource;
    
    private string myPosition;
    private int numberOfSteps;
    List<KeyValuePair<float, float>> controlPoints = new List<KeyValuePair<float, float>>();
    List<string> strs = new List<string>();

    int currentPoint;

    // Use this for initialization
    void Start() {
        controlPoints.Add(new KeyValuePair<float, float>(234, 579));
        controlPoints.Add(new KeyValuePair<float, float>(270, 475));
        controlPoints.Add(new KeyValuePair<float, float>(320, 371));
        controlPoints.Add(new KeyValuePair<float, float>(421, 296));
        controlPoints.Add(new KeyValuePair<float, float>(247, 110));
        strs.Add("Следущая точка: азимут 190, ориентир развилка");
        strs.Add("Точка пройдена, следущая точка: азимут 230, ориентир валун");
        strs.Add("Точка пройдена, следущая точка: азимут 265, ориентир высохшее дерево");
        strs.Add("Точка пройдена, следущая точка: азимут 90, грунтовая дорога");
        numberOfSteps = 0;
        
        currentPoint = 0;
        InvokeRepeating("UpdatePlayerPosition", 0, 1);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(140, 30, 400, 200), myPosition);
    }

    void UpdatePlayerPosition() {
        Debug.Log(controlPoints.Count);
        numberOfSteps++;
        Vector3 position = player.transform.position;
        if (currentPoint < controlPoints.Count) {
            if (inRadius(position.x, position.z)) {
                currentPoint++;
                Debug.Log("INCREMENTED: " + currentPoint);
                if (currentPoint == controlPoints.Count) {
                    myPosition = "Задание выполнено. Ваше время "+ Time.time;
                    successSoundSource.clip = finishedSound;
                    successSoundSource.Play();
                    return;
                } else {
                    successSoundSource.Play();
                    Debug.Log("Move to the next point. Next point: " + controlPoints[currentPoint]);
                }
            }
            //myPosition = "CURRENT POSITION:\n\t" + position + ".\nCURRENT TARGET POINT:\n\t" + controlPoints[currentPoint].Key + " " + controlPoints[currentPoint].Value;
            myPosition = strs[currentPoint - 1];
        }
    }

    bool inRadius(float x, float z) {
        float x1 = controlPoints[currentPoint].Key;
        float z1 = controlPoints[currentPoint].Value;
        double dist = Mathf.Sqrt(Mathf.Pow(x1 - x, 2) + Mathf.Pow(z1 - z, 2));
        return dist < 10.0;
    }
}
