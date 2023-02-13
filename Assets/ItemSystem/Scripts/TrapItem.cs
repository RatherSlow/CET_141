using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapItem : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Sprite icon;

    [SerializeField]
    string itemName;
    [SerializeField]
    [TextArea(4, 16)]
    string description;

    [SerializeField]
    float rawDamage = 10;
    
    [SerializeField]
    bool isPickupOnCollision = true;

    // Start is called before the first frame update
    private void Start()
    {
        if (isPickupOnCollision)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isPickupOnCollision)
        {
            if (collider.tag == "Player")
            {
                Interact(collider);
            }
        }
    }

    void Update()
    {

    }
    

    public void Interact(Collider collider)
    {
        Debug.Log("Stepped on " + transform.name);
        collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);        
    }
}
