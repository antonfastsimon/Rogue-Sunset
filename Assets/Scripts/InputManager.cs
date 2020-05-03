using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SJ
{
    public class InputManager : MonoBehaviour
    {
        float playerMovement = 5f;
        Rigidbody rigid;
        Transform trans;
        private bool walk;
        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
            Debug.Log(Input.GetKey(KeyCode.Space));
            walk = Input.GetKey(KeyCode.Space);
                if (Input.GetKey(KeyCode.W))
                {
                rigid.velocity += transform.forward * Time.deltaTime * playerMovement;
                }

                //if (Input.GetAxis("Depth"))
                //{
                //    transform.localPosition.z += playerMovement * Input.GetAxis("Horizontal") * Time.deltaTime;
                //}
        }
    }
}