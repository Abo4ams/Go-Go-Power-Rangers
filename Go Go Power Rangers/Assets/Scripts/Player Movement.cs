using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("[1, Infinity]")]
    [SerializeField]
    float speed = 1f;

    Animator animator;

    Transform t;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Console.Write(animator.parameters);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Console.Write("left");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Console.Write("right");
        }
    }

    private void FixedUpdate()
    {
        
    }
}
