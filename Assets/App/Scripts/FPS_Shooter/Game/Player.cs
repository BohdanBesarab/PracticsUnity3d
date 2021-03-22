using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")] public GameObject playerCamera;

    [Header("Gameplay")] public int initrailAmmo = 12;
    public int intialHealth;
    public float knockbackForce = 10f;
    public float heartDuration = 0.5f;

    public float health;
    public float Health
    {
        get { return health; }
    }

    public int ammo;
    public int Ammo
    {
        get { return ammo; }
    }

    public bool killed;
    public bool Killed
    {
        get { return killed; }
    }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        health = intialHealth;
        ammo = initrailAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0&& Killed==false)
            {
                ammo--;
                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true);
                bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }

    }

    //check for collisions
    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<AmmoCrate>() != null)
        {
            //collect ammo crates
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }else if (otherCollider.GetComponent<HealthCrate>() != null)
        {
            //collect health crates
            HealthCrate healthCrate = otherCollider.GetComponent<HealthCrate>();
            health += healthCrate.health;

            Destroy(healthCrate.gameObject);
        }

        if (isHurt == false)
        {
            GameObject hazard = null;
            if (otherCollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = otherCollider.GetComponent<Enemy>();
                if (enemy.Killed == false)
                {
                    hazard = enemy.gameObject;
                    health -= enemy.damage;
                }
                //Touching enemy
                isHurt = true;
                //Preform the knockback effect
            }
            else if (otherCollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = otherCollider.GetComponent<Bullet>();
                if (bullet.ShootByPlayer == false)
                {
                    hazard = bullet.gameObject;
                    
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }

            if (hazard != null)
            {
                isHurt = true;
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);
                StartCoroutine(HurtRoutine());
            }

            if (health <= 0)
            {
                if (killed == false)
                {
                    killed = true;
                    OnKill();
                }
            }
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(heartDuration);
        isHurt = false;
    }
    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
    }
}

