using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mesaIngrediente : MonoBehaviour
{

    private GameObject personagem = null;
    [SerializeField] private int ingrediente = 0;
    public UnityEvent OnCancel = null;

    public void iniciarColeta() {

        this.personagem = transform.GetComponent<mesaSemaforo>().GetPersonagem();

        if( this.personagem.GetComponent<Player>().GetPrato() != -1 ) {

            OnCancel.Invoke();

        }

    }

    public void pegarIngrediente() {

        personagem.GetComponent<Player>().SegurarIngrediente( this.ingrediente );

    }

}
