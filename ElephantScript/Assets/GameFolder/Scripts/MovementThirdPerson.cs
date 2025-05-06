using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementThirdPerson : MonoBehaviour
{
    
    public float speed = 4f;
    [SerializeField] private float rotateSpeed = 4f;
    Rigidbody rb;
    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

        if (moveInput.magnitude >= 0.1f) {
            Quaternion _rotation = Quaternion.LookRotation(moveInput);
            _rotation.x = 0;
            _rotation.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, rotateSpeed * Time.deltaTime);
            
        }
        anim.SetBool("isWalk", moveInput.magnitude >= 0.1f);
        rb.velocity = moveInput * speed;
    }
}
