using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public float isGroundedRayLength = 0.3f;
    public LayerMask layerMaskForGrounded;

    private Rigidbody rb;
    private BoxCollider bc;
    private Renderer rend;
    private Transform trans;

    private bool fastFall;
    public PhysicMaterial dodge;
    public PhysicMaterial neutral;

    private float jumpSquatLag = 5;
    private float dodgeCount;


    //STATES
    private bool waveDash = false;
    private bool isDodging;
    private bool isGrounded;
    private bool specialFall;
    private bool jumpSquat = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        rend = GetComponent<Renderer>();
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();

        #region basic movement
        if (!isDodging)
        {
            if (jumpSquat)
            {
                jumpSquatLag -= 60 * Time.deltaTime;
            }
            if (jumpSquatLag <= 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 10, 0);
                jumpSquatLag = 5;
                jumpSquat = false;
            }
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                { 
                jumpSquat = true;
                }
                    //rb.AddForce(transform.up * 2000);
            }
            if (Input.GetKeyDown(KeyCode.S) && !isGrounded && rb.velocity.y <= 0 && !fastFall)
            {
                rb.velocity = new Vector3(rb.velocity.x, -10, 0);
                //rb.AddForce(transform.up * -2000);
                fastFall = true;
            }
            if (Input.GetKey(KeyCode.D) && !waveDash)
            {
                rb.velocity = new Vector3(10, rb.velocity.y, 0);
                //rb.AddForce(transform.right * 40);
            }
            if (Input.GetKey(KeyCode.A) && !waveDash)
            {
                rb.velocity = new Vector3(-10, rb.velocity.y, 0);
                //    rb.AddForce(transform.right * -40);
            }
            #endregion
            #region DodegMovement
            if (Input.GetKeyDown(KeyCode.Space) && !specialFall)
            {
                if (isGrounded)
                {
                    //Shield logic
                }
                else
                {
                    dodgeCount = 20;
                    isDodging = true;
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.useGravity = false;
                    
                    if (Input.GetKey(KeyCode.A))
                    {
                        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                        {
                            rb.AddForce(transform.right * -3000);
                            rb.AddForce(transform.up * 3000);
                        }
                        else
                        {
                            rb.AddForce(transform.right * -3000);
                        }
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
                        {
                            rb.AddForce(transform.right * -3000);
                            rb.AddForce(transform.up * -3000);
                        }
                        else
                        {
                            rb.AddForce(transform.up * -3000);
                        }
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                        {
                            rb.AddForce(transform.right * 3000);
                            rb.AddForce(transform.up * 3000);
                        }
                        else
                        {
                            rb.AddForce(transform.up * 3000);
                        }
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                        {
                            rb.AddForce(transform.right * 3000);
                            rb.AddForce(transform.up * -3000);
                        }
                        else
                        {
                            rb.AddForce(transform.right * 3000);
                        }
                    }

                    specialFall = true;
                }
                #endregion
            }
        }
        if(isDodging || waveDash)
        {
            dodgeCount -= 60 * Time.deltaTime;
            bc.material = dodge;
            rend.material.SetColor("_Color", Color.white);
        }
        if (dodgeCount <= 0)
        {
            if (!waveDash && isDodging)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
            isDodging = false;
            waveDash = false;
            bc.material = neutral;
            rend.material.SetColor("_Color", Color.red);
            rb.useGravity = true;

        }
        //wd state
        if (isDodging && isGrounded)
        {
            waveDash = true;
            isDodging = false;
                //jab, hopp, turnaround
        }
    }
    void IsGrounded()
    {
        Vector3 position = transform.position;
        position.y = bc.bounds.min.y + 0.1f;
        float length = isGroundedRayLength + 0.1f;
        Debug.DrawRay(position, Vector3.down * length);
        bool grounded = Physics.Raycast(position, Vector3.down, length, layerMaskForGrounded.value);
        isGrounded = grounded;
        if (isGrounded)
        {
            fastFall = false;
            specialFall = false;
        }
    }
}
