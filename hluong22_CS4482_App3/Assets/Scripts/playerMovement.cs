using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minY, maxY;

    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Transform bulletSpawn;

    public float shootingCounter = 0.4f;
    private float currentShootingCounter;
    private bool shootingStatus;

    // Start is called before the first frame update
    void Start()
    {
        currentShootingCounter = shootingCounter;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        shootingLimit();
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

    void shootingLimit()
    {
        shootingCounter += Time.deltaTime;
        if (shootingCounter > currentShootingCounter)
        {
            shootingStatus = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (shootingStatus)
            {
                shootingStatus = false;
                shootingCounter = 0f;
                Instantiate(playerBullet, bulletSpawn.position, Quaternion.identity);
            }
        }
    }

}
