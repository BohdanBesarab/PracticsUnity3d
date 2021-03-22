using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeDuraction = 2f;
    public float damage = 5f;
    
    private float lifeTimer;

    private bool shotByPlayer;
    public bool ShootByPlayer
    {
        get
        {
            return shotByPlayer;
        }
        set { shotByPlayer = value; }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        lifeTimer = lifeDuraction;

    }

    // Update is called once per frame
    void Update()
    {
        //make the bullet move
        transform.position += transform.forward * speed * Time.deltaTime;
        lifeTimer -= Time.deltaTime;
        if (lifeTimer<=0f)
        {
            gameObject.SetActive(false);
        }
    }
}
