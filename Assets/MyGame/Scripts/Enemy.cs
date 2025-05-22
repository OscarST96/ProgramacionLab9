using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;//velocidad del enemigo
    [SerializeField] private float separationRadius = 3f;//velocidad del enemigo
    [SerializeField] private Player player;//referencia del player
    [SerializeField] private GameManager gameManager;

    private Rigidbody rb;//Es un componente dentro de unity, el cual trabaja con fisicas.
    public bool Follow = false;//Para que siga al player
    public bool MoveAway = false;//Para que se aleje del player 
    public bool FollowSeparate = false;//

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (player == null && gameManager.PlayerSelect != null)//Si player es null, y gameManager.PlayerSelect no es null.
        {
            player = gameManager.PlayerSelect;//Asignamos el gameManager.PlayerSelect a player.
        }
    }
    private void FixedUpdate()
    {
        if (player == null) return;
        OnFollow();
        OnMoveAway();
        OnFollowSeparate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Score--;
            Destroy(this.gameObject);
        }
    }
    #region Metodos
    //Metodo para que siga al jugador
    private void OnFollow()
    {
        if (Follow == false && FollowSeparate == true || MoveAway == true) return;//Verificamos que solo este activo el bool Follow.
        
        Vector3 direction = (player.transform.position - transform.position).normalized;//disminuimos la distancia, para ello se toma la posicion del player y la restamos con la del enemigo, utilizando normalized.
        rb.velocity = direction * speed;//asignamos la velocidad y la direccion en la que se movera.
    }
    //Metodo para que se aleje del jugador
    private void OnMoveAway()
    {
        if (MoveAway == false && Follow == true || FollowSeparate == true) return;//Verificamos que solo este activo el bool MoveAway.

        Vector3 direction = (transform.position - player.transform.position).normalized;//Aumentamos la distancia, para ello se toma la posicion del enemigo y la restamos con la del player, utilizando normalized.
        rb.velocity = direction * speed;//asignamos la velocidad y la direccion en la que se movera.
    }
    //Metodo para que siga al jugador, pero tomando distancia con los demas enemigos
    private void OnFollowSeparate()
    {
        if (FollowSeparate == false && Follow == true || MoveAway == true) return;//Verificamos que solo este activo el bool FollowSeparate.

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;//disminuimos la distancia, para ello se toma la posicion del player y la restamos con la del enemigo, utilizando normalized.
        Vector3 separation = Vector3.zero;//Este vector acumulará la fuerza de separación respecto a los otros enemigos.

        Enemy[] allEnemies = FindObjectsOfType<Enemy>();//Busca todos los enemigos en la escena, para calcular separación con cada uno.
        
        //Itera sobre todos los enemigos.
        foreach (Enemy other in allEnemies)
        {
            if (other != this)//Ignora a sí mismo
            {
                float distance = Vector3.Distance(transform.position, other.transform.position);//Calcula la distancia a cada otro enemigo y la guarda en un float.
                if (distance < separationRadius)//Si la distancia es menor que el radio establecido
                {
                    //Calcula una fuerza que se aleje del otro enemigo.
                    separation += (transform.position - other.transform.position).normalized / distance;//Cuanto más cerca estén, mayor será la fuerza de repulsión
                }
            }
        }
        //Combina la dirección hacia el jugador con la fuerza de separación de los demás enemigos.
        Vector3 finalDirection = (directionToPlayer + separation).normalized;
        rb.velocity = finalDirection * speed;//Mueve al enemigo con la velocidad deseada en la dirección calculada.
    }
    #endregion
}
