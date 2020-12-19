using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesaPedido : MonoBehaviour
{

    private GameObject personagem = null;
    [SerializeField] private GameObject balao = null;

    public void iniciarPedido() {

        this.personagem = transform.GetComponent<mesaSemaforo>().GetPersonagem();

        balao.SetActive(true);

    }

    public void finalizarPedido() {

        balao.SetActive(false);

        personagem.GetComponent<Player>().DefinirPrato(57);

    }

}
