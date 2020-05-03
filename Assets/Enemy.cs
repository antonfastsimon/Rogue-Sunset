using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    private Renderer rend;
    private Transform trans;

    private bool isGrounded;
    public float isGroundedRayLength = 0.3f;
    public LayerMask layerMaskForGrounded;

    public static Enemy instance;

    private void Awake()
    {
        instance = this;
    }
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
    }
    void IsGrounded()
    {
        Vector3 position = transform.position;
    position.y = bc.bounds.min.y + 0.1f;
        float length = isGroundedRayLength + 0.1f;
    Debug.DrawRay(position, Vector3.down* length);
        bool grounded = Physics.Raycast(position, Vector3.down, length, layerMaskForGrounded.value);
    isGrounded = grounded;
    }
}
