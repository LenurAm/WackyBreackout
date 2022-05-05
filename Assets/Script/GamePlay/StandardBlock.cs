using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : MonoBehaviour
{   //Block script. 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Destroy the block
    private void OnCollisionEnter2D(Collision2D col)
    { if (col.gameObject.CompareTag("Ball"))
        { 
            Destroy(gameObject);
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        }

    }
}
