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
    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private bool interacting = false;
    private Animator animator;
    private List<int> receita;
    private Queue<int> ingredientes = new Queue<int>();
    private int prato = -1;
    private int pontos = 0;

    [SerializeField] GameObject UIPlayer = null;
    [SerializeField] private GameController gc = null;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    void FixedUpdate() {

        rb.velocity = new Vector2(horizontalInput*speed, verticalInput*speed);

        if( horizontalInput == 0 && verticalInput == 0 ) {
                
            animator.SetInteger("state",  0);

        } else {

            if( verticalInput > 0 ) animator.SetInteger("state",  2);
            if( verticalInput < 0 ) animator.SetInteger("state",  1);
            if( horizontalInput > 0 ) animator.SetInteger("state",  3);
            if( horizontalInput < 0 ) animator.SetInteger("state",  4);

        }

        if( interacting && onTable ) {

            table.transform.GetComponent<mesaSemaforo>().EntrarNaFila(gameObject);

            interacting = false;

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
    public void DefinirReceita( List<int> receita ) {

        List<Sprite> ingredientes = gc.getIngredientes();

        this.receita = receita;

        Transform pedido = UIPlayer.transform.Find("Pedido").transform;

        for( int i=0; i<receita.Count; i++ ) {

            pedido.Find( (i+1) + "" ).transform.GetComponent<Image>().sprite = ingredientes[ receita[i] ];
            pedido.Find( (i+1) + "" ).gameObject.SetActive(true);

        }

    }

    public void SegurarIngrediente( int ingrediente ) {

        List<Sprite> listaIngredientes = gc.getIngredientes();
        Transform imgIngredientes = UIPlayer.transform.Find("Segurando").transform.Find("Ingredientes").transform;

        this.ingredientes.Enqueue( ingrediente );
        if( this.ingredientes.Count > 3 ) this.ingredientes.Dequeue();


        int counter = 0;
        foreach( int i in this.ingredientes ) {

            imgIngredientes.Find( (counter+1) + "" ).transform.GetComponent<Image>().sprite = listaIngredientes[ i ];
            imgIngredientes.Find( (counter+1) + "" ).gameObject.SetActive(true);

            counter++;

        }

    }

    public void DefinirPrato(int prato) {

        this.prato = prato;

        Transform imgIngredientes = UIPlayer.transform.Find("Segurando").transform.Find("Ingredientes").transform;
        int img = transform.name == "Player1" ? 1 : 3;

        if( prato != -1 ) {
        
            List<Sprite> listaPratos = gc.getPratos();

            imgIngredientes.Find( "" + img ).transform.GetComponent<Image>().sprite = listaPratos[ prato ];
            imgIngredientes.Find( "" + img ).gameObject.SetActive(true);
        
        } else {

            imgIngredientes.Find( "" + img ).gameObject.SetActive(false);

        }

    }

    public int GetPrato() {

        return this.prato;

    }

    public void LimparIngredientes() {

        Transform imgIngredientes = UIPlayer.transform.Find("Segurando").transform.Find("Ingredientes").transform;

        int counter = 0;
        foreach( int i in this.ingredientes ) {

            imgIngredientes.Find( (counter+1) + "" ).gameObject.SetActive(false);
            counter++;

        }

        this.ingredientes.Clear();
            
    }

    public void LimparReceita() {

        Transform pedido = UIPlayer.transform.Find("Pedido").transform;

        for( int i=0; i<receita.Count; i++ ) {

            pedido.Find( (i+1) + "" ).gameObject.SetActive(false);

        }

        this.receita.Clear();

    }

    public Queue<int> GetIngredientes() {

        return this.ingredientes;

    }

    public List<int> GetReceita() {

        return this.receita;

    }

    public void AdicionarPontos(int pontos) {

        this.pontos += pontos;

        Transform pontuacao = UIPlayer.transform.Find("Pedido").transform.Find("Pontuacao");

        pontuacao.GetComponent<Text>().text = "" + this.pontos;

    }

    public int GetPontuacao() {

        return this.pontos;

    }

    public Animator GetPlayerAnimator() {

        return this.animator;

    }

    public void Move(float horizontal, float vertical) {

        this.horizontalInput = horizontal;
        this.verticalInput = vertical;

    }
    public void Interact(bool interacting) {

        this.interacting = interacting;

    }
    
}
