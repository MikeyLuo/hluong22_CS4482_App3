using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float maxY;

    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private Transform bulletSpawn;

    public float shootingCounter = 0.4f;
    private float currentShootingCounter;
    private bool shootingStatus;
    public float amp;
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        currentShootingCounter = shootingCounter;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        //shootingLimit();
    }

    void movement()
    {
        {
            Vector3 tempPos = transform.position;
            tempPos = new Vector3(initPos.x, Mathf.Sin(Time.time)*amp+initPos.y, 0);

            if ((Mathf.Sin(Time.time) * amp + initPos.y) > maxY)
                tempPos.y = maxY;
                transform.position = tempPos;
        }
  
        //{
            //Vector3 tempPos = transform.position;
            //tempPos.y -= speed * Time.deltaTime;

            //if (tempPos.y < minY)
                //tempPos.y = minY;
            //transform.position = tempPos;
        //}
    }

    //void shootingLimit()
    //{
        //shootingCounter += Time.deltaTime;
        //if (shootingCounter > currentShootingCounter)
        //{
            //shootingStatus = true;
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //if (shootingStatus)
            //{
                //shootingStatus = false;
                //shootingCounter = 0f;
                //Instantiate(enemyBullet, bulletSpawn.position, Quaternion.identity);
            //}
        //}
    //}

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("playerBullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision2D.gameObject);
        }
    }
}
