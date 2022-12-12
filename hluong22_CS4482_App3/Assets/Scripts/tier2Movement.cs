using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tier2Movement : MonoBehaviour
{

    //public float maxY,minY;
    public float speed = 2f;
    public GameObject[] tier2;

    [SerializeField]
    private GameObject tier2bullet;

    [SerializeField]
    private Transform bulletSpawn;

    public float shootingCounter = 0.4f;
    private float currentShootingCounter;
    private bool shootingStatus;
    public float amp;
    public float freq;
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        currentShootingCounter = shootingCounter;
        initPos = transform.position;
        StartCoroutine(t2enemySpawn(5));
        StartCoroutine(shootingLimit(1));
    }

    // Update is called once per frame
    void Update()
    {
        t2movement();
    }

    void t2movement()
    {
        {
            Vector3 tempPos = transform.position;
            transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, 0);
        }
    }
    IEnumerator shootingLimit(float seconds)
    {

        Instantiate(tier2bullet, bulletSpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(seconds);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        { //tier 1
            if (collision2D.gameObject.CompareTag("playerBullet"))
            {
                Destroy(this.gameObject);
                Destroy(collision2D.gameObject);
            }
        }
    }
    IEnumerator t2enemySpawn(float seconds)
    {
        int randomIndex = Random.Range(0, tier2.Length);
        Vector3 randomPosition = new Vector3(initPos.x, Random.Range(-4, 6), 0);
        transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);

        yield return new WaitForSeconds(seconds);
        Instantiate(tier2[randomIndex], randomPosition, Quaternion.identity);
    }
}