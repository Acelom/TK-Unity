using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform shieldCenter;
    public float shieldRadius;
    public int numOfObjects;
    public float ShieldLaunchForce; 
    public GameObject hemisphere;
    public Camera camera;
   

    private List<GameObject> shieldObjects = new List<GameObject>();
    private GameObject[] launchList;
    private List<Vector3> newRock= new List<Vector3>();
    private int i = 0;
    private float t = 0; 

    private void Awake()
    {
        launchList = GameObject.FindGameObjectsWithTag("LaunchObject");
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Floor").GetComponent<MeshCollider>(), hemisphere.GetComponent<MeshCollider>());

    }

    // Update is called once per frame
    void Update()
    { 
        hemisphere.transform.localScale = new Vector3(shieldRadius,shieldRadius , shieldRadius ); 

        if (Input.GetButtonDown("Shield"))
        {
            hemisphere.GetComponent<MeshCollider>().enabled = true;
            Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Floor").GetComponent<MeshCollider>(), hemisphere.GetComponent<MeshCollider>());
            i = 0;
            if (i < numOfObjects)
            {
                do
                {
                    if (newRock.Count < i + 1)
                    {
                        newRock.Add( Random.onUnitSphere * shieldRadius);
                    }
                    else
                    {
                        newRock[i] = (Random.onUnitSphere * shieldRadius);
                    }

                    if (newRock[i].z > 0 && newRock[i].y > - 0.33f * shieldRadius )
                    {
                        shieldObjects.Add((GameObject)Instantiate(launchList[Random.Range(0, launchList.Length)]));
                        shieldObjects[i].transform.parent = hemisphere.transform;
                        shieldObjects[i].transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
                        shieldObjects[i].transform.localPosition = newRock[i]  + Vector3.down * 4;
                        shieldObjects[i].GetComponent<Rigidbody>().useGravity = false;
                        shieldObjects[i].GetComponent<BoxCollider>().enabled = false;
                        shieldObjects[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        i += 1;
                    }
                }
                while (i < numOfObjects);
            }
        }

        if (Input.GetButton("Shield"))
        {
            i = 0;
            do
            {
                shieldObjects[i].transform.localPosition = Vector3.Lerp(newRock[i] + Vector3.down * 4, newRock[i], t);
                i += 1;
            }
            while (i < numOfObjects); 
            t += Time.deltaTime;
        }

        if (Input.GetButtonUp("Shield"))
        {
            hemisphere.GetComponent<MeshCollider>().enabled = false; 
            foreach (GameObject go in shieldObjects)
            {
                go.transform.parent = null;
                go.GetComponent<Rigidbody>().useGravity = true;
                go.GetComponent<BoxCollider>().enabled = true;
                go.GetComponent<Rigidbody>().AddForce(camera.transform.forward * Random.Range(ShieldLaunchForce - ShieldLaunchForce/5, ShieldLaunchForce + ShieldLaunchForce / 5));
            }
            newRock.Clear(); 
            shieldObjects.Clear();
            t = 0; 
        }
    }
}
