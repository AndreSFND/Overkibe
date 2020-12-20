using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class mesaEntrega : MonoBehaviour
{

    [SerializeField] private GameObject balao = null;
    [SerializeField] private Sprite feliz = null;
    [SerializeField] private Sprite bravo = null;

    private GameObject personagem = null;
    public UnityEvent OnCancel = null;

    public void iniciarEntrega() {

        this.personagem = transform.GetComponent<mesaSemaforo>().GetPersonagem();
        int prato = this.personagem.GetComponent<Player>().GetPrato();

        if( prato == -1 ) {

            OnCancel.Invoke();
            return;

        }

        if( prato == 0 ) this.balao.GetComponent<SpriteRenderer>().sprite = feliz;
        else this.balao.GetComponent<SpriteRenderer>().sprite = bravo;

        this.balao.SetActive(true);

    }

    public void finalizarEntrega() {
        
        Player player = this.personagem.GetComponent<Player>();

        if( player.GetPrato() == 0 ) {

            player.AdicionarPontos(10);

        } else {

            player.AdicionarPontos(-10);

        }

        player.DefinirPrato(-1);
        player.LimparIngredientes();

        this.balao.SetActive(false);

    }

}
