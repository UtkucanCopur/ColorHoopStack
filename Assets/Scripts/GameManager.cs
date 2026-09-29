using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Publics
    public bool isMoving;
    public int targetStandCount;
    public AudioSource audioSource;
    public AudioClip holdClip;
    public AudioClip fitClip;
    public GameObject completedPanel;


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
                        Stand stand = hitInfo.collider.GetComponent<Stand>();

                        if (stand.circles.Count != 4 && stand.circles.Count != 0)
                        {
                            if (_circle.color == stand.circles[^1].GetComponent<Circle>().color)
                            {
                                _selectedStand.GetComponent<Stand>().HandleSocketProcess(_selectedObject);
                                _circle.Move("ChangePosition", hitInfo.collider.gameObject, stand.GetAvailableSocket(), stand.movementPosition);
                                stand.availableSocketIndex++;
                                stand.circles.Add(_selectedObject);
                                _selectedObject = null;
                                _selectedStand = null;
                                stand.ControlCircles();
                                audioSource.PlayOneShot(fitClip);
                            } else
                            {
                                _circle.Move("ReturnSocket");
                                _selectedObject = null;
                                _selectedStand = null;
                                audioSource.PlayOneShot(holdClip);
                            }


                            
                        } else if (stand.circles.Count == 0) 
                        {
                            _selectedStand.GetComponent<Stand>().HandleSocketProcess(_selectedObject);
                            _circle.Move("ChangePosition", hitInfo.collider.gameObject, stand.GetAvailableSocket(), stand.movementPosition);
                            stand.availableSocketIndex++;
                            stand.circles.Add(_selectedObject);
                            stand.ControlCircles();
                            _selectedObject = null;
                            _selectedStand = null;
                            audioSource.PlayOneShot(fitClip);
                        } else
                        {
                            _circle.Move("ReturnSocket");
                            _selectedObject = null;
                            _selectedStand = null;
                            audioSource.PlayOneShot(holdClip);
                        }


                        

                    } else if (_selectedStand == hitInfo.collider.gameObject) 
                    {
                        _circle.Move("ReturnSocket");
                        _selectedObject = null;
                        _selectedStand = null;
                        audioSource.PlayOneShot(holdClip);

                    } else
                    {
                        Stand stand = hitInfo.collider.GetComponent<Stand>();
                        _selectedObject = stand.GetTopCircle();
                        _circle = _selectedObject.GetComponent<Circle>();
                        isMoving = true;

                        if (_circle.canMove)
                        {
                            _circle.Move("Selected", null, null, _circle.locatedStand.GetComponent<Stand>().movementPosition);

                            _selectedStand = _circle.locatedStand;
                            audioSource.PlayOneShot(holdClip);
                        }


                    }

                }
            }
        }
    }


    public void StandCompleted()
    {
        _completedStandCount++;
        if (_completedStandCount == targetStandCount)
        {
            completedPanel.SetActive(true);
        }
            
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
