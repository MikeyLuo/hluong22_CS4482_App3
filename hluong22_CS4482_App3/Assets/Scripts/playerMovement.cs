using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 tempPos = transform.position;
            tempPos.y += speed * Time.deltaTime;

            if (tempPos.y > maxY)
                tempPos.y = maxY;
            transform.position = tempPos;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 tempPos = transform.position;
            tempPos.y -= speed * Time.deltaTime;

            if (tempPos.y < minY)
                tempPos.y = minY;
            transform.position = tempPos;
        }
    }
}
