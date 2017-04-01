using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControllerScript : MonoBehaviour {
	
    public GameObject player;
    public AudioClip finishedSound;
    public AudioSource successSoundSource;
	public Transform playerTransform;
    
    private string myPosition;
    private int numberOfSteps;
    List<KeyValuePair<float, float>> controlPoints;
	List<Instruction> targets;
    int currentPoint;
    private int resultTime;

	private GUIStyle instructionNameStyle;
	private GUIStyle instructionTextStyle;

	private const string nextPointStr = "Целевой азимут: ";
	private const string landmarkStr = "Ориентир: ";
	private const string azimuthStr = "Текущий азимут: ";
	private const string taskCompletedStr = "Задание выполнено. Ваше время: ";

	private Instruction instruction;

	internal class Instruction
	{
		public int DestinationAzimuth { get; set; }

		public int CurrentAzimuth { get; set; }

		public string LandmarkName { get; set; }

		public bool TaskCompleted { get; set; }

		public Instruction()
		{
		}
	}

    // Use this for initialization
    void Start() {
        //playerTransform = player.transform;
		controlPoints = new List<KeyValuePair<float, float>>();
		instruction = new Instruction();
		instructionNameStyle = new GUIStyle();
		instructionNameStyle.fontSize = 14;
		instructionNameStyle.normal.textColor = Color.white;

		instructionTextStyle = new GUIStyle();
		instructionTextStyle.fontSize = 14;
		instructionTextStyle.normal.textColor = Color.green;

        controlPoints.Add(new KeyValuePair<float, float>(234, 579));
        controlPoints.Add(new KeyValuePair<float, float>(270, 475));
        controlPoints.Add(new KeyValuePair<float, float>(320, 371));
        controlPoints.Add(new KeyValuePair<float, float>(421, 296));
        controlPoints.Add(new KeyValuePair<float, float>(257, 120));

		FillTargets();

        numberOfSteps = 0;
        currentPoint = 0;

        InvokeRepeating("UpdatePlayerPosition", 0, 1);
    }

	private void FillTargets()
	{
		targets = new List<Instruction>();

		targets.Add (
			new Instruction {
				DestinationAzimuth = 190, 
				LandmarkName = "развилка"
			});
		targets.Add (
			new Instruction {
				DestinationAzimuth = 230, 
				LandmarkName = "валун"
			});
		targets.Add (
			new Instruction {
				DestinationAzimuth = 240, 
				LandmarkName = "высохшее дерево"
			});
		targets.Add (
			new Instruction {
				DestinationAzimuth = 140, 
				LandmarkName = "грунтовая дорога"
			});
	}
		

    // Update is called once per frame
    void Update() {
    }
    
	private void UpdateCurrentAzimuth()
	{
		int tmp = 360 - (int)playerTransform.eulerAngles.y;
		if (tmp == 360)
		{
			tmp = 0;
		}
		instruction.CurrentAzimuth = tmp;
	}
    private int AzimuthAngle()
    {
        Vector3 pos = player.transform.position;
        float x = controlPoints[currentPoint].Key - pos.x;
        float z = controlPoints[currentPoint].Value - pos.z;
        float Cos = z / Mathf.Sqrt(x * x + z * z);
        float angle = Mathf.Acos(Cos)*180/Mathf.PI;
        
        if (x >= 0)
        {
            return 360 - (int)angle;
        }
        else
        {
            return (int)angle;
        }
    }

    private void OnGUI()
    {
		if (!instruction.TaskCompleted) {
			GUILayout.BeginArea(new Rect(90, 250, 200, 200));
			//ShowSingleInstruction (nextPointStr, instruction.DestinationAzimuth.ToString());
            ShowSingleInstruction(nextPointStr,AzimuthAngle().ToString() );
            ShowSingleInstruction (landmarkStr, instruction.LandmarkName.ToString());
            UpdateCurrentAzimuth();
            ShowSingleInstruction (azimuthStr, instruction.CurrentAzimuth.ToString());
			GUILayout.EndArea ();
		}
		else 
		{
            int min = resultTime / 60;
            int sec = resultTime % 60;
            string strToShow = min + ":" + sec+":00";
			ShowSingleInstruction (taskCompletedStr, /*Time.time.ToString()*/strToShow);
		}
    }

	private void ShowSingleInstruction(string instructionName, string instructionText)
	{
		GUILayout.BeginHorizontal ("box");
		GUILayout.Label (instructionName, instructionNameStyle);
		GUILayout.Label (instructionText, instructionTextStyle);
		GUILayout.EndHorizontal();
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
					instruction.TaskCompleted = true;
                    resultTime = (int)Time.time;
                    successSoundSource.clip = finishedSound;
                    successSoundSource.Play();
                    return;
                } else {
                    successSoundSource.Play();
                    Debug.Log("Move to the next point. Next point: " + controlPoints[currentPoint]);
                }
            }
            //myPosition = "CURRENT POSITION:\n\t" + position + ".\nCURRENT TARGET POINT:\n\t" + controlPoints[currentPoint].Key + " " + controlPoints[currentPoint].Value;
			instruction = targets[currentPoint - 1];
        }
    }

    bool inRadius(float x, float z) {
        float x1 = controlPoints[currentPoint].Key;
        float z1 = controlPoints[currentPoint].Value;
        double dist = Mathf.Sqrt(Mathf.Pow(x1 - x, 2) + Mathf.Pow(z1 - z, 2));
        return dist < 10.0;
    }
}
