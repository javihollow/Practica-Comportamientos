﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject maitre;
    public GameObject cliente;
    public GameObject catmarero;
    public GameObject catco;


    private float countdown;
    private int numClientes;

    //Provisional para probar al ladrón
    /*private Mundo mundo;
    private GameObject plato;*/

    void Start()
    {
        countdown = Random.Range(10, 20);
        numClientes = 1;
        Instantiate(maitre);
        Instantiate(catmarero);
        Instantiate(cliente);
        Instantiate(catco);

        //Provisional
        /*mundo = GetComponent<Mundo>();
        plato = mundo.plato;
        mundo.setPlato("A", 1, plato);*/
    }

    void Update()
    {
        if (numClientes < 11)
        {
            if (countdown <= 0)
            {
                countdown = Random.Range(10, 20);
                Instantiate(cliente);
                numClientes++;
            }

            countdown -= Time.deltaTime;
        }
    }

}
