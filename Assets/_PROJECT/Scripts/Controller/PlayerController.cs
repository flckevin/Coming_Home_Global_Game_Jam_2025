using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controller General Info")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public Rigidbody2D objectRb;        //physic of the object
    public LineRenderer lineRend;       //line render for dragging feedback
    
    
    [Header("Drag Setting"),Space(10)]
    [HorizontalLine(padding = 20, thickness = 4)]
    public float dragLimit;             //limit length of drag
    public float ShootForce;            //force to add after drag
    public float touchZDepth;           //depth of z
    public bool ableToDrag;             //bool to identify whether the player able to drag

    //============================= PRIVATE ============================= 
    private Camera _cam;                                //camera main
    private bool _isDragging;                           //identifier to check whether the player is draggung the ball
    private CharacterBehaviourRoot _characterBehaviour;  //behaviour of the character
    //===================================================================

    void Awake()
    {
        //assign playe controller to this class
        GameManager.Instance.PLR_playerController = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //============================= GET ============================= 
        //get the camera if the camera does not exist
        if(_cam == null) _cam = Camera.main;
        _characterBehaviour = objectRb.GetComponent<CharacterBehaviourRoot>();
        //===============================================================

        //============================= SET ============================= 
        ControllerInitialize();
        //===============================================================
    }

    // Update is called once per frame
    void Update()
    {
        //if the controll not able to drag then stop execute
        if(ableToDrag == false) return;
        Controller();
    }

    private void ControllerInitialize()
    {
        #region ================== LINE SETUP ================== 
        //set the number of verticle inline
        lineRend.positionCount = 2;
        //set start pos
        lineRend.SetPosition(0,Vector2.zero);
        //set end pos
        lineRend.SetPosition(1,Vector2.zero);
        //disable line render on start since playe rhave not drag yet
        lineRend.enabled = false;
        #endregion
    }

    /// <summary>
    /// function to controll the dragging
    /// </summary>
    private void Controller()
    {
        if(Input.touchCount > 0)
        {
            //get the first touch position
            Touch _touch = Input.GetTouch(0);
            //convert the touch position to be the postion on the world
            Vector3 _currentPos = _cam.ScreenToWorldPoint(_touch.position);
            //change the depth of z
            _currentPos.z = touchZDepth;

            //if the player start to touch
            if(_touch.phase == TouchPhase.Began)
            {
                DragStartHanlde(_currentPos);
            }
            //if the player drag
            else if(_touch.phase == TouchPhase.Moved)
            {
                DragMoveHandle(_currentPos);
            }
            //if the player end or cancle touch
            else if(_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            {
                DragEndHandle(_currentPos);
            }
        }
    }

    #region ============== DRAG HANDLE EVENTS ==============

    /// <summary>
    /// Event on drag controller start
    /// </summary>
    /// <param name="_touchPos"> position of the touch </param>
    private void DragStartHanlde(Vector3 _touchPos)
    {
        objectRb.velocity = Vector3.zero;
        objectRb.isKinematic = true;
        //enable line
        lineRend.enabled = true;
        //set true to is dragging
        _isDragging = true;
        //set start pos
        lineRend.SetPosition(0,objectRb.position);
        //set end pos
        lineRend.SetPosition(1,_touchPos);
    }

    /// <summary>
    /// Event to handle on drag move
    /// </summary>
    /// <param name="_touchPos"> touch position </param>
    private void DragMoveHandle(Vector3 _touchPos)
    {
        //declare vector 3 to store start position
        Vector3 _startPos = lineRend.GetPosition(0);
        //declare vector 3 to store shoot distance
        Vector3 _distance = _touchPos - _startPos;
        
        //if the length of the line have not reach limit
        if(_distance.magnitude < dragLimit)
        {
            //keep strethching the line
            lineRend.SetPosition(1,_touchPos);
        }
        else // the line reaches to the limit
        {
            //calculate the limited line
            Vector3 _limitedLine = _startPos + (_distance.normalized * dragLimit);
            //set the line to be static at that position
            lineRend.SetPosition(1,_limitedLine);
        }
    }

    /// <summary>
    /// Event functaion to handle on drag end
    /// </summary>
    /// <param name="_touchPos"> touch position </param>
    private void DragEndHandle(Vector3 _touchPos)
    {
        //call character behaviour on shoot
        _characterBehaviour.Onshoot();
        //set object kinimatic to fase
        objectRb.isKinematic = false;
        //disable line
        lineRend.enabled = false;
        //set is dragging to false
        _isDragging = false;

        //get start position
        Vector3 _startPos = lineRend.GetPosition(0);
        //get current position
        Vector3 _currentPos = lineRend.GetPosition(1);

        //calculate distance
        Vector3 _distance = _currentPos - _startPos;
        //calculate final force to add
        Vector3 _finalForce = _distance * ShootForce;

        //if the object does not exist then stop execute
        if(objectRb == null) return;
        //add force to the ball
        objectRb.AddForce(-_finalForce,ForceMode2D.Impulse);
    }

    #endregion
}
