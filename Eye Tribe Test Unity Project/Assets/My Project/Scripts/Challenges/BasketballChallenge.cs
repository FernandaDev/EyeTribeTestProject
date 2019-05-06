using System;
using UnityEngine;

public class BasketballChallenge : Challenge 
{
    [SerializeField] Transform basketBallHoop; // reference to the hoop GameObject so we can move it.
    [SerializeField] Transform[] hoopSpawnPositions; // all the possible positions for the hoop to spawn.
    int currentHoopPosition; // keeps track of the hoop's position, and then add the score based on it.

    void ChangeHoopToRandomPosition()
    {
        int randomIndex = UnityEngine.Random.Range(0, hoopSpawnPositions.Length);

        int randomXposition = UnityEngine.Random.Range(-1,1);

        currentHoopPosition = randomIndex;

        basketBallHoop.transform.position = new Vector3( hoopSpawnPositions[randomIndex].position.x + (randomXposition*3f), // get a random side
                                                        basketBallHoop.position.y,                   // keep the height
                                                        hoopSpawnPositions[randomIndex].position.z);

        // As soon as we change the position, we dont need to listen anymore, since we only set a new position when we score.
        InteractiveObjectController.OnDespawn -= ChangeHoopToRandomPosition;
    }

    public override void AddScore()
    {
        CurrentScore += (1 + currentHoopPosition); // add the score based on the position of the hoop (by distance)

        // We need to subscribe only when we score. And then we will wait a bit to change the position.
        InteractiveObjectController.OnDespawn += ChangeHoopToRandomPosition;
    }
}
