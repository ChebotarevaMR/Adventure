using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public enum State { Idle, WalkUp,WalkDown, WalkRight, WalkLeft};

    public Animator Animator;
    public float Speed = 1.0f;
    private State _state = State.Idle;
    private Vector3 _dir;
    private Rigidbody2D _rigidBody;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (_state != State.Idle)
        {
            _rigidBody.MovePosition(transform.position + _dir * Speed * Time.deltaTime);
            //transform.position += _dir * Speed * Time.deltaTime;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        var currentState = State.Idle;
        _dir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            currentState = State.WalkUp;
            _dir = Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentState = State.WalkDown;
            _dir = Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentState = State.WalkRight;
            _dir = Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentState = State.WalkLeft;
            _dir = Vector3.left;
        }
        // Action
        if (Input.GetKey(KeyCode.E))
        {
            currentState = State.Idle;
            var chair = FindObjectOfType<ChairThrow>();
            if(!chair.IsBreak && !chair.IsThrow)
            {
                chair.Throw();
            }
        }
        if (_state != currentState)
        {
            Debug.Log((int)currentState);
            Animator.SetInteger("State", (int)currentState);
            _state = currentState;
        }
        
    }
}
