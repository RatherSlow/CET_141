using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    float hitPoints;

    public Slider healthSlider;

    void Start()
    {
        hitPoints = maxHitPoints;
    }

    void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        SetHealthSlider();

        Debug.Log("OUCH: " + hitPoints.ToString());

        if (hitPoints <= 0)
        {
            OnDeath();
        }        
    }   
    
    void SetHealthSlider()
    {
        if (healthSlider !=null)
        {
            healthSlider.value = NormalisedHitPoints();
        }
    }

    float NormalisedHitPoints()
    {
        return hitPoints / maxHitPoints;
    }

    void OnDeath()
    {
        Debug.Log("TODO: GAME OVER - YOU DIED");
        GameManager.instance.GameOver();
    }
}
