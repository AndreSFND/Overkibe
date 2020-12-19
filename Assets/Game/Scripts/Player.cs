using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb = null;
    private bool onTable = false;
    private GameObject table = null;
    public float speed;
    private Animator animator;

    private int prato;
    private Queue<int> ingredientes = new Queue<int>();

    private bool realizandoTarefa = false;
    [SerializeField] Text labelPrato = null;
    [SerializeField] Text labelIngredientes = null;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontalInput   = Input.GetAxis("Horizontal");
        float verticalInput     = Input.GetAxis("Vertical");

        if( realizandoTarefa ) {

            horizontalInput = 0f;
            verticalInput = 0f;

        }

        if( horizontalInput == 0 && verticalInput == 0 ) {
            
            animator.SetInteger("state",  0);

        } else {

            if( verticalInput > 0 ) animator.SetInteger("state",  2);
            if( verticalInput < 0 ) animator.SetInteger("state",  1);
            if( horizontalInput > 0 ) animator.SetInteger("state",  3);
            if( horizontalInput < 0 ) animator.SetInteger("state",  4);

        }

        rb.velocity = new Vector2(horizontalInput*speed, verticalInput*speed);

        if( Input.GetKeyDown("space") && onTable && !realizandoTarefa ) {

            table.transform.GetComponent<mesaSemaforo>().EntrarNaFila(gameObject);

        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if( col.name == "MesaRange" ) {

            Debug.Log("Aperte X para interagir");
            onTable = true;

            table = col.transform.parent.gameObject;

        }

    }

    void OnTriggerExit2D(Collider2D col)
    {

        if( col.name == "MesaRange" ) {

            Debug.Log("Não está mais interagindo");
            onTable = false;

            table = null;

        }

    }

    public void Travar() {

        this.realizandoTarefa = true;

    }

    public void Destravar() {

        this.realizandoTarefa = false;

    }

    public void DefinirPrato( int prato ) {

        this.prato = prato;

        labelPrato.text = prato + "";

    }

    public void SegurarIngrediente( int ingrediente ) {

        this.ingredientes.Enqueue( ingrediente );

        if( this.ingredientes.Count > 4 ) this.ingredientes.Dequeue();

        labelIngredientes.text = "";

        foreach( int i in this.ingredientes ) {

            labelIngredientes.text += i + " ";

        }

    }
    
}
