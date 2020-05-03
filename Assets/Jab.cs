using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jab : MonoBehaviour
{
    float jabFrames = 15;
    private bool jabbing = false;
    private MeshRenderer rend;
    private BoxCollider bc;

    private float angle = Mathf.PI/4;
    private float kb = 10;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        rend.enabled = false;
        jabbing = false;
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jabbing)
        {
            jabFrames -= 60 * Time.deltaTime;
        }
        if (jabFrames <= 10)
        {
            rend.enabled = true;
            bc.enabled = true;
        }
        if (jabFrames <= 5)
        {
            rend.enabled = false;
            bc.enabled = false;
        }
        if (jabFrames <= 0)
        {
            jabbing = false;
            jabFrames = 15;

        }

        if (Input.GetKey(KeyCode.Z))
        {
            jabbing = true;
        }
    }
        private void OnTriggerEnter(Collider other)
        {
        Debug.Log("asd");
            if (other.gameObject.tag == "Player")
            {
            Enemy.instance.transform.position += new Vector3(Mathf.Cos(angle) * kb, Mathf.Sin(angle) * kb, 0);
            }
        }
    
}
