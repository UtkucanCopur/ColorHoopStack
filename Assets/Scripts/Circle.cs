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
    private GameObject _locatedStand;
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

                break;
            case "FitSocket":

                break;
            case "ReturnSocket":

                break;
        }
    }


}
