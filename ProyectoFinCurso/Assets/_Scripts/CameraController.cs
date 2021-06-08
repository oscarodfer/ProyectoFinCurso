using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPosition;
    private float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        cameraSpeed = target.GetComponent<PlayerController>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}
