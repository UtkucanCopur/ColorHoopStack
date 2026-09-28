using UnityEngine;

public class Circle : MonoBehaviour
{
    //Publics
    public GameObject locatedStand;
    public GameObject locatedSocket;
    public bool canMove;
    public string color;
    public GameManager gameManager;



    //Privates
    private GameObject _movingPosition;
    private GameObject _nextStand;
    private bool _selected;
    private bool _changePos;
    private bool _fitSocket;
    private bool _returnSocket;


    private void Update()
    {
        if (_selected)
        {
            transform.position = Vector3.Lerp(transform.position, _movingPosition.transform.position, .2f);
            if (Vector3.Distance(transform.position,_movingPosition.transform.position) < .10f)
            {
                _selected = false;

            }
        }

        if (_changePos)
        {
            transform.position = Vector3.Lerp(transform.position, _movingPosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, _movingPosition.transform.position) < .10f)
            {
                _changePos = false;
                _fitSocket = true;

            }
        }

        if (_fitSocket)
        {
            transform.position = Vector3.Lerp(transform.position, locatedSocket.transform.position, .2f);
            if (Vector3.Distance(transform.position, locatedSocket.transform.position) < .10f)
            {
                transform.position = locatedSocket.transform.position;
                _fitSocket = false;

                locatedStand = _nextStand;

                if (locatedStand.GetComponent<Stand>().circles.Count > 1)
                {
                    locatedStand.GetComponent<Stand>().circles[^2].GetComponent<Circle>().canMove = false;
                }
                gameManager.isMoving = false;
            }
        }


    }


    public void Move(string process, GameObject stand = null,GameObject socket = null, GameObject movingObject = null)
    {
        switch(process)
        {
            case "Selected":
                _movingPosition = movingObject;
                _selected = true;
                break;
            case "ChangePosition":
                _nextStand = stand;
                locatedSocket = socket;
                _movingPosition = movingObject;
                _fitSocket = false;
                _changePos = true;
                break;
            case "FitSocket":

                break;
            case "ReturnSocket":

                break;
        }
    }


}
