using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

public class mesaSemaforo : MonoBehaviour
{

    [SerializeField] float duracao = 3f;
    [SerializeField] private GameObject progressBar = null;
    [SerializeField] private GameController gameController = null;

    private Queue<GameObject> fila = new Queue<GameObject>();
    private bool interagindo = false;
    private float contadorTempo = 0f;
    private GameObject personagemAtual = null;
    public UnityEvent OnStartInteraction = null;
    public UnityEvent OnExitInteraction = null;

    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

        // Se nao tiver ninguem anotando pedido e houver pessoas na fila
        if( fila.Count > 0 && !interagindo ) {

            interagindo = true;
            contadorTempo = 0f;
            personagemAtual = fila.Dequeue();

            progressBar.SetActive(true);

            OnStartInteraction.Invoke();

        } else if ( interagindo ) {

            if( contadorTempo < duracao ) {

                contadorTempo += Time.deltaTime;

            } else if( contadorTempo >= duracao ) {

                interagindo = false;
                contadorTempo = 0f;

                if( personagemAtual.transform.name == "Player1" ) gameController._t1.Resume();
                else gameController._t2.Resume();
                
                personagemAtual = null;

                progressBar.SetActive(false);

                OnExitInteraction.Invoke();

            }

        }
        
    }

    public void EntrarNaFila(GameObject personagem) {

        fila.Enqueue( personagem );

        if( personagem.transform.name == "Player1" ) gameController._t1.Suspend();
        else gameController._t2.Suspend();
        
    }

    public GameObject GetPersonagem() {

        return this.personagemAtual;

    }

    public void Cancel() {

        progressBar.SetActive(false);

        this.interagindo = false;
        this.contadorTempo = 0f;

        if( personagemAtual.transform.name == "Player1" ) gameController._t1.Resume();
        else gameController._t2.Resume();

        this.personagemAtual = null;

        Debug.Log("cancelado");

    }

}
