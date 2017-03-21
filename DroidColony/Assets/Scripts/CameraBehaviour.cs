using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public Vector3 speed;
    public float accel;
    




	// Use this for initialization
	void Start () {
        
        speed = new Vector3 (0.0f, 0.0f, 0.0f);
        accel = 0.1f;

    }
	
	// Update is called once per frame
	void Update () {
        var player = GameObject.Find("Player");

        var playerX = player.transform.position.x;
        var playerY = player.transform.position.y;

        var playerSpeed = player.GetComponent<Rigidbody2D>().velocity;
        
        Camera camera = GetComponent<Camera>();
        var rightBound = camera.ViewportToWorldPoint(new Vector3(0.7f, 0.0f, camera.nearClipPlane));
        var upBound = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.65f, camera.nearClipPlane));
        var leftBound = camera.ViewportToWorldPoint(new Vector3(0.3f, 0.0f, camera.nearClipPlane));
        var downBound = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.35f, camera.nearClipPlane));

        var rightAccelZone = camera.ViewportToWorldPoint(new Vector3(0.6f, 0.0f, camera.nearClipPlane));
        var upAccelZone = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.6f, camera.nearClipPlane));
        var leftAccelZone = camera.ViewportToWorldPoint(new Vector3(0.4f, 0.0f, camera.nearClipPlane));
        var downAccelZone = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.4f, camera.nearClipPlane));
        //print("playerX " + playerX);
        //print("rightBound " + rightBound);
        //print("leftBound " + leftBound);


        //Accellerate camera toward player if player is leaving the camera bounds
        if ((playerX > rightAccelZone.x) && (playerSpeed.x > 0))
        {
            speed.x += ((playerSpeed.x - speed.x) / 30);
        }
        if (playerX > rightBound.x)
        {
            if (playerSpeed.x > 2.0f)
                speed.x = playerSpeed.x * (1.2f);
            else
                speed.x = 2.0f;
        }

        
        if ((playerX < leftAccelZone.x) && (playerSpeed.x < 0))
        {
            
            speed.x += ((playerSpeed.x - speed.x) / 30);
            
        }
        if (playerX < leftBound.x)
        {
            if (playerSpeed.x < -2.0f)
                speed.x = playerSpeed.x * (1.2f);
            else
                speed.x = -2.0f;
        }

        //X brakes
        if ((playerX < rightAccelZone.x) && (playerX > leftAccelZone.x))
        {
            

            if (speed.x > (2 * accel))
            {
                if (playerSpeed.x < 1.0f)
                {
                    speed.x -= accel * 8;
                }
                else 
                    speed.x -= accel;
            }
            else if (speed.x < -(2 * accel))
            {
                if (playerSpeed.x < 1.0f)
                {
                    speed.x += accel * 8;
                }
                else
                    speed.x += accel;
            }
            else
            {
                speed.x = 0;
            }
        }


        if ((playerY > upAccelZone.y) && (playerSpeed.y > 0))
        {
            speed.y += ((playerSpeed.y - speed.y) / 30);
        }

        if (playerY > upBound.y)
        {
            if (playerSpeed.y > 2.0f)
                speed.y = playerSpeed.y * (1.2f);
            else
                speed.y = 2.0f;
        }


        if ((playerY < downAccelZone.y) && (playerSpeed.y < 0))
        {
            speed.y += ((playerSpeed.y - speed.y) / 30);
        }

        if (playerY < downBound.y)
        {
            if (playerSpeed.y < -2.0f)
                speed.y = playerSpeed.y * (1.2f);
            else
                speed.y = -2.0f;
        }

        

        //Y brakes
        if ((playerY < upAccelZone.y) && (playerY > downAccelZone.y))
        {
            


            if (speed.y > (2 * accel))
            {
                if (playerSpeed.y < 1.0f)
                {
                    speed.y -= accel * 8;
                }
                else
                    speed.y -= accel;
            }
            else if (speed.y < -(2 * accel))
            {
                if (playerSpeed.y < 1.0f)
                {
                    speed.y += accel * 8;
                }
                else
                    speed.y += accel;
            }
            else
            {
                speed.y = 0;
            }
        }

        //Move Camera based on speed
        //transform.Translate(speed);

        GetComponent<Rigidbody2D>().velocity = speed;
       

        //Check for scroll wheel to zoom
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            //zoom in
            
            camera.orthographicSize -= 0.1f;
        
        }
        else if (d < 0f)
        {
            //zoom out
            

            camera.orthographicSize += 0.1f;
            
        }
    }
}
