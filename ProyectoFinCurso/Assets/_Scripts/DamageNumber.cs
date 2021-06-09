using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{

    public float damageSpeed;
    public float damagePoint;
    public TMP_Text damageText;
    public Vector2 direction = new Vector2(1, 0);
    public float timeToChangeDirection = 1.0f;
    public float timeToChangeDirectionCounter = 1.0f;

    void Update()
    {
        timeToChangeDirectionCounter -= Time.deltaTime;
        if(timeToChangeDirectionCounter < timeToChangeDirection/2) 
        {
            direction *= -1;
            timeToChangeDirectionCounter += timeToChangeDirection;
        }
        if (damageText != null) 
        {
            damageText.text = "" + damagePoint;
        }
        this.transform.position = new Vector3(this.transform.position.x + direction.x * damageSpeed * Time.deltaTime, this.transform.position.y + damageSpeed * Time.deltaTime, 0);
        this.transform.localScale = this.transform.localScale * (1 - Time.deltaTime / 3);
    }
}
