using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class ShootTheTarget : Challenge
{
    Sprite reticleSprite;
    [SerializeField] Sprite aimSprite;
    [SerializeField] Image reticleImage;

    [SerializeField] Transform[] targetSpawnPositions;
    [SerializeField] ObjectPool targetPool;

    GameObject breakeableObject;

    private void OnEnable()
    {
        Breakable.OnTargetDestroy += OnTargetDestroy;
    }
    private void OnDisable()
    {
        Breakable.OnTargetDestroy -= OnTargetDestroy;
    }

    private void Start()
    {
        reticleSprite = reticleImage.sprite;
    }

    public override void StartChallenge()
    {
        base.StartChallenge();

        ChangeReticleImage(aimSprite);
        SpawnTargetInRandomPosition();
    }

    public void ChangeReticleImage(Sprite _sprite)
    {
        reticleImage.sprite = _sprite;
    }

    public override void EndChallenge()
    {
        ChangeReticleImage(reticleSprite);

        if(breakeableObject!=null)
            targetPool.ReturnGameObjectToPool(breakeableObject);

        base.EndChallenge();
    }

    public override void SpawnThrowableObject()
    {
        _currentThrowableObject = _objectPool.GetGameObjectFromPool();
        _currentThrowableObject.transform.position = reticleImage.transform.position;
        _currentThrowableObject.transform.parent = transform.GetChild(0);
    }

    void OnTargetDestroy(GameObject _breakableObject)
    {
        breakeableObject = _breakableObject;
        SpawnTargetInRandomPosition();
        targetPool.ReturnGameObjectToPool(_breakableObject);
    }

    public void SpawnTargetInRandomPosition()
    {
        int rand = UnityEngine.Random.Range(0, targetSpawnPositions.Length);

        GameObject target = targetPool.GetGameObjectFromPool();

        breakeableObject = target;

        target.transform.position = targetSpawnPositions[rand].position;
        target.transform.parent = targetSpawnPositions[rand];
    }

}
