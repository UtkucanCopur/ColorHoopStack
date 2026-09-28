using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Publics
    public bool isMoving;
    public int targetStandCount;

    //Privates
    private GameObject _selectedObject;
    private GameObject _selectedStand;
    private Circle _circle;
    private int _completedStandCount;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100))
            {
                if (hitInfo.collider != null && hitInfo.collider.CompareTag("Stand"))
                {
                    
                    if (_selectedObject != null && _selectedStand != hitInfo.collider.gameObject)
                    {
                        //sending circle
                    } else
                    {
                        Stand stand = hitInfo.collider.GetComponent<Stand>();
                        _selectedObject = stand.GetTopCircle();
                        _circle = _selectedObject.GetComponent<Circle>();
                        isMoving = true;

                        if (_circle.canMove)
                        {
                            _circle.Move("Selected",null,null,_circle.locatedStand.GetComponent<Stand>().movementPosition);

                            _selectedStand = _circle.locatedStand;

                        }


                    }

                }
            }
        }
    }
}
