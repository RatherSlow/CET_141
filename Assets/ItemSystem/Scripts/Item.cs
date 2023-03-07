using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
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

    float rotateY = 0f;
    [SerializeField]
    float spinspeed = 0.3f;
    [SerializeField]
    bool isSpinable = true;

    [SerializeField]
    float weight = 0;
    [SerializeField]
    int quantity = 1;
    [SerializeField]
    int maxStackableQuantity = 1; // for bundles of items, such as arrows or coins.

    [SerializeField]
    bool isStorable = false; // if false, item will be used when picked up.
    [SerializeField]
    bool isConsumable = true; // if true, item will be destroyed (or quantity reduced) when used

    [SerializeField]
    bool isPickupOnCollision = false;

    [SerializeField]
    int pointValue = 1;

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
                Interact();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpinable == true)
        {
            if (rotateY < 360f)
            {
                Spin();
            }
            else
            {
                rotateY = 0f;
                Spin();
            }
        }
    }

    public void Spin()
    {
        transform.localRotation = Quaternion.Euler(0f, rotateY, 0f);
        rotateY += spinspeed;
    }
    public void Interact()
    {
        Debug.Log("Picked up " + transform.name);

        if (isStorable)
        {
            Store();
        }
        else
        {
            Use();
        }
    }

    void Store()
    {
        Debug.Log("Storing " + transform.name);

        //ToDo Inventory system

        Destroy(gameObject);
    }

    void Use()
    {
        Debug.Log("Using " + transform.name);
        if (isConsumable)
        {
            quantity--;
            if (quantity <= 0)
            {
                Destroy(gameObject);
            }
            GameManager.IncrementScore(pointValue);
        }
    }
}
