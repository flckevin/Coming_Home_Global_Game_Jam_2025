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
    public float dragFroce;             //force to add after drag
    public float touchZDepth;           //depth of z
    //============================= PRIVATE ============================= 
    private Camera _cam;        //camera main
    private bool _isDragging;   //identifier to check whether the player is draggung the ball
    //===================================================================

    // Start is called before the first frame update
    void Start()
    {
        //============================= GET ============================= 
        //get the camera if the camera does not exist
        if(_cam == null) _cam = Camera.main;
        //===============================================================

        //============================= SET ============================= 
        ControllerInitialize();
        //===============================================================
    }

    // Update is called once per frame
    void Update()
    {
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
                DragHandle(_currentPos);
            }
            //if the player end or cancle touch
            else if(_touch.phase == TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
            {
                //disable line
                lineRend.enabled = false;
                //set is dragging to false
                _isDragging = false;
            }
        }
    }

    private void DragStartHanlde(Vector3 _pos)
    {
        //enable line
        lineRend.enabled = true;
        //set true to is dragging
        _isDragging = true;
        //set start pos
        lineRend.SetPosition(0,_pos);
        //set end pos
        lineRend.SetPosition(1,_pos);
    }

    private void DragHandle(Vector3 _pos)
    {
        Vector3 _startPos = lineRend.GetPosition(0);
        //set end pos
        lineRend.SetPosition(1,_pos);
    }
}
