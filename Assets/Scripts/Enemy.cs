using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, WalkUp, WalkDown, WalkLeft, WalkRight, JumpUp, JumpDown, JumpLeft, JumpRight, Attack }
    public Animator Animator;
    public float Speed = 2;
    public AStart AStart;

    public GameObject Player;
    private Dictionary<Vector2Int, Vector2Int> _path;
    private Vector2Int _start;
    private Vector2Int _target;
    private Vector3 _dir;

    private bool _isFollow;

    private void Start()
    {

    }

    private void UpdateAnimation()
    {
        if (!_isFollow) Animator.Play("Idle");
        else
        {
            if (Mathf.Abs(_start.x - _target.x) <= 1 && Mathf.Abs(_start.y - _target.y) <= 1)
            {
                if (_dir.x < 0)
                {
                    Animator.Play("JumpRight");
                }
                else if (_dir.x > 0)
                {
                    Animator.Play("JumpLeft");
                }
                else if (_dir.y < 0)
                {
                    Animator.Play("JumpUp");
                }
                else if (_dir.y > 0)
                {
                    Animator.Play("JumpDown");
                }
            }
            else
            {
                if (_dir.x < 0)
                {
                    Animator.Play("WalkRight");
                }
                else if (_dir.x > 0)
                {
                    Animator.Play("WalkLeft");
                }
                else if (_dir.y < 0)
                {
                    Animator.Play("WalkUp");
                }
                else if (_dir.y > 0)
                {
                    Animator.Play("WalkDown");
                }
            }
        }
    }

    private void Update()
    {
        var follow  = Mathf.Abs((transform.position - Player.transform.position).magnitude) < 25;
        Debug.Log(follow);
        if(!_isFollow && follow)
        {
            Debug.Log("1");
            _isFollow = true;
            _start = AStart.Convert(transform.position);
            _target = AStart.Convert(Player.transform.position);
            _path = AStart.SetStartAndTargetPosition(transform.position, Player.transform.position);
            transform.position = AStart.Convert(_start);
            var prev = GetPrev();
            if (!_path.ContainsKey(prev))
            {
                Debug.Log("1 Not contains key " + prev);
                _isFollow = false;
                UpdateAnimation();
                return;
            }
            _dir = transform.position - AStart.Convert(prev);
        }
        else if(_isFollow && !follow)
        {
            Debug.Log("2");
            _isFollow = false;
        }

        if (_isFollow)
        {
            Debug.Log("3");
            var start = AStart.Convert(transform.position);
            var target = AStart.Convert(Player.transform.position);
            if(_start != start)
            {
                _start = start;
                transform.position = AStart.Convert(_start);
                var prev = GetPrev();
                if (!_path.ContainsKey(prev))
                {
                    Debug.Log("2 Not contains key " + prev);
                    _isFollow = false;
                    UpdateAnimation();
                    return;
                }
                _dir = transform.position - AStart.Convert(prev);
            }
            if (_target != target)
            {
                _start = start;
                _target = target;
                _path = AStart.SetStartAndTargetPosition(transform.position, Player.transform.position);
                transform.position = AStart.Convert(_start);
                var prev = GetPrev();
                if (!_path.ContainsKey(prev))
                {
                    Debug.Log("2 Not contains key " + prev);
                    _isFollow = false;
                    UpdateAnimation();
                    return;
                }
                _dir = transform.position - AStart.Convert(prev);

            }
            Debug.Log("4 " + _dir + " " + _start + " " + GetPrev());
            transform.position -= _dir * Time.deltaTime * Speed;

        }
        UpdateAnimation();
    }

    private Vector2Int GetPrev()
    {
        var start = _target;
        var prev = new Vector2Int(-1, -1);
        if (_path != null)
        {
            int k = 0;
            while (start != _start && k < 100)
            {
                if (_path.ContainsKey(start))
                {
                    prev = start;
                    start = _path[start];
                }
                else
                {
                    break;
                }

                k++;
            }
        }
        return prev;
    }

    private void OnDrawGizmos()
    {
        if (_path == null) return;
        var start = _target;
        int k = 0;
        while (start != _start && k < 100)
        {
            Gizmos.DrawSphere(AStart.Convert(start), 0.3f);
            if (_path.ContainsKey(start))
            {
                start = _path[start];
            }
            else
            {
                break;
            }

            k++;
        }
    }

}
