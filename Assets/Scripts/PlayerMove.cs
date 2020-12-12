using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public enum State { Idle, WalkUp,WalkDown, WalkRight, WalkLeft};

    public Animator Animator;
    public float Speed = 10.0f;
    public Collider2D Collider;
    private State _state = State.Idle;
    private Vector3 _dir;
    private Collider2D _collider;
    private int _key = 1;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
    }

    private bool OverlapExist()
    {

        Collider2D[] allOverlappingColliders = new Collider2D[16];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        int overlapCount = Physics2D.OverlapCollider(Collider, contactFilter, allOverlappingColliders);
        string result = "";
        int ignore = 0;
        if (overlapCount > 0)
        {
            for (int i = 0; i < overlapCount; i++)
            {
                Collider2D x = allOverlappingColliders[i];
                if (x.gameObject.GetComponent<IgnoreCollider>() != null || !x.bounds.Intersects(Collider.bounds)) ignore++;
                result += " " + x.gameObject.name + " " + x.bounds.Intersects(Collider.bounds) + " " + x.bounds + " " + Collider.bounds;
            }
        }
        return (overlapCount - ignore) != 0;
    }



    // Update is called once per frame
    private void Update()
    {
        var currentState = State.Idle;
        _dir = new Vector3(0, 0, 0);
        var key = _key;
        if (Input.GetKey(KeyCode.W))
        {
            currentState = State.WalkUp;
            _dir = Vector3.up;
            key = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentState = State.WalkDown;
            _dir = Vector3.down;
            key = 2;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentState = State.WalkRight;
            _dir = Vector3.right;
            key = 3;
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentState = State.WalkLeft;
            _dir = Vector3.left;
            key = 4;
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
            Animator.SetInteger("State", (int)currentState);
            _state = currentState;
        }
        if(_key != key)
        {
            Animator.SetInteger("Key", key);
            _key = key;
        }
        if (_state != State.Idle)
        {
            var shift = _dir * Speed * Time.deltaTime;
            var position = transform.position + shift;
            Collider.offset = _collider.offset + (Vector2)shift;

            if (!OverlapExist())
            {
                transform.position = position;
            }
            Collider.offset = _collider.offset;
        }

    }
}
