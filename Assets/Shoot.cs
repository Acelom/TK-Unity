using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform target;
    public GameObject bullet;

    private GameObject shot; 


    private void Update()
    {
        transform.LookAt(target);
        if (Input.GetButtonDown("Shoot"))
        {
            shot = Instantiate(bullet, parent: gameObject.transform, true);
            shot.transform.parent = null; 
            shot.transform.position = gameObject.transform.position; 
            shot.transform.LookAt(target); 
            shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * 5, ForceMode.Impulse);
        }
    }


}
