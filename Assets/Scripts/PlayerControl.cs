using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float movementSpeed;

  

    private Vector3 input;
 
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);

    }
}
