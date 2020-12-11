using UnityEngine;

public class GlassesFly : MonoBehaviour
{
    public Animator Animator;
    public float Speed = 1.0f;
    public Transform Player;
    private bool _isFly;
    private bool _isBreak;
    void Update()
    {
        if (_isFly && !_isBreak && transform.position.y > Player.position.y)
        {
            transform.position -= Vector3.up * Speed * Time.deltaTime;
            if(transform.position.y <= Player.position.y && !_isBreak)
            {
                _isBreak = true;
                Animator.SetBool("isBreak", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !_isFly)
        {
            _isFly = true;
            Animator.SetBool("isFly", true);
        }        
    }
}
