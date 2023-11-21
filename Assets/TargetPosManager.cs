using UnityEngine;

public class TargetPosManager : MonoBehaviour
{
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    public void PlayAnimation()
    {
        _animator.Play("TargetFlip");
    }
}
