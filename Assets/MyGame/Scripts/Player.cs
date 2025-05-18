using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float speed = 5f;//Velocidad en la que se movera el player.
    private Rigidbody rb;//Es un componente dentro de unity, el cual trabaja con fisicas.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //Movimiento del player
        float x = Input.GetAxis("Horizontal");//horizontal es una palabra clave del input, la cual va del 0 al 1, este valor lo guardamos en un float.
        float y = Input.GetAxis("Vertical");//Vertical es una palabra clave del input, la cual va del 0 al 1, este valor lo guardamos en un float.
        Vector3 direction = new Vector3( x, 0, y);//Creamos un Vector3 para asignar nuestros dos flotantes.
        rb.velocity = direction * speed;//Le damos la direcion en la que se va a mover el jugador, multiplicado por speed a rb.velocity.
    }
}
