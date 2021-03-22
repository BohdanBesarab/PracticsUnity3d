using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public int bulletAmount = 20;

    private List<GameObject> bullets;
    
    public static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance
    {
        get { return instance; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //preload bullets.
        bullets = new List<GameObject>(bulletAmount);
        
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            
            bullets.Add(prefabInstance);
        }
    }

    public GameObject GetBullet(bool shotByPlayer)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().ShootByPlayer = shotByPlayer;
                return bullet;
            }
        }
        
        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullet>().ShootByPlayer = shotByPlayer;
        bullets.Add(prefabInstance);
        
        return prefabInstance;
    }
}
