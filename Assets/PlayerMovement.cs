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

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        bool sprint = Input.GetButton("Sprint");
        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, - Input.GetAxis("Vertical")); 
        MovementManager(x, y);

        if (anim.GetBool(hash.walkingBool))
        {
            if (anim.GetBool(hash.sprintingBool))
            {
                anim.SetBool(hash.sprintingBool, true);
                player.transform.position += movementDirection * Time.deltaTime * movementSpeed * 2;
            }
            else
            {
                player.transform.position += movementDirection * Time.deltaTime * movementSpeed;
                anim.SetBool(hash.sprintingBool, false); 
            }      
        }
    }

    private void Update()
    {
 
    }

    private void MovementManager(float horizontal, float vertical)
    {

        if ((-0.1f > horizontal || horizontal > 0.1f) || (-0.1f > vertical || vertical > 0.1f))
        {
            anim.SetBool(hash.walkingBool, true);
        }
        else
        {
            anim.SetBool(hash.walkingBool, false);
        }

        Debug.Log(anim.GetBool(hash.walkingBool));
    }
}
