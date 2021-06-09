using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private PlayerController player;
    private CameraController camera;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        camera = FindObjectOfType<CameraController>();

        player.transform.position = this.transform.position;
        camera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, camera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
