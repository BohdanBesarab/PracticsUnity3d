using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 5;
    public float damage = 5;
    
    private bool killed = false;
    public  bool Killed
    {
        get { return killed; }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Bullet>() != null)
        {
            Bullet bullet = otherCollider.GetComponent<Bullet>();
            if (bullet.ShootByPlayer)
            {

                health -= bullet.damage;
                bullet.gameObject.SetActive(false);
                if (health <= 0)
                {
                    if (killed ==false)
                    {
                        killed = true;
                        OnKill();
                    }
                }
            }
        }
    }

    protected virtual void  OnKill()
    {
        
    }
}

