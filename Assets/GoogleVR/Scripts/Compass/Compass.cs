using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    public Vector3 NorthDirection;
    public Transform Player;
    public Quaternion PlayerDirection;
    public RectTransform ArrowsLayer;
    public string Angle;

    private void OnGUI()
    {
        GUI.Label(new Rect(580, 30, 800, 200), Angle);
    }
    void Update()
    {
        NorthDirection.z = Player.eulerAngles.y;
        //Debug.Log("current angle "+Player.eulerAngles);
        int tmp =360-(int)Player.eulerAngles.y;
        if (tmp == 360)
        {
            tmp = 0;
        }
        Angle = "Текущий азимут " +tmp;
        ChangeArrowNorthDirection();
    }

    public void ChangeArrowNorthDirection()
    {
        Vector3 dir = transform.position - Player.position;
        PlayerDirection = Quaternion.LookRotation(dir);
        PlayerDirection.z = -PlayerDirection.y;
        PlayerDirection.x = 0;
        PlayerDirection.y = 0;
        ArrowsLayer.localRotation = PlayerDirection * Quaternion.Euler(NorthDirection);
    }
}
