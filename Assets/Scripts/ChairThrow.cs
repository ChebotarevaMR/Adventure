using UnityEngine;

public class ChairThrow : MonoBehaviour
{
    public Animator Animator;
    public float Speed = 1;
    public bool IsThrow { get; private set; }   
    public bool IsBreak { get; private set; }
    private Vector3 _dir = Vector3.left;
    private float _time = 3;
    private float _currentTime = -1;
    public void Throw()
    {
        IsThrow = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (IsThrow && !IsBreak)
        {
            if (_currentTime < 0)
            {
                _currentTime = 0;
                Animator.SetInteger("State", 1);
            }
            _currentTime += Time.deltaTime;
            transform.position += _dir * Speed * Time.deltaTime;
            if (_currentTime > _time)
            {
                IsBreak = true;
                Animator.SetInteger("State", 2);
            }
        }
    }
}
