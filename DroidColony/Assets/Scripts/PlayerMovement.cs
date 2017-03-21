using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip playerDash;

    public bool invulnerable;
    public int invulTime;
    public int invulCounter = 0;

    public int maxHP;
    public int hp;
    
    public float speed = 10.0f;
    public float accel = 0.002f;

    public int upgradePoints;
    public static int storyend = 0;

    public float terminalVelocity;
    public float fastTerminal;
    public float slowTerminal;
    public float brakes;

    private float xVel;
    private float yVel;
    private float velocityMagnitude;
    private float velocityTheta;


    private bool dashing;


    public float dashSpeed;
    public int dashCooldown;
    private int dashCounter = 0;

    public int dashTime;
    private int dashTimeCounter = 0;

    public int spikeDashDamage;

    public GameObject CurrentPeripheral;

    public int peripheralType;  
    public GameObject Spike;    //peripheral type 1
    public GameObject Shield;   //peripheral type 2
    public GameObject Gun;      //peripheral type 3
    public GameObject Orbital;  //peripheral type 4
    public GameObject Flash;    //Hit Reaction

    public GameObject Bullet;   //Bullet for Gun Peripheral
    public float bulletSpeed;

    private float shootStallTime = 10;
    private float shootStallCounter = 0;

    public float shotCooldownTime;
    private float shotCooldownCounter = 0;
    public float totalShots;
    private float currentShots;

    private bool bashing = false;
    public float bashDestroyThreshold;
    public float bashSpeed;


    private float spikeAngle;
    private float mouseAngle;
    private float targetAngle;

    private int spikeQuadrant;
    private int mouseQuadrant;
    private int targetQuadrant;

    private float spikeXDistance;
    private float spikeYDistance;
    private float mouseXDistance;
    private float mouseYDistance;

    private Vector3 realMousePos;

    private float rightStickY;
    private float rightStickX;

    private float leftStickY;
    private float leftStickX;

    public float spikeRotationTolerance;
    public float spikeRotateSpeed;

    public float dampingRange;
    public float dampingAmmount;

    private Rigidbody2D rB;


    // Use this for initialization
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        CurrentPeripheral = Spike;
    }

    // Update is called once per frame
    void Update()
    {
        storyend--;
        handleCooldowns();
        setAttributes();
        switchPeripheral();

        //if(Input.GetButton("Fire2") || (Input.GetAxis("Fire2") == 1))
        //{
        //    //Spike dash
        //    if (peripheralType == 1)
        //    {
        //        if (dashCounter <= 0)
        //        {
        //            leftDash();
        //            dashCounter = dashCooldown;
        //            dashTimeCounter = dashTime;
        //            dashing = true;
        //        }
        //    }
        //}
        //Action
        if (Input.GetButton("Fire1") || (Input.GetAxis("Fire1") == 1))
        {
            //Spike dash
            if (peripheralType == 1)
            {
                if (dashCounter <= 0)
                {
                    dash();
                    invulnerable = true;
                    dashCounter = dashCooldown;
                    dashTimeCounter = dashTime;
                    dashing = true;
                    AudioSource.PlayClipAtPoint(playerDash, transform.position);
                    storyend = 1;
		}
            }
            //Gun shoot
            if (peripheralType == 2)
            {
                if (shootStallCounter <= 0)
                {
                    if (currentShots > 0)
                    {
                        currentShots--;
                        shotCooldownCounter = (totalShots - currentShots) * (shotCooldownTime / totalShots);
                        shoot();
                        shootStallCounter = shootStallTime;
                    }
                }
            }

            if(peripheralType == 3)
            {
                shieldBash();
                bashing = true;
            }
        }
        else 
        {
            bashing = false;
        }

        
        //Get Input
        rightStickX = Input.GetAxisRaw("RightHorizontal");
        rightStickY = Input.GetAxisRaw("RightVertical");
        leftStickY = Input.GetAxisRaw("LeftVertical");
        leftStickX = Input.GetAxisRaw("LeftHorizontal");

        //these functions take care of player movement and peripheral rotation based on axis values
        handleMovement();
        rotatePeripheral();
    }

    void leftDash()
    {
        //if no controller use mouse
        if (Input.GetJoystickNames().Length == 0)
        {
            //get real mouse position relative to screen
            realMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //get angle the mouse is at relative to player
            mouseXDistance = realMousePos.x - transform.position.x;
            mouseYDistance = realMousePos.y - transform.position.y;
        }
        else    //use controller
        {
            mouseXDistance = leftStickX;
            mouseYDistance = leftStickY;
        }

        xVel = 0;
        yVel = 0;
        
        //have logic based on what quadrant its in, to keep angle accurate
        if (mouseXDistance >= 0 && mouseYDistance >= 0)
        {
            //first quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance));
            mouseQuadrant = 1;

            xVel = Mathf.Cos(mouseAngle) * dashSpeed;
            yVel = Mathf.Sin(mouseAngle) * dashSpeed;
        }
        else if (mouseXDistance <= 0 && mouseYDistance >= 0)
        {
            //second quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (Mathf.PI / 2f);
            mouseQuadrant = 2;

            xVel = Mathf.Cos(mouseAngle) * dashSpeed;
            yVel = Mathf.Sin(mouseAngle) * dashSpeed;
        }
        else if (mouseXDistance <= 0 && mouseYDistance <= 0)
        {
            //third quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance)) + (Mathf.PI);
            mouseQuadrant = 3;

            xVel = Mathf.Cos(mouseAngle) * dashSpeed;
            yVel = Mathf.Sin(mouseAngle) * dashSpeed;

        }
        else if (mouseXDistance >= 0 && mouseYDistance <= 0)
        {
            //fourth quadrant
            mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (3f * Mathf.PI / 2f);
            mouseQuadrant = 4;

            xVel = Mathf.Cos(mouseAngle) * dashSpeed;
            yVel = Mathf.Sin(mouseAngle) * dashSpeed;
        }

        rB.velocity = new Vector3(xVel, -yVel, 0f);
    }

    void shieldBash()
    {
        //get angle that spike is at relative to player
        spikeXDistance = Spike.transform.position.x - transform.position.x;
        spikeYDistance = Spike.transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (spikeXDistance >= 0 && spikeYDistance >= 0)
        {
            //first quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance));
            spikeQuadrant = 1;
        }
        else if (spikeXDistance <= 0 && spikeYDistance >= 0)
        {
            //second quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (Mathf.PI / 2f);
            spikeQuadrant = 2;
        }
        else if (spikeXDistance <= 0 && spikeYDistance <= 0)
        {
            //third quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance)) + (Mathf.PI);
            spikeQuadrant = 3;
        }
        else if (spikeXDistance >= 0 && spikeYDistance <= 0)
        {
            //fourth quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (3f * Mathf.PI / 2f);
            spikeQuadrant = 4;
        }

        float xVel = bashSpeed * Mathf.Cos(spikeAngle);
        float yVel = bashSpeed * Mathf.Sin(spikeAngle);

        rB.AddForce(new Vector3(xVel, yVel));
    }

    void shoot()
    {
        //get angle that spike is at relative to player
        spikeXDistance = Spike.transform.position.x - transform.position.x;
        spikeYDistance = Spike.transform.position.y - transform.position.y;

        //have logic based on what quadrant its in, to keep angle accurate
        if (spikeXDistance >= 0 && spikeYDistance >= 0)
        {
            //first quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance));
            spikeQuadrant = 1;
        }
        else if (spikeXDistance <= 0 && spikeYDistance >= 0)
        {
            //second quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (Mathf.PI / 2f);
            spikeQuadrant = 2;
        }
        else if (spikeXDistance <= 0 && spikeYDistance <= 0)
        {
            //third quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance)) + (Mathf.PI);
            spikeQuadrant = 3;
        }
        else if (spikeXDistance >= 0 && spikeYDistance <= 0)
        {
            //fourth quadrant
            spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (3f * Mathf.PI / 2f);
            spikeQuadrant = 4;
        }

        float bulletStartX;
        float bulletStartY;

        bulletStartX = Gun.transform.position.x + 0.5f * Mathf.Cos(spikeAngle);
        bulletStartY = Gun.transform.position.y + 0.5f * Mathf.Sin(spikeAngle);

        GameObject tempBullet = (GameObject)Instantiate(Bullet, new Vector3(bulletStartX, bulletStartY), Bullet.transform.rotation);
        tempBullet.SetActive(true);

        tempBullet.GetComponent<BulletMotion>().velocity = new Vector3(bulletSpeed * Mathf.Cos(spikeAngle) + rB.velocity.x, 
                                                                        bulletSpeed * Mathf.Sin(spikeAngle) + rB.velocity.y);
    }

    void setAttributes()
    {
        if(peripheralType == 1)
        {
            terminalVelocity = fastTerminal;
        }
        else if (peripheralType == 2)
        {
            terminalVelocity = fastTerminal + 2f;
        }
        else if (peripheralType == 3)
        {
            terminalVelocity = slowTerminal;

            //if bashing, lower turn speed
            

            //if speed magnitude is greater than bash destroy threshold, set the shield threshold
            if (Mathf.Sqrt(Mathf.Pow(rB.velocity.x, 2) + (Mathf.Pow(rB.velocity.y, 2))) > bashDestroyThreshold)
            {
                Shield.GetComponent<PlayerShieldBehavior>().destroy = true;
            }
            else
            {
                Shield.GetComponent<PlayerShieldBehavior>().destroy = false;
            }
        }
        else if (peripheralType == 4)
        { 
        }

        if (bashing)
        {
            spikeRotateSpeed = 50f;
        }
        else
        {
            spikeRotateSpeed = 1000f;
        }
    }

    void switchPeripheral()
    {
        if (Input.GetButton("P1"))
        {
            CurrentPeripheral.gameObject.SetActive(false);
            peripheralType = 1;
            CurrentPeripheral = Spike;
            Spike.gameObject.SetActive(true);
        }
        if (Input.GetButton("P2"))
        {
            CurrentPeripheral.gameObject.SetActive(false);
            peripheralType = 2;
            CurrentPeripheral = Gun;
            Gun.gameObject.SetActive(true);
        }
        if (Input.GetButton("P3"))
        {
            CurrentPeripheral.gameObject.SetActive(false);
            peripheralType = 3;
            CurrentPeripheral = Shield;
            Shield.gameObject.SetActive(true);
        }
    }

    void handleMovement()
    {
        if (!(dashing || bashing))
        {
            //find magnitude and angle of velocity vector
            velocityMagnitude = Mathf.Sqrt(Mathf.Pow(rB.velocity.x, 2) + (Mathf.Pow(rB.velocity.y, 2)));

            //setVelocity();

            accelerate();
            
        }
    }
    void accelerate()
    {

        //apply extra slowdown if going diagonal and above terminal velocity
        if (Mathf.Sqrt(Mathf.Pow(rB.velocity.x, 2) + (Mathf.Pow(rB.velocity.y, 2))) > terminalVelocity)
        {
            rB.velocity = new Vector3(rB.velocity.x * 0.9f, rB.velocity.y * 0.9f, 0f);
        }
        else
            rB.AddForce(new Vector3(leftStickX * accel, leftStickY * -accel, 0f));

        //brakes
        if (leftStickX == 0)
        {
            if (Mathf.Abs(rB.velocity.x) > 0.5f)
            {

                rB.AddForce(new Vector3(Mathf.Sign(rB.velocity.x) * -1 * brakes, 0f, 0f));

            }
            else rB.velocity = new Vector3(0f, rB.velocity.y, 0f);
        }
        if (leftStickY == 0)
        {
            if (Mathf.Abs(rB.velocity.y) > 0.5f)
            {

                rB.AddForce(new Vector3(0f, Mathf.Sign(rB.velocity.y) * -1 * brakes, 0f));

            }
            else rB.velocity = new Vector3(rB.velocity.x, 0f, 0f);
        }
    }

    float setVelocity()
    {
        velocityTheta = 0f;
        
        //have logic based on what quadrant its in, to keep angle accurate
        if (leftStickX == 0 && leftStickY == 0)
        {
            rB.velocity = new Vector3(0f, 0f, 0f);
        }
        else if (leftStickX >= 0 && leftStickY >= 0)
        {
            //first quadrant
            velocityTheta = Mathf.Atan(Mathf.Abs(leftStickY / leftStickX));
            rB.velocity = new Vector3(speed * Mathf.Cos(velocityTheta), -speed * Mathf.Sin(velocityTheta), 0f);
        }
        else if (leftStickX <= 0 && leftStickY >= 0)
        {
            //second quadrant
            velocityTheta = Mathf.Atan(Mathf.Abs(leftStickY / leftStickX)) + (Mathf.PI / 2f);
            rB.velocity = new Vector3(-speed * Mathf.Sin(velocityTheta), speed * Mathf.Cos(velocityTheta), 0f);
        }
        else if (leftStickX <= 0 && leftStickY <= 0)
        {
            //third quadrant
            velocityTheta = Mathf.Atan(Mathf.Abs(leftStickY / leftStickX)) + (Mathf.PI);
            rB.velocity = new Vector3(speed * Mathf.Cos(velocityTheta), -speed * Mathf.Sin(velocityTheta), 0f);
        }
        else if (leftStickX >= 0 && leftStickY <= 0)
        {
            //fourth quadrant
            velocityTheta = Mathf.Atan(Mathf.Abs(leftStickY / leftStickX)) + (3f * Mathf.PI / 2f);
            rB.velocity = new Vector3(-speed * Mathf.Sin(velocityTheta), speed * Mathf.Cos(velocityTheta), 0f);
        }
        
        return velocityTheta;
    }

    void handleCooldowns()
    {
        if(invulnerable)
        {
            Flash.SetActive(true);
            if (invulCounter++ >= invulTime)
            {
                Flash.SetActive(false);
                invulnerable = false;
                invulCounter = 0;
            }
        }

        if(dashing)
        {
            Spike.GetComponent<SpikeBehavior>().damage = spikeDashDamage;
        }
        else
        {
            Spike.GetComponent<SpikeBehavior>().damage = 0;
        }
        //handle dash cooldown
        if (dashCounter > 0)
        {
            dashCounter--;
        }
        else
        {
            dashCounter = 0;
        }

        //handle dash time
        if (dashTimeCounter > 0)
        {
            dashTimeCounter--;
        }
        else
        {
            dashing = false;
            dashTimeCounter = 0;
        }

        if (shootStallCounter > 0)
        {
            shootStallCounter--;
        }
        else
        {
            shootStallCounter = 0;
        }

        if (shotCooldownCounter > 0)
        {
            shotCooldownCounter--;
        }
        else
        {
            shotCooldownCounter = 0;
            currentShots = totalShots;
        }
    }

    //debugging messages
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 400), "SpikeAngle: " + spikeAngle
                                            + "\nPlayerAngle: "
                                            + "\nRightHorizontal: " + rightStickX
                                            + "\nRightVertical: " + rightStickY
                                            + "\n--HP = " + hp
                                            + "\nCurrentShots = " + currentShots
                                            + "\nSpeed = " + Mathf.Sqrt(Mathf.Pow(rB.velocity.x, 2) + (Mathf.Pow(rB.velocity.y, 2)))
                                            + "\nMouse Pos: " + realMousePos
                                            + "\nUpgrade: " + upgradePoints
                                            );
    }

    void dash()
    {
        //if no controller use mouse
        if (Input.GetJoystickNames().Length == 0)
        {
            //get real mouse position relative to screen
            realMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //get angle the mouse is at relative to player
            mouseXDistance = realMousePos.x - transform.position.x;
            mouseYDistance = realMousePos.y - transform.position.y;
        }
        else    //use controller
        {
            mouseXDistance = rightStickX;
            mouseYDistance = rightStickY;
        }

        xVel = 0;
        yVel = 0;
        if (rightStickX == 0 && rightStickY == 0)
        {
            //get angle that spike is at relative to player
            spikeXDistance = Spike.transform.position.x - transform.position.x;
            spikeYDistance = Spike.transform.position.y - transform.position.y;

            //have logic based on what quadrant its in, to keep angle accurate
            if (spikeXDistance >= 0 && spikeYDistance >= 0)
            {
                //first quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance));
                spikeQuadrant = 1;
            }
            else if (spikeXDistance <= 0 && spikeYDistance >= 0)
            {
                //second quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (Mathf.PI / 2f);
                spikeQuadrant = 2;
            }
            else if (spikeXDistance <= 0 && spikeYDistance <= 0)
            {
                //third quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance)) + (Mathf.PI);
                spikeQuadrant = 3;
            }
            else if (spikeXDistance >= 0 && spikeYDistance <= 0)
            {
                //fourth quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (3f * Mathf.PI / 2f);
                spikeQuadrant = 4;
            }
            xVel = Mathf.Cos(spikeAngle) * dashSpeed;
            yVel = Mathf.Sin(spikeAngle) * dashSpeed;
        }
        else
        { 
            //have logic based on what quadrant its in, to keep angle accurate
            if (mouseXDistance >= 0 && mouseYDistance >= 0)
            {
                //first quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance));
                mouseQuadrant = 1;

                xVel = Mathf.Cos(mouseAngle) * dashSpeed;
                yVel = Mathf.Sin(mouseAngle) * dashSpeed;
            }
            else if (mouseXDistance <= 0 && mouseYDistance >= 0)
            {
                //second quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (Mathf.PI / 2f);
                mouseQuadrant = 2;

                xVel = Mathf.Cos(mouseAngle) * dashSpeed;
                yVel = Mathf.Sin(mouseAngle) * dashSpeed;
            }
            else if (mouseXDistance <= 0 && mouseYDistance <= 0)
            {
                //third quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance)) + (Mathf.PI);
                mouseQuadrant = 3;

                xVel = Mathf.Cos(mouseAngle) * dashSpeed;
                yVel = Mathf.Sin(mouseAngle) * dashSpeed;

            }
            else if (mouseXDistance >= 0 && mouseYDistance <= 0)
            {
                //fourth quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (3f * Mathf.PI / 2f);
                mouseQuadrant = 4;

                xVel = Mathf.Cos(mouseAngle) * dashSpeed;
                yVel = Mathf.Sin(mouseAngle) * dashSpeed;
            }
        }
        
        
        rB.velocity = new Vector3(xVel, yVel, 0f);
    }
    
    //rotate spike toward mouse pointer
    void rotatePeripheral()
    {
        if (peripheralType == 2)
        {
            Vector3 playerDirection = Vector3.right * rightStickX + Vector3.up * rightStickY;

            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
            }
        }
        else
        {
            //get angle that spike is at relative to player
            spikeXDistance = Spike.transform.position.x - transform.position.x;
            spikeYDistance = Spike.transform.position.y - transform.position.y;

            //have logic based on what quadrant its in, to keep angle accurate
            if (spikeXDistance >= 0 && spikeYDistance >= 0)
            {
                //first quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance));
                spikeQuadrant = 1;
            }
            else if (spikeXDistance <= 0 && spikeYDistance >= 0)
            {
                //second quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (Mathf.PI / 2f);
                spikeQuadrant = 2;
            }
            else if (spikeXDistance <= 0 && spikeYDistance <= 0)
            {
                //third quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeYDistance / spikeXDistance)) + (Mathf.PI);
                spikeQuadrant = 3;
            }
            else if (spikeXDistance >= 0 && spikeYDistance <= 0)
            {
                //fourth quadrant
                spikeAngle = Mathf.Atan(Mathf.Abs(spikeXDistance / spikeYDistance)) + (3f * Mathf.PI / 2f);
                spikeQuadrant = 4;
            }


            //if no controller use mouse
            
            if (Input.GetJoystickNames().Length == 0)
            {
                //get real mouse position relative to screen
                realMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //get angle the mouse is at relative to player
                mouseXDistance = realMousePos.x - transform.position.x;
                mouseYDistance = realMousePos.y - transform.position.y;
            }
            else    //use controller
            {
                mouseXDistance = rightStickX;
                mouseYDistance = rightStickY;

                if (rightStickY == 0 && rightStickX == 0)
                {
                    rB.freezeRotation = true;
                }
                else
                    rB.freezeRotation = false;

            }


            ////if no controller use mouse
            //if (Input.GetJoystickNames()[0].Equals(null))
            //{
            //    //get real mouse position relative to screen
            //    realMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    //get angle the mouse is at relative to player
            //    mouseXDistance = realMousePos.x - transform.position.x;
            //    mouseYDistance = realMousePos.y - transform.position.y;
            //}
            //else    //use controller
            //{
            //    if ((Mathf.Abs(rightStickX) > 0.85f) || (Mathf.Abs(rightStickY) > 0.85f))
            //    {
            //        mouseXDistance = rightStickX;
            //        mouseYDistance = rightStickY;
            //    }
            //}

            //have logic based on what quadrant its in, to keep angle accurate
            if (mouseXDistance >= 0 && mouseYDistance >= 0)
            {
                //first quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance));
                mouseQuadrant = 1;

            }
            else if (mouseXDistance <= 0 && mouseYDistance >= 0)
            {
                //second quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (Mathf.PI / 2f);
                mouseQuadrant = 2;
            }
            else if (mouseXDistance <= 0 && mouseYDistance <= 0)
            {
                //third quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseYDistance / mouseXDistance)) + (Mathf.PI);
                mouseQuadrant = 3;
            }
            else if (mouseXDistance >= 0 && mouseYDistance <= 0)
            {
                //fourth quadrant
                mouseAngle = Mathf.Atan(Mathf.Abs(mouseXDistance / mouseYDistance)) + (3f * Mathf.PI / 2f);
                mouseQuadrant = 4;
            }


            //different cases that need to be handled differently
            if (spikeQuadrant == 1 && mouseQuadrant == 4)
            {
                //clockwise

                rB.angularVelocity = -spikeRotateSpeed;
                //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
                //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -spikeRotateSpeed);
            }
            else if (spikeQuadrant == 4 && mouseQuadrant == 1)
            {
                //counter clockwise
                rB.angularVelocity = spikeRotateSpeed;
                //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
                //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), spikeRotateSpeed);
            }
            else if ((spikeQuadrant == 1 && mouseQuadrant == 3) || (spikeQuadrant == 3 && mouseQuadrant == 1))
            {
                if (Mathf.Abs(spikeAngle - mouseAngle) > (2 * Mathf.PI - (spikeAngle) + mouseAngle))
                {
                    //counter clockwise
                    rB.angularVelocity = spikeRotateSpeed;
                    //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
                }
                else
                {
                    rB.angularVelocity = -spikeRotateSpeed;
                    //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
                }
            }
            else if ((spikeQuadrant == 2 && mouseQuadrant == 4) || (spikeQuadrant == 4 && mouseQuadrant == 2))
            {
                if ((spikeAngle - mouseAngle) < (2 * Mathf.PI + (spikeAngle) - mouseAngle))
                {
                    //counter clockwise
                    rB.angularVelocity = spikeRotateSpeed;
                    //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
                }
                else
                {
                    rB.angularVelocity = -spikeRotateSpeed;
                    //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
                }
            }
            //normal case
            else
            {
                if (spikeAngle - mouseAngle < -spikeRotationTolerance)
                {
                    //counter clockwise
                    rB.angularVelocity = spikeRotateSpeed;
                    //rB.AddTorque(spikeRotateSpeed, ForceMode2D.Force);
                    //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), spikeRotateSpeed);
                }
                else if (spikeAngle - mouseAngle > spikeRotationTolerance)
                {
                    //clockwise
                    rB.angularVelocity = -spikeRotateSpeed;
                    //rB.AddTorque(-spikeRotateSpeed, ForceMode2D.Force);
                    //SpikeRotator.transform.Rotate(new Vector3(0f, 0f, 1f), -spikeRotateSpeed);
                }
                else
                {
                    //dead zone, stop rotation
                    rB.angularVelocity *= 0.5f;
                }
            }

            //slow down when it gets close to the tolerance
            if (Mathf.Abs(spikeAngle - mouseAngle) < dampingRange * spikeRotationTolerance)
            {
                rB.angularVelocity /= dampingAmmount;
            }
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{

    //    if (col.gameObject.CompareTag("Enemy Peripheral"))
    //    {
    //        //get hit
    //        hp--;
    //    }

    //}
}
