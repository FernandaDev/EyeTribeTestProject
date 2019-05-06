using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class Challenge : MonoBehaviour
{
	protected GameObject _currentThrowableObject;

    [SerializeField] protected Transform throwableItemSpawnPosition;

	[SerializeField] protected ObjectPool _objectPool;

    [SerializeField] Text scoreText;

    int _currentScore = 0; // the score of the current challenge
    public int CurrentScore
    {
        get { return _currentScore; }

        protected set
        {
            if (value != _currentScore)
            {
                _currentScore = value;
                UpdateScoreUI(); // every time the score changes, we need to update de score text.
            }
        }
    }

    public virtual void SpawnThrowableObject()
	{
		_currentThrowableObject = _objectPool.GetGameObjectFromPool();

        if(throwableItemSpawnPosition != null)
		    _currentThrowableObject.transform.position = throwableItemSpawnPosition.position;

		_currentThrowableObject.transform.parent = transform.GetChild(0); // make this a child of the "scene" gameObject
	}

	public void DespawnCurrentThrowableObject()
	{
		_objectPool.ReturnGameObjectToPool(_currentThrowableObject);
		_currentThrowableObject = null;
	}

	public void Respawn()
	{
        DespawnCurrentThrowableObject();
		SpawnThrowableObject();
	}

	public virtual void StartChallenge()
	{
        UpdateScoreUI();

        if (_currentThrowableObject == null)
		{
			SpawnThrowableObject();
		}
	}

    public virtual void EndChallenge()
    {
        CurrentScore = 0;
        DespawnCurrentThrowableObject();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public ObjectPool GetObjectPool()
    {
        return _objectPool;
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score\n" + CurrentScore.ToString();
    }

    public virtual void AddScore()
    {
        CurrentScore++;
    }
}
