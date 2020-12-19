using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesaIngrediente : MonoBehaviour
{

    private GameObject personagem = null;
    [SerializeField] private int ingrediente = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void iniciarColeta() {

        this.personagem = transform.GetComponent<mesaSemaforo>().GetPersonagem();

    }

    public void pegarIngrediente() {

        personagem.GetComponent<Player>().SegurarIngrediente( this.ingrediente );

    }

}
