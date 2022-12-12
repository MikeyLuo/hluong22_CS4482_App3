using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooting : MonoBehaviour
{
    public float speed = 5f;
    public float notActiveTimer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("deactivateBullets", notActiveTimer);
    }

    // Update is called once per frame
    void Update()
    {
        shootMechanics();
    }

    void shootMechanics()
    {
        Vector3 bulletPos = transform.position;
        bulletPos.x += speed * Time.deltaTime;
        transform.position = bulletPos;
    }

    void deactivateBullets()
    {
        gameObject.SetActive(false);
    }
}
