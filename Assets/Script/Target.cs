using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour, ITarget
{
    [SerializeField] UnityEvent hitEvent;
    [SerializeField] GameObject hitSoundObj;
    public int scoreReward = 1;

    [SerializeField] float movingTargetBounds;
    bool _moving = false;
    int _direction = 1;
    float _speed = 1;
    float _minMoveX, _maxMoveX;

    bool _allowDestroy = false, _allowDestroyStartTarget;
    float _destroyPosY = 0;

    bool _initialMove = true;
    float _initialMovePosY = 0;

    void Start()
    {
        _minMoveX = transform.position.x - movingTargetBounds;
        _maxMoveX = transform.position.x + movingTargetBounds;
        _initialMovePosY = transform.position.y + 5;
        _destroyPosY = transform.position.y;

        _direction = Random.Range(-1, 2);
        GetComponent<Collider>().enabled = false;

        RandomizeMovingTarget();
    }

    void Update()
    {
        if (_initialMove)
        {
            if (transform.position.y < _initialMovePosY) transform.position = new Vector3(transform.position.x, transform.position.y + (10 * Time.deltaTime), transform.position.z);
            else
            {
                transform.position = new Vector3(transform.position.x, _initialMovePosY, transform.position.z);
                GetComponent<Collider>().enabled = true;
                _initialMove = false;
            }
        }

        if (_moving)
        {
            if (transform.position.x > _maxMoveX) _direction = -1;
            if (transform.position.x < _minMoveX) _direction = 1;

            transform.position = new Vector3(transform.position.x + (_direction * _speed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (_allowDestroy)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (10 * Time.deltaTime), transform.position.z);
            if (transform.position.y <= _destroyPosY) Destroy(transform.parent.gameObject);
        }

        if (_allowDestroyStartTarget)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (10 * Time.deltaTime), transform.position.z);
            if (transform.position.y <= _destroyPosY) Destroy(transform.gameObject);
        }
    }

    public void Hit()
    {
        hitEvent.Invoke();
        CreateHitSound();
    }

    public void DestroyTarget()
    {
        _allowDestroy = true;
    }
    public void DestroyStartTarget()
    {
        _allowDestroyStartTarget = true;
    }

    public void DestroyGoodTarget()
    {
        RandomSpawner.goodTargets--;
        DestroyTarget();
    }

    public void DestroyBadTarget()
    {
        RandomSpawner.badTargets--;
        DestroyTarget();
    }

    void RandomizeMovingTarget()
    {
        int isMoving = Random.Range(0, 2);
        if (isMoving == 1) _moving = true;
    }

    void CreateHitSound()
    {
        GameObject newObj = Instantiate(hitSoundObj, transform.position, Quaternion.identity);
        newObj.transform.parent = null;
    }
}
