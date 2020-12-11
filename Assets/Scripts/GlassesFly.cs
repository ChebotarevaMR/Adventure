using UnityEngine;

public class GlassesFly : MonoBehaviour
{
    public Animator Animator;
    public float Speed = 1.0f;
    public Transform Player;
    public bool IsFly { get; private set; }
    public bool IsBreak { get; private set; }
    void Update()
    {
        if (IsFly && !IsBreak && transform.position.y > Player.position.y)
        {
            transform.position -= Vector3.up * Speed * Time.deltaTime;
            if(transform.position.y <= Player.position.y && !IsBreak)
            {
                IsBreak = true;
                Animator.SetBool("isBreak", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !IsFly)
        {
            IsFly = true;
            Animator.SetBool("isFly", true);
        }        
    }
}
