using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    /// <summary>
    /// Ball spawner
    /// </summary>
    [SerializeField]
    GameObject prefabBall;

    //Timer, to spawn balls
    Timer spawnBall;
    //random number to get delay
    float spawnRange;

    //location of new ball spawning
    Vector2 spawnPositionMin;
    Vector2 spawnPositionMax;
    bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        //get the ball coordinates
        GameObject tempBall = GameObject.Instantiate(prefabBall);
        BoxCollider2D ballCol=tempBall.GetComponent<BoxCollider2D>();
        //ball colider half sizes
        float ballColHalfWidth = ballCol.size.x / 2;
        float ballColHalfHeight = ballCol.size.y / 2;
        // get the spawn location
        spawnPositionMin=new Vector2 (tempBall.transform.position.x-ballColHalfWidth,
            tempBall.transform.position.y-ballColHalfHeight);
        spawnPositionMax=new Vector2 (tempBall.transform.position.x+ballColHalfWidth,
            tempBall.transform.position.y+ballColHalfHeight);
        Destroy (tempBall);

        //spawn ball for the game beggining
        SpawnBall();

        //get the spawnRange
        spawnRange = ConfigurationUtils.MaxSpawnTime - ConfigurationUtils.MinSpawnTime;

        //Retrieve the Timer component
        spawnBall = gameObject.AddComponent<Timer>();

        //duration of the spawn balls
        spawnBall.Duration = SpawnTimerDelay();
        spawnBall.Run();
    }
    /// <summary>
    /// Spawn a new ball, after the timer has run out
    /// </summary>
    public void SpawnBall()
    {
        //GameObject tempBall = GameObject.Instantiate(prefabBall);
        if (Physics2D.OverlapArea(spawnPositionMin, spawnPositionMax)==null)
        {
            spawning = false;
            Instantiate(prefabBall);
        }
        else
        {
            spawning = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnBall.Finished)
        {
            SpawnBall();
            spawnBall.Duration=SpawnTimerDelay();
            spawnBall.Run();
        }
        //try to spawn again, if ball colided before
        if (spawning)
        {
            SpawnBall();
        }
    }
    float SpawnTimerDelay()
    {
        return ConfigurationUtils.MinSpawnTime+Random.value*spawnRange;
    }
}
