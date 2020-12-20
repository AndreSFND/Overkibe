using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mesaPreparacao : MonoBehaviour
{

    private GameObject personagem = null;
    public UnityEvent OnCancel = null;

    public void iniciarPreparacao() {

        this.personagem = transform.GetComponent<mesaSemaforo>().GetPersonagem();

        List<int> receita = personagem.GetComponent<Player>().GetReceita();
        Queue<int> ingredientes = personagem.GetComponent<Player>().GetIngredientes();

        if( receita == null || ingredientes == null || receita.Count == 0 || ingredientes.Count == 0 ) {

            OnCancel.Invoke();

        }

    }

    public void finalizarPreparacao() {

        List<int> receita = personagem.GetComponent<Player>().GetReceita();
        Queue<int> ingredientes = personagem.GetComponent<Player>().GetIngredientes();

        bool receitaCorreta = true;
        int counter = 0;
        foreach( int i in ingredientes ) {

            if( i != receita[counter] ) {

                receitaCorreta = false;
                break;

            }

            counter++;

        }

        if( ingredientes.Count < 3 ) receitaCorreta = false;

        personagem.GetComponent<Player>().LimparReceita();
        personagem.GetComponent<Player>().LimparIngredientes();

        if( receitaCorreta ) {
            
            personagem.GetComponent<Player>().DefinirPrato(0);

        } else {

            Debug.Log("errado!");

            personagem.GetComponent<Player>().DefinirPrato(1);

        }

    }

}
