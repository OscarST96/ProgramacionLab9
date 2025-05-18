using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float timeSpawner = 5f;//cada cuanto va a aparecer un enemigo.
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;
    [SerializeField] private Transform positionC;

    private float currentTime = 0;//almacena el tiempo trascurrido.

    public void InstatiateSpawner(GameObject enemy1, GameObject enemy2, GameObject enemy3)//este metodo se tiene que llamar en el update, y se pide 3 objectos.
    {
        currentTime += Time.deltaTime;//currentTime almacena el tiempo de Time.deltaTime.
        if (currentTime >= timeSpawner)//Si currentTime es mayor o igual al timeSpawner.
        {
            int index = UnityEngine.Random.Range(0, 3);//genera un numero (0, 1, 2), el cual se guarda en un int.
            switch (index)
            {
                //Instantiate es un metodo de MonoBehaviour el cual genera un objecto en la escena.
                case 0:
                    //Instantiate nos pide un object, la posicion y la rotacion.
                    Instantiate(enemy1, positionA.position, Quaternion.identity);//Se genera el enemigo 1.
                    Debug.Log("Se a generado el enemigo 1");
                    break;
                case 1:
                    Instantiate(enemy2, positionB.position, Quaternion.identity);//Se genera el enemigo 2.
                    Debug.Log("Se a generado el enemigo 2");
                    break;
                case 2:
                    Instantiate(enemy3, positionC.position, Quaternion.identity);//Se genera el enemigo 3.
                    Debug.Log("Se a generado el enemigo 3");
                    break;
            }
            currentTime = 0;//El tiempo trascurrido se reinicia;
        }
    }
}
