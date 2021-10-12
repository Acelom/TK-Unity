using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public float liftForce;
    public float liftTime;
    public float maxSpeed;
    public float acceleration; 
    public Transform holdPosition;
    public float launchSpeed; 



    private GameObject launchItem;
    private Camera cam;
    private bool timerOn;
    private float timer;
    private float speed;
    private float distance; 

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 
    }


    private void FixedUpdate()
    {
        if (Input.GetButton("Launch"))
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && launchItem is null && hit.transform.gameObject.GetComponent<Rigidbody>() != null)
            {
                launchItem = hit.transform.gameObject;
                launchItem.GetComponent<Rigidbody>().AddForce(0, liftForce * (launchItem.GetComponent<Rigidbody>().mass), 0);
                timerOn = true;
                distance = Vector3.Distance(launchItem.transform.position, holdPosition.position);
            }

            if (timerOn)
            {
                timer += Time.deltaTime;
            }

            if (!(launchItem is null) && timer > liftTime)
            {
                launchItem.GetComponent<Rigidbody>().useGravity = false;

                launchItem.transform.position = Vector3.MoveTowards(launchItem.transform.position, holdPosition.position, (Mathf.Sqrt(distance) * speed));
                if (speed <= maxSpeed)
                {
                    speed += acceleration;
                }
            }

        }

        if (!Input.GetButton("Launch") && !(launchItem is null))
        {
            launchItem.transform.rotation = cam.transform.rotation;
            Debug.Log("Miss");
            launchItem.GetComponent<Rigidbody>().AddForce((launchItem.transform.forward * launchSpeed * (launchItem.GetComponent<Rigidbody>().mass)));
            launchItem.GetComponent<Rigidbody>().useGravity = true;
            launchItem.GetComponent<Rigidbody>().AddTorque(Random.Range(0f, 50f * (launchItem.GetComponent<Rigidbody>().mass)), Random.Range(0f, 50f * (launchItem.GetComponent<Rigidbody>().mass)), Random.Range(0f, 50f * (launchItem.GetComponent<Rigidbody>().mass)));
            launchItem = null;
            timerOn = false;
            timer = 0;
        }
    }      
}
