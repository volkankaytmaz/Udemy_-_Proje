using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 5.0f;
    private bool _grounded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    // Start is called before the first frame update
    void Start()
    {
        //Assign Handle to Rigidbody
        _rigid = GetComponent<Rigidbody2D>();

        //Assign Handle to PlayerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();

        //Assign Handle to Playermove
        _playerSprite = GetComponentInChildren<SpriteRenderer>();

        //Assign Handle to Sword Arc
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>(); 
        //First child of Player animation
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); //Calling Movement Method
        
        //Checking condition if Player is grounded or not
        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _playerAnim.Attack(); //Call method
        }
    }

    void Movement()
    {
        //Left/Right Movement
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded(); //Initially Player will be grounded

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        //Jump player Logic
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down);

        if (hitInfo.collider != null)
        {
            if (_resetJump == false)
            {
                //Animator bool of Jump Player
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    //Face right of player and Running Logic
    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition; //temp variable for local position of that Sprite(SwordArc)
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if(faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition; //temp variable for local position of that Sprite(SwordArc)
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}
