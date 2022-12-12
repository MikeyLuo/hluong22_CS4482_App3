using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tier1Movement : MonoBehaviour
{

    //public float speed = 3f;
    public GameObject[] tier1;
    public float notActiveTimer = 2f;
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(t1enemySpawn(3));
    }

    // Update is called once per frame
    void Update()
    {
        t1movement();
    }
    void t1movement()
    {
        {
            transform.Translate(Vector3.up * 5f * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        {
            if (collision2D.gameObject.CompareTag("playerBullet"))
            {
                Destroy(this.gameObject);
                Destroy(collision2D.gameObject);
            }
        }
    }
    IEnumerator t1enemySpawn(float seconds)
    {
        int randomIndex = Random.Range(0, tier1.Length);
        Vector3 randomPosition = new Vector3(initPos.x, Random.Range(-4, 6), 0);
        transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);

        yield return new WaitForSeconds(seconds);
        Instantiate(tier1[randomIndex], randomPosition, Quaternion.identity);
        Invoke("deactivatetier1", notActiveTimer);

    }

    void deactivatetier1()
    {
        gameObject.SetActive(false);
    }
}
