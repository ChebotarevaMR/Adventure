using UnityEngine;

public class TakeGlasses : MonoBehaviour
{
    public GlassesFly GlassesFly;
    private bool _isTakingGlasses;
    void Update()
    {
        if (_isTakingGlasses && !GlassesFly.IsBreak && !GlassesFly.IsFly)
        {
            GlassesFly.transform.position = transform.position + Vector3.up * 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!GlassesFly.IsBreak && !GlassesFly.IsFly)
            {
                _isTakingGlasses = true;
            }
        }
    }
}
