using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour
{

    // Start is called before the first frame update
    /// <summary>
    /// A paddle script
    /// </summary>
    
    // Paddle rigibody

    Rigidbody2D paddle;

    //the variable which keeps the half of the paddle collider
    float colliderHalfWidth;

    SpriteRenderer paddleSpriteRenderer;

    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;
    void Start()
    {
        paddle = GetComponent<Rigidbody2D>();

        //change the color
        paddleSpriteRenderer = GetComponent<SpriteRenderer>();
        Color color = Color.blue;
        paddleSpriteRenderer.color = color;

        //get the half of paddle collider
        Collider2D col =paddle.GetComponent<Collider2D>();
        colliderHalfWidth = (col.bounds.size.x)/2;
    }

    public float CalculateClampedX(float x)
    {
        if (x-colliderHalfWidth<ScreenUtils.ScreenLeft)
        {
            x=colliderHalfWidth+ScreenUtils.ScreenLeft;
        }
        if (x+colliderHalfWidth>ScreenUtils.ScreenRight)
        {
            x=ScreenUtils.ScreenRight-colliderHalfWidth;
        }
        return x;
    }
    
    /// <summary>
    /// Method FixedUpdate updates faster than Update method. 
    /// I am using this method for moving the paddle. 
    /// </summary>
    void FixedUpdate()
    {
        //save the pressing right or left buttom
        float horizontalInput = Input.GetAxis("Horizontal");
        //check is the right or left arrow has been pressed or not
        if (horizontalInput != 0)
        {
            Vector2 position = paddle.position;
            position.x +=horizontalInput*
                ConfigurationUtils.PaddleMoveUnitsPerSecond*
                Time.deltaTime;
            position.x = CalculateClampedX(position.x);

            paddle.MovePosition(position);
                
        }
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball")&&
            GetCollision(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                colliderHalfWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }
    /// <summary>
    /// indicate collision the top of the paddle and a ball. 
    /// Return the boolean if collision is hapenning
    /// </summary>
    /// <param name="coll">Collision2D</param>
    /// <returns></returns>
    bool GetCollision (Collision2D coll)
    {
        float tolerance = 0.05f;
        //two contacts points, one on the ball, the second one on the top of paddle
        ContactPoint2D[] contacts = new ContactPoint2D[2];
        coll.GetContacts(contacts);

        return Mathf.Abs(contacts[0].point.y - contacts[1].point.y) < tolerance;
    }
   
}
