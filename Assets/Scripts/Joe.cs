using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Joe : MonoBehaviour
    {
        Animator anim;
    Transform trans;
        void Start()
        {
            anim = GetComponent<Animator>();
            Debug.Log("hej");
            
        trans = GetComponent<Transform>();
        }

        void Update()
        {
        anim.SetBool("isStartOfGame", false);
    }
    }
