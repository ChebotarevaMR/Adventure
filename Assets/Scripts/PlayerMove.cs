using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator Animator;
    public float Speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Animator.SetBool("isWalkUp", true);
            transform.position += Vector3.up * Speed * Time.deltaTime;
        }
        else
        {
            Animator.SetBool("isWalkUp", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Animator.SetBool("isWalkDown", true);
            transform.position -= Vector3.up * Speed * Time.deltaTime;
        }
        else
        {
            Animator.SetBool("isWalkDown", false);
        }
    }
}
