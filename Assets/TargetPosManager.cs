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
        GetComponentInChildren<Target>().DestroyTarget();
    }

    public void PlayAnimation(string animation)
    {
        _animator.Play(animation);
    }
}
