using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControllerScript : MonoBehaviour {
    public GameObject player;
    public AudioSource successSoundSource;
    private string myPosition;

    List<KeyValuePair<float, float>> controlPoints = new List<KeyValuePair<float, float>>();
    int currentPoint;

    // Use this for initialization
    void Start() {
        controlPoints.Add(new KeyValuePair<float, float>(234, 579));
        controlPoints.Add(new KeyValuePair<float, float>(270, 475));
        controlPoints.Add(new KeyValuePair<float, float>(432, 511));
        controlPoints.Add(new KeyValuePair<float, float>(563, 421));
        controlPoints.Add(new KeyValuePair<float, float>(262, 134));
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
        Vector3 position = player.transform.position;
        if (currentPoint < controlPoints.Count) {
            if (inRadius(position.x, position.z)) {
                currentPoint++;
                successSoundSource.Play();
                Debug.Log("INCREMENTED: " + currentPoint);
                if (currentPoint == controlPoints.Count) {
                    myPosition = "You have finished the task!";
                    return;
                } else {
                    Debug.Log("Move to the next point. Next point: " + controlPoints[currentPoint]);
                }
            }
            myPosition = "CURRENT POSITION:\n\t" + position + ".\nCURRENT TARGET POINT:\n\t" + controlPoints[currentPoint].Key + " " + controlPoints[currentPoint].Value;
        }
    }

    bool inRadius(float x, float z) {
        float x1 = controlPoints[currentPoint].Key;
        float z1 = controlPoints[currentPoint].Value;
        double dist = Mathf.Sqrt(Mathf.Pow(x1 - x, 2) + Mathf.Pow(z1 - z, 2));
        return dist < 5.0;
    }
}
