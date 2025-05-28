using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 5f;//Velocidad en la que se movera el player.
    [SerializeField] private float cadence = 1f;
    [SerializeField] private float impulseDash = 1f;
    [SerializeField] private float couldown = 5;
    [SerializeField] private int stamina;
    [SerializeField] private int staminaMax;
    [SerializeField] private BulletController bullet;

    private Rigidbody rb;//Es un componente dentro de unity, el cual trabaja con fisicas.
    private Vector3 direction;//Almacena la posicion del puntero o mouse.
    private float currentTime = 0;
    private GameObject bulletInstatiate;
    private int score = 0;
    private bool isDash = true;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public Vector3 Direction => direction;
    public GameObject BulletInstatiate => bulletInstatiate;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Movement();
        RotationPlayer();
        Shoot();
    }
    private void FixedUpdate()
    {
        Dash();
    }
    private void Movement()
    {
        /*//Movimiento del player
        float x = Input.GetAxis("Horizontal");//horizontal es una palabra clave del input, la cual va del 0 al 1, este valor lo guardamos en un float.
        float y = Input.GetAxis("Vertical");//Vertical es una palabra clave del input, la cual va del 0 al 1, este valor lo guardamos en un float.
        Vector3 direction = new Vector3(x, 0, y);//Creamos un Vector3 para asignar nuestros dos flotantes.
        rb.linearVelocity = direction * speed;//Le damos la direcion en la que se va a mover el jugador, multiplicado por speed a rb.velocity.*/
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
    private void RotationPlayer()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hit))
        {
            Vector3 lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            direction = new Vector3(lookPos.x, transform.position.y, lookPos.z);
            transform.root.LookAt(lookPos);
        }
    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            currentTime += Time.deltaTime;
            if (currentTime > cadence)
            {
                // Instanciamos la bala en la posición del jugador y en la misma rotación que él
                Instantiate(bullet.gameObject, transform.position, transform.rotation);
                currentTime = 0;
            }
        }

    }
    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isDash && stamina > 0)
            {
                rb.AddForce(transform.forward * impulseDash, ForceMode.Impulse);
                stamina -= 20;
                Debug.Log("Dash, " + stamina);
                isDash = false;
            }
            Invoke("OnDash", couldown);
            if (stamina <= 0)
            {
                stamina += staminaMax;
                print("se a reseteado tu stamina.");
            }
        }
    }
    private void OnDash()
    {
        isDash = true;
        Debug.Log("Se ha activado nuevamente el dash.");
    }
}
