using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    //Publics
    public GameObject movementPosition;
    public GameObject[] sockets;
    public int availableSocketIndex;
    public List<GameObject> circles = new List<GameObject>();


    //Privates
    [SerializeField] private GameManager gameManager;


    public GameObject GetTopCircle()
    {
        return circles[^1]; // select last member of list
    }
}
