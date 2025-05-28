using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float timeLife = 5;
    
    [SerializeField]private GameManager gameManager;
    [SerializeField]private Player player;

    private Rigidbody rb;
    private Vector3 direction;
    private float currentTime = 0;
    public float Speed => speed;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();//Obtiene los componentes del player que esta en escena.
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        if (player == null && gameManager.PlayerSelect != null)//Si player es null, y gameManager.PlayerSelect no es null.
        {
            player = gameManager.PlayerSelect;//Asignamos el gameManager.PlayerSelect a player.
        }
        direction = (player.Direction - player.transform.position).normalized;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameManager.PlayerSelect.Score += 1;
            Debug.Log("Score: " + gameManager.PlayerSelect.Score);
        }
    }
    private void Movement()
    {
        rb.linearVelocity = direction * speed;
        DestroyBullet();
    }
    private void DestroyBullet()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeLife)
        {
            Destroy(gameObject);
        }
    }
}
