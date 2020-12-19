using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mesaSemaforo : MonoBehaviour
{

    private Queue<GameObject> fila = new Queue<GameObject>();
    
    [SerializeField] float duracao = 3f;

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

            OnStartInteraction.Invoke();

        } else if ( interagindo ) {

            if( contadorTempo < duracao ) {

                contadorTempo += Time.deltaTime;

            } else if( contadorTempo >= duracao ) {

                interagindo = false;
                contadorTempo = 0f;

                personagemAtual.GetComponent<Player>().Destravar();
                personagemAtual = null;

                OnExitInteraction.Invoke();

            }

        }
        
    }

    public void EntrarNaFila(GameObject personagem) {

        fila.Enqueue( personagem );
        
        personagem.GetComponent<Player>().Travar();

    }

    public GameObject GetPersonagem() {

        return this.personagemAtual;

    }

}
