using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target; 

    private void FixedUpdate()
    {
        transform.LookAt(target);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Translate(-mouseX * Time.deltaTime * 50, -mouseY * Time.deltaTime * 50, 0);
        transform.LookAt(target);

        
        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            transform.position += 2 * transform.forward * ((Vector3.Distance(transform.position, target.position) - 2));
        }
        else if (Vector3.Distance(transform.position, target.position) < 2.5)
        {
            transform.position += 2 * transform.forward * Time.deltaTime * ((Vector3.Distance(transform.position, target.position) - 3));

        }
        else if (Vector3.Distance(transform.position, target.position) > 3.5)
        {
            transform.position += 2 * transform.forward * Time.deltaTime * ((Vector3.Distance(transform.position, target.position) - 3));
        }
    }
 


}
