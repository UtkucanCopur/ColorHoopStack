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
    private int completedCircleIndex;

    public GameObject GetTopCircle()
    {
        if (circles.Count == 0) return null;
        return circles[^1]; // select last member of list
    }

    public void HandleSocketProcess(GameObject objtectToRemove)
    {
        circles.Remove(objtectToRemove);
        
        if (circles.Count != 0)
        {
            availableSocketIndex--;
            circles[^1].GetComponent<Circle>().canMove = true;
        }
        else
        {
            availableSocketIndex = 0;
        }
    }

    public GameObject GetAvailableSocket()
    {
        return sockets[availableSocketIndex];
    }


    public void ControlCircles()
    {
        if (circles.Count == 4)
        {
            string color = circles[0].GetComponent<Circle>().color;


            foreach (var item in circles)
            {
                if (color == item.GetComponent<Circle>().color)
                    completedCircleIndex++;
            }

            if (completedCircleIndex == 4)
            {
                gameManager.StandCompleted();
                CompletedStandProcess();
            }
            else
            {
                
                completedCircleIndex = 0;
            }

        }
    }

    private void CompletedStandProcess()
    {
        foreach (var item in circles)
        {
            item.GetComponent<Circle>().canMove = false;
            Color color = item.GetComponent<MeshRenderer>().material.color;
            color.a = 0.5f;
            item.GetComponent<MeshRenderer>().material.color = color;
            gameObject.tag = "CompletedStand";

        }
    }


}
