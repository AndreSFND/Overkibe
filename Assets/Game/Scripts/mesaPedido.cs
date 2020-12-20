using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mesaPedido : MonoBehaviour
{

    private GameObject personagem = null;
    private List<int> receita = null;
    [SerializeField] private GameObject balao = null;
    [SerializeField] private GameController gc = null;
    public UnityEvent OnCancel = null;

    public void iniciarPedido() {

        this.personagem = transform.GetComponent<mesaSemaforo>().GetPersonagem();

        List<int> receitaAtual = personagem.GetComponent<Player>().GetReceita();

        if( receitaAtual != null && receitaAtual.Count > 0 ) {

            OnCancel.Invoke();
            return;

        }

        List<Sprite> ingredientes = gc.getIngredientes();

        receita = new List<int> { 

            Random.Range(0, ingredientes.Count), 
            Random.Range(0, ingredientes.Count), 
            Random.Range(0, ingredientes.Count) 

        };

        balao.transform.Find("1").transform.GetComponent<SpriteRenderer>().sprite = ingredientes[ receita[0] ];
        balao.transform.Find("2").transform.GetComponent<SpriteRenderer>().sprite = ingredientes[ receita[1] ];
        balao.transform.Find("3").transform.GetComponent<SpriteRenderer>().sprite = ingredientes[ receita[2] ];

        balao.SetActive(true);

    }

    public void finalizarPedido() {

        balao.SetActive(false);

        personagem.GetComponent<Player>().DefinirReceita(receita);

    }

}
