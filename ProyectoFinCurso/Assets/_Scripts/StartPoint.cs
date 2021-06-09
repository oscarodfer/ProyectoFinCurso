using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private PlayerController player;
    private CameraController theCamera;

    public Vector2 facingDirection = Vector2.zero;
    public string startUuid;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();

        if (!player.nextUuid.Equals(startUuid))
        {
            return;
        }

        player.transform.position = this.transform.position;
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);

        player.lastMovement = facingDirection;
    } 
}
