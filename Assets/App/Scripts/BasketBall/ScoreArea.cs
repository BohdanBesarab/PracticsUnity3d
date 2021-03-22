using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    private void Start()
    {
        effectObject.SetActive(false);
    }

    public GameObject effectObject;
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Ball>()!=null){
                  effectObject.SetActive(true);
        }
    }
}
