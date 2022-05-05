using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// A ball script
    /// </summary>
    /// the duration of the ball life span
    Timer deathTime;

    //the delay to the ball moving
    Timer moveTime;

    void Start()
    {

        //float angle = 270 * Mathf.Deg2Rad;
        //Vector2 vector2 = new Vector2(ConfigurationUtils.BallMoveUnitsPerSecond * Mathf.Cos(angle),
        //    ConfigurationUtils.BallMoveUnitsPerSecond * Mathf.Sin(angle));
        //GetComponent<Rigidbody2D>().AddForce(vector2);

        //The timer is starting.
        //Add timer component to the ball for ball destroying
        deathTime = gameObject.AddComponent<Timer>();
        deathTime.Duration = ConfigurationUtils.BallLifeSeconds;
        deathTime.Run();

        //for ball moving delay
        moveTime = gameObject.AddComponent<Timer>();
        moveTime.Duration = 1;
        moveTime.Run();
    }
    /// <summary>
    /// The method changes the ball direction after collision
    /// </summary>
    public void SetDirection(Vector2 direction)
    {
        Rigidbody2D ball = GetComponent<Rigidbody2D>();
        float speed = ball.velocity.magnitude;
        ball.velocity = direction * speed;
    }
    // Update is called once per frame
    void Update()
    {
        //check the rest of moving time
        if (moveTime.Finished)
        {
            moveTime.Stop();
            StartMoving();
        }
        //check the rest of time
        if (deathTime.Finished)
        {
            Camera.main.GetComponent<BallSpawner>().SpawnBall();
            Destroy(gameObject);
        }
    }
    void StartMoving()
    {
        float angle = 270 * Mathf.Deg2Rad;
        Vector2 vector2 = new Vector2(ConfigurationUtils.BallMoveUnitsPerSecond * Mathf.Cos(angle),
            ConfigurationUtils.BallMoveUnitsPerSecond * Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().AddForce(vector2);
    }
    void OnBecameInvisible()
    {
        if (!deathTime.Finished)
        {
            float halfBallheight=gameObject.GetComponent<BoxCollider2D>().size.y/2;
            if ((transform.position.y-halfBallheight) <ScreenUtils.ScreenBottom)
            {
                Camera.main.GetComponent<BallSpawner>().SpawnBall();
                Destroy (gameObject);

            }
        }
    }
}
