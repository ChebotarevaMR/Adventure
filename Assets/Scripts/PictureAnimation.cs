using UnityEngine;

public class PictureAnimation : MonoBehaviour
{
    public Animator Animator;
    public float Delay = 10;
    private float _currentTime;
    

    // Update is called once per frame
    void Update()
    {
        if(_currentTime > Delay)
        {
            _currentTime = 0;
            var value = Animator.GetBool("State");
            Animator.SetBool("State", !value);
        }
        _currentTime += Time.deltaTime;
    }
}
