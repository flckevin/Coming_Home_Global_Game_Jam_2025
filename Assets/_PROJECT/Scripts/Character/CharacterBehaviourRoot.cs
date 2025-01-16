using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviourRoot : MonoBehaviour
{
    [Header("Ground Check")]
    [HorizontalLine(padding = 20, thickness = 4)]
    public LayerMask groundMask;        //maks layer of the ground
    public Transform groundCheckPos;    //ground check position
    public float groundCheckRadius;     //ground radius

    //=================================== PRIVATE =================================== 
    private bool _isGrounded;                       //bool to check whether the character is grounded
    private PlayerController _playerController;     //player controller
    protected Rigidbody2D _playerRigi;                //player rigibody
    //================================================================================

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //========================= GET =========================
        _playerController = GameManager.Instance.PLR_playerController;
        _playerRigi = GetComponent<Rigidbody2D>();
        //=======================================================
    }

    // Update is called once per frame
    void Update()
    {
        HeightDetection();
    }

    void HeightDetection()
    {
        //detecting on ground
        _isGrounded = Physics2D.OverlapCircle((Vector2)groundCheckPos.position,groundCheckRadius,groundMask);
        //setting controller able to controll or
        _playerController.ableToDrag = _isGrounded;

    }

    public virtual void Onshoot(){}
}
