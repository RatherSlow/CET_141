using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 25;
    float rotateY = 90f;

    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        if(hitPoints <= 0)
        {
            Invoke("SelfTerminate", 0f);
        }        
    }

    void SelfTerminate()
    {
        transform.Rotate(rotateY, 0f, 0f);
        Destroy(gameObject);
    }
}
