using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    public Vector3 NorthDirection;
    public Transform Player;
    public Quaternion PlayerDirection;
    public RectTransform ArrowsLayer;

    void Update()
    {
        NorthDirection.z = Player.eulerAngles.y;
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
