using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementThirdPerson : MonoBehaviour
{
    
    [SerializeField] private float speed = 4f;
    [SerializeField] private float rotateSpeed = 4f;
    Rigidbody rb;


    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (moveInput.magnitude >= 0.1f) {
            Quaternion _rotation = Quaternion.LookRotation(moveInput);
            _rotation.x = 0;
            _rotation.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, rotateSpeed * Time.deltaTime);
            
        }

        rb.velocity = moveInput * speed;
    }
}
