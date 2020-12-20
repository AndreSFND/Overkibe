using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class GameController : MonoBehaviour
{

    [SerializeField] private int timer = 60;
    [SerializeField] private Text timerText = null;
    [SerializeField] private List<Sprite> ingredientes = null;
    [SerializeField] private List<Sprite> pratos = null;
    [SerializeField] private Player player1 = null;
    [SerializeField] private Player player2 = null;

    public Thread _t1;
    public Thread _t2;

    void Start() {

        this.timerText.text = (this.timer / 60) + ":" + (this.timer % 60).ToString("00");
        InvokeRepeating("setTimer", 1f, 1f);

        _t1 = new Thread(_ControlPlayer1);
        _t1.Start();

        _t2 = new Thread(_ControlPlayer2);
        _t2.Start();

    }

    private bool W = false;
    private bool A = false;
    private bool S = false;
    private bool D = false;
    private bool J = false;
    
    private bool Up = false;
    private bool Left = false;
    private bool Down = false;
    private bool Right = false;
    private bool Space = false;

    void FixedUpdate()
    {

        this.W = Input.GetKey( KeyCode.W );
        this.A = Input.GetKey( KeyCode.A );
        this.S = Input.GetKey( KeyCode.S );
        this.D = Input.GetKey( KeyCode.D );
        this.J = Input.GetKey( KeyCode.J );

        this.Up = Input.GetKey( KeyCode.UpArrow );
        this.Left = Input.GetKey( KeyCode.LeftArrow );
        this.Down = Input.GetKey( KeyCode.DownArrow );
        this.Right = Input.GetKey( KeyCode.RightArrow );
        this.Space = Input.GetKey( KeyCode.Space );
        
    }

    void setTimer() {

         if( this.timer > 0 ) {
                
            this.timer--;
            this.timerText.text = (this.timer / 60) + ":" + (this.timer % 60).ToString("00");

        } else {

            int p1Score = player1.GetPontuacao();
            int p2Score = player2.GetPontuacao();

            if( p1Score > p2Score ) this.timerText.text = "Player 1 venceu!";
            else if( p2Score > p1Score ) this.timerText.text = "Player 2 venceu!";
            else if( p2Score == p1Score ) this.timerText.text = "Empate!";

            Time.timeScale = 0;

        }

    }

    void _ControlPlayer1() {

        while( _t1.IsAlive ) {

            int horizontalInput = 0;
            int verticalInput = 0;
            bool interacting = false;

            if( W ) verticalInput = 1;
            if( A ) horizontalInput = -1;
            if( S ) verticalInput = -1;
            if( D ) horizontalInput = 1;
            if( J ) interacting = true;

            player1.Move(horizontalInput, verticalInput);
            player1.Interact(interacting);

            Thread.Sleep(100);

        }

    }

    void _ControlPlayer2() {

        while( _t2.IsAlive ) {

            int horizontalInput = 0;
            int verticalInput = 0;
            bool interacting = false;

            if( Up ) verticalInput = 1;
            if( Left ) horizontalInput = -1;
            if( Down ) verticalInput = -1;
            if( Right ) horizontalInput = 1;
            if( Space ) interacting = true;

            player2.Move(horizontalInput, verticalInput);
            player2.Interact(interacting);

            Thread.Sleep(100);

        }

    }

    void OnApplicationQuit()
    {

        _t1.Abort();

        Debug.Log("Application ending after " + Time.time + " seconds");

    }

    public List<Sprite> getIngredientes() {

        return this.ingredientes;

    }

    public List<Sprite> getPratos() {

        return this.pratos;

    }

}
