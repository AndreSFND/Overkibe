using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb = null;
    private bool onTable = false;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontalInput   = Input.GetAxis("Horizontal");
        float verticalInput     = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalInput*speed, verticalInput*speed);

        if( Input.GetKeyDown("space") && onTable ) {

            Debug.Log("Interagindo com balcao");

        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if( col.name == "MesaRange" ) {

            Debug.Log("Aperte X para interagir");
            onTable = true;

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {

        if( col.name == "MesaRange" ) {

            Debug.Log("Não está mais interagindo");
            onTable = false;

        }

    }
    
}
