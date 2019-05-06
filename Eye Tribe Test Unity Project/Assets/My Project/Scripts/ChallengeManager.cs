using System.Collections;
using System.Collections.Generic;
using EyeTribe.Unity.Calibration;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class ChallengeManager : MonoBehaviour 
{
	public static ChallengeManager instance;

	private Challenge _currentChallenge;
    public Challenge CurrentChallenge{
        get {return _currentChallenge;}

        private set {
            if(value != _currentChallenge)
            {
                if (_currentChallenge != null)
                {
                    InteractiveObjectController.OnDespawn -= _currentChallenge.Respawn;
                    Score.OnScore -= _currentChallenge.AddScore;
                }

                _currentChallenge = value;

                Score.OnScore += _currentChallenge.AddScore;
                InteractiveObjectController.OnDespawn += _currentChallenge.Respawn;
            }

        }
    }

	[SerializeField] Challenge[] challenges;

	[SerializeField] Button _calibrateButton;
	[SerializeField] Button _backButton;
	
	Button[] _challengeButtons;

	[SerializeField] EyeUI _eyeUI;

    [SerializeField] Transform _challengesButtonUI;

	void Awake()
	{
		if(instance == null)
			instance = this;

		_backButton.onClick.AddListener( ()=> { OnChallengeExit(); } );

		for (int i = 0; i < _challengesButtonUI.childCount; i++)
		{
            int index = i;
			_challengesButtonUI.GetChild(i).GetComponent<Button>().onClick.AddListener
			( () => { SetupChallenge(index); } );
		}
	}

	public void SetupChallenge(int currentChallengeIndex)
	{
		
		SetCurrentChallenge(currentChallengeIndex);

		CurrentChallenge.transform.GetChild(0).gameObject.SetActive(true); // turn on the whole scene.
        CurrentChallenge.StartChallenge(); // Start the Challenge.

		_eyeUI.TurnOff(); // turn off the eyes.

		_calibrateButton.gameObject.SetActive(false); // turn off the calibrate button.

		_challengesButtonUI.gameObject.SetActive(false);// Turn off the challenges button panel in the left side.
		
		_backButton.gameObject.SetActive(true); // turn on the back button, so you can exit the challenge.
	}

	void SetCurrentChallenge(int currentChallengeIndex)
	{
		if(currentChallengeIndex >= challenges.Length) // check if the index is bigger than the amount of challenges.
			return;

        CurrentChallenge = challenges[currentChallengeIndex];
	}

	void OnChallengeExit()
	{
        CurrentChallenge.EndChallenge(); // this is a virtual function that handles what needs to be reset for the current challenge.
		_eyeUI.TurnOn(); // turn the eyes back on.
		_calibrateButton.gameObject.SetActive(true); // turn back on the calibrate button
		_challengesButtonUI.gameObject.SetActive(true); // turn back on the challenges panel in the left side.
		_backButton.gameObject.SetActive(false); // turn off the back button.
	}

    public ObjectPool GetCurrentOjectPool()
    {
        return CurrentChallenge.GetObjectPool();
    }

}
