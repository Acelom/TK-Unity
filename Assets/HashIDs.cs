using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashIDs : MonoBehaviour
{
    public int walkState;
    public int sprintState; 

    public int walkingBool;
    public int sprintingBool; 



    private void Awake()
    {
        walkState = Animator.StringToHash("Walk");
        sprintState = Animator.StringToHash("Sprint");

        walkingBool = Animator.StringToHash("Walking");
        sprintingBool = Animator.StringToHash("Sprinting"); 
    }
}

