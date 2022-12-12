using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tier3Movement : MonoBehaviour
{

    public GameObject[] tier3;
    public float notActiveTimer = 1f;

    [SerializeField]
    private GameObject tier3bullet;

    [SerializeField]
    private Transform bulletSpawn;

    public float shootingCounter = 0.4f;
    private float currentShootingCounter;
    private bool shootingStatus;
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        currentShootingCounter = shootingCounter;
        initPos = transform.position;
        StartCoroutine(t3enemySpawn(8));
        StartCoroutine(shootingLimit(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        { //tier 3
            if (collision2D.gameObject.CompareTag("playerBullet"))
            {
                Destroy(this.gameObject);
                Destroy(collision2D.gameObject);
            }
        }
    }

    IEnumerator shootingLimit(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(tier3bullet, bulletSpawn.position, Quaternion.identity);
    }


    IEnumerator t3enemySpawn(float seconds)
    {
        int randomIndex = Random.Range(0, tier3.Length);
        
        Vector3 randomPosition = new Vector3(initPos.x, Random.Range(-4, 6), 0);
        transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);

        yield return new WaitForSeconds(seconds);
        Instantiate(tier3[randomIndex], randomPosition, Quaternion.identity);

    }
}
