using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    public Animator Animator;
    private int _state;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(_state == 0 || _state == 2)
            {
                _state = 1;
                Animator.SetInteger("State", 1);
            }
            else if(_state == 1)
            {
                _state = 2;
                Animator.SetInteger("State", 2);
            }
        }
    }
}
