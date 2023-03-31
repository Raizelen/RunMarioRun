using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CameraScript : MonoBehaviour
{

    [SerializeField] public float horizontalSpeed = 2.0F;
    [SerializeField] public float verticalSpeed = 2.0F;
    private float h;
    private float v;

  

    private Vector3 input;
 
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
         h += horizontalSpeed * Input.GetAxis("Mouse X");
         v += verticalSpeed * Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-v, h, 0);
    }
}
