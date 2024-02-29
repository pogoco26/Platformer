using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class CharacterControllerLive : MonoBehaviour
{

    public float accel = 10f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag != "Dead"){
            float horizontalMovement = Input.GetAxis("Horizontal");
            Rigidbody rbody = GetComponent<Rigidbody>();
            rbody.velocity += Vector3.right * -1 * horizontalMovement * Time.deltaTime * accel;

            Collider col = GetComponent<Collider>();
            float halfHeight = col.bounds.extents.y + 0.03f;

            Vector3 startPoint = transform.position;
            Vector3 endPoint = startPoint + Vector3.down * halfHeight;

            isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);
            Color lineColor = (isGrounded) ? Color.red:Color.blue;
            Debug.DrawLine(startPoint, endPoint, lineColor, 0f, true);

            if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
                rbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            }
            else if(!isGrounded && Input.GetKey(KeyCode.Space))
            {
                if(rbody.velocity.y > 0){
                    rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
                }
                
            }

            if(Math.Abs(rbody.velocity.x) > maxSpeed){
                //rbody.velocity = rbody.velocity.normalized * maxSpeed;
                Vector3 newVel = rbody.velocity;
                newVel.x = Math.Clamp(newVel.x, -maxSpeed, maxSpeed);
                rbody.velocity = newVel;
            }
            float yaw = (rbody.velocity.x > 0)?90:-90;
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);

            float speed = Math.Abs(rbody.velocity.x);
            GetComponent<Animator>().SetFloat("Speed", speed);
            GetComponent<Animator>().SetBool("IsGrounded", isGrounded);
        }
    }
}
