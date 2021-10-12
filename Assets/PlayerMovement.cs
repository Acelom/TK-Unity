using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed;

    private Animator anim;
    private HashIDs hash;
    private GameObject player;
    private Transform characterTransform;
    private Camera cam;
    private GameObject launchItem; 

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
        cam = GameObject.FindGameObjectWithTag ( "MainCamera" ).GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        bool sprint = Input.GetButton("Sprint");
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, - Input.GetAxis("Vertical"));
        Vector3 realDirection = new Vector3 (Camera.main.transform.TransformDirection(movementDirection).x, 0, Camera.main.transform.TransformDirection(movementDirection).z);
        MovementManager(x, y, sprint);


        if (anim.GetBool(hash.walkingBool))
        {
            Quaternion newRotation = Quaternion.LookRotation(realDirection);
            characterTransform.rotation = Quaternion.Slerp(characterTransform.rotation, newRotation, Time.deltaTime * 10);

            if (anim.GetBool(hash.sprintingBool))
            {
                anim.SetBool(hash.sprintingBool, true);
                player.transform.position += characterTransform.forward * Time.deltaTime * movementSpeed * 2;
                if (cam.fieldOfView <= 60)
                {
                    cam.fieldOfView += cam.fieldOfView / 20;
                }
            }
            else
            {
                player.transform.position += characterTransform.forward * Time.deltaTime * movementSpeed;
                anim.SetBool(hash.sprintingBool, false);

                if (cam.fieldOfView > 40)
                {
                    cam.fieldOfView -= cam.fieldOfView / 20;
                }
                else if (cam.fieldOfView < 40)
                {
                    cam.fieldOfView = 40; 
                }
            }      
        }

    }


    private void MovementManager(float horizontal, float vertical, bool sprint)
    {

        if ((-0.1f > horizontal || horizontal > 0.1f) || (-0.1f > vertical || vertical > 0.1f))
        {
            anim.SetBool(hash.walkingBool, true);
        }
        else
        {
            anim.SetBool(hash.walkingBool, false);
        }

        if (anim.GetBool(hash.walkingBool) && Input.GetButton("Sprint"))
        {
            anim.SetBool(hash.sprintingBool, true);
        }
        else
        {
            anim.SetBool(hash.sprintingBool, false);
        }
    }
}
