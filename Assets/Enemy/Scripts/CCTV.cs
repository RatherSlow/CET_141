using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    [SerializeField]
    float CameraAngle = 35f;
    float rotateY = 0f;
    [SerializeField]
    float spinspeed = 0.3f;
    [SerializeField]
    bool isActive = true;
    public Transform CameraCage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            if (rotateY > 60f || rotateY < -60f)
            {
                spinspeed = -1*spinspeed;
                Spin();
            }
            else
            {                
                Spin();
            }
        }
    }
    public void Spin()
    {
        CameraCage.localRotation = Quaternion.Euler(CameraAngle, rotateY, 0f);
        rotateY += spinspeed;
    }
}
