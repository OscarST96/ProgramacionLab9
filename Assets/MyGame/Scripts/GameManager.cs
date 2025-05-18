using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Referencia del player
    [SerializeField] private Player player1;//Almacenara el script del player 1.
    [SerializeField] private Player player2;//Almacenara el script del player 2. 
    [SerializeField] private Player player3;//Almacenara el script del player 3.
    [SerializeField] private Player playerSelect;
    //Referencia de los enemigos
    [SerializeField] private Enemy enemy1;
    [SerializeField] private Enemy enemy2;
    [SerializeField] private Enemy enemy3;
    //Referencia del spawner
    [SerializeField] private Spawner spawner;
    
    private int index;

    //getters, me permitiran leer las propiedades que deseo en otras clases.
    public int Index => index;
    public Player PlayerSelect => playerSelect;

    //Start se llama una vez al inicio.
    void Start()
    {
        InstatiatePlayer(player1, player2, player3);//Llamamos el metodo en el start, para que solo se llame una vez.
    }

    // Update es un metodo que se llama cada Frame por segundo(FPS).
    void Update()
    {
        spawner.InstatiateSpawner(enemy1.gameObject, enemy2.gameObject , enemy3.gameObject);//Llamamos al metodo atraves de la referencia de la clase spawner en Update para que este funcionando constantemente.
    }
    public void InstatiatePlayer(Player p1, Player p2, Player p3)//Este metodo spawneara al player en la escena.
    {
        GameObject instance = null;
        index = Random.Range(0, 3);//genera un numero (0, 1, 2), el cual se guarda en un int.
        switch (index)
        {
            case 0:
                instance = Instantiate(p1.gameObject, player1.transform.position, Quaternion.identity);//Se genera el jugador 1, y se guarda en un GameObject.
                Debug.Log("Se a generado el player 1");
                break;
            case 1:
                instance =  Instantiate(p2.gameObject, player2.transform.position, Quaternion.identity);//Se genera el jugador 2, y se guarda en un GameObject.
                Debug.Log("Se a generado el player 2");
                break;
            case 2:
                instance = Instantiate(p3.gameObject, player3.transform.position, Quaternion.identity);//Se genera el jugador 3, y se guarda en un GameObject.
                Debug.Log("Se a generado el player 3");
                break;
        }
        playerSelect = instance.GetComponent<Player>();//Asignamos a playerSelect el componente del player que a sido instanciado.
    }
}
