using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform cameraTransform;
    [SerializeField]
    Transform goodgunTransform;
    float range = 100f;

    [SerializeField]
    float rawDamage = 10f;

    void Update()
    {
        if (!MenuController.IsGamePaused)
        {
            FireWeapon();
        }
    }
    void FireWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SwitchPerspective switchPerspective = GetComponent<SwitchPerspective>();
            Ray ray = new Ray();
            if (switchPerspective.GetPerspective() == SwitchPerspective.Perspective.First)
            {
                cameraTransform = Camera.main.transform;
                ray = new Ray(cameraTransform.position, cameraTransform.forward);
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 5, Color.magenta, 1f);
            }
            else
            {
                //goodgunTransform = gameObject.transform.Find("GoodGun3");
                ray = new Ray(goodgunTransform.position, goodgunTransform.forward);
                Debug.DrawRay(goodgunTransform.position, goodgunTransform.forward * 5, Color.green, 1f);
            }         
            Debug.Log("FIRE 1");
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, range))
            {
                if(raycastHit.transform != null)
                {
                    raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                Debug.Log("NO RAYCAST");
            }
        }
    }
}
