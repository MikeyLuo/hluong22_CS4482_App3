using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float minY, maxY;
    public GameObject waveui;
    public GameObject waveui2;
    public GameObject waveui3;
    public GameObject finishui;
    public static bool finish;
    public InputField inname;
    public Text timertxt;
    public Text scoretxt;
    public static float currenTimer;
    public int culmscore;
    public GameObject heart1, heart2, heart3, shield;
    public static bool dead;
    public float damagePool;
    public static bool isShield;
    public static bool isHeart1;
    public static bool isHeart2;
    public static bool isHeart3;


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
        finish = false;
        currenTimer = 0;
        finishui.SetActive(false);
        currentShootingCounter = shootingCounter;
        StartCoroutine(t1enemySpawn(5));
        StartCoroutine(wave2(60));
        StartCoroutine(wave3(120));
        culmscore = 0;
        dead = false;
        //damagePool = 0;
        isShield = true;
        isHeart1 = true;
        isHeart2 = true;
        isHeart3 = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        shootingLimit();
        currenTimer += Time.deltaTime;
        timertxt.text = "Time: " + currenTimer.ToString("F2");
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

    void OnCollisionEnter2D(Collision2D collision2D)
    {

        //
        {
            if (collision2D.gameObject.CompareTag("tier1") || collision2D.gameObject.CompareTag("tier3Bullet"))
            {
                damagePool = damagePool + 1;
                Destroy(collision2D.gameObject);
                if (damagePool >= 10)
                {
                    shield.SetActive(false);
                    isShield = false;
                }

                if (damagePool >=15 && !isShield)
                {
                    heart3.SetActive(false);
                    Destroy(collision2D.gameObject);
                }

                if (damagePool >= 20 && !isShield)
                {
                    heart2.SetActive(false);
                    Destroy(collision2D.gameObject);
                }

                if (damagePool >= 25 && !isShield)
                {
                    heart1.SetActive(false);
                    isHeart1 = false;
                    Destroy(collision2D.gameObject);
                }

                if (damagePool > 26 && !isShield && !isHeart1)
                {
                    heart3.SetActive(false);
                    heart1.SetActive(false);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    shield.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    finish = true;
                    finishui.SetActive(true);
                    Time.timeScale = 0f;
                }
            }

            if (collision2D.gameObject.CompareTag("tier2Bullet"))
            {
                damagePool = damagePool + 4;
                Destroy(collision2D.gameObject);
                if (damagePool >= 10)
                {
                    shield.SetActive(false);
                    isShield = false;
                }

                if (damagePool >= 15 && !isShield)
                {
                    heart3.SetActive(false);
                    Destroy(collision2D.gameObject);
                }

                if (damagePool >= 20 && !isShield)
                {
                    heart2.SetActive(false);
                    Destroy(collision2D.gameObject);
                }

                if (damagePool >= 25 && !isShield)
                {
                    heart1.SetActive(false);
                    isHeart1 = false;
                    Destroy(collision2D.gameObject);
                }

                if (damagePool > 26 && !isShield && !isHeart1)
                {
                    heart3.SetActive(false);
                    heart1.SetActive(false);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    shield.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    finish = true;
                    finishui.SetActive(true);
                    Time.timeScale = 0f;
                }
            }

            if (collision2D.gameObject.CompareTag("tier4Bullet"))
            {
                damagePool = damagePool + 26;
                //Destroy(collision2D.gameObject);
                if (damagePool >= 25)
                {
                    heart1.SetActive(false);
                    heart2.SetActive(false);
                    heart3.SetActive(false);
                    shield.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    finish = true;
                    finishui.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
    }

    IEnumerator t1enemySpawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        waveui.SetActive(false);

    }
    IEnumerator wave2(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        waveui2.SetActive(true);

    }
    IEnumerator wave3(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        waveui3.SetActive(true);

    }

    public void logData()
    {
        PlayerPrefs.SetInt("finishTime", Convert.ToInt32(finish));
        PlayerPrefs.SetFloat("time", currenTimer);
        PlayerPrefs.SetString("name", inname.text);
        //PlayerPrefs.SetInt("score", score);
        SceneManager.LoadScene("leaderboard");
    }
}
