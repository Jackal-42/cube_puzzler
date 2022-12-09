using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelManager levelManager;

    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public int jumpLength = 20;
    public float apexGravityScale = 0.5f;
    public float holdjumpGravityScale = 0.5f;
    public float floatyThreshold = 3f;
    public float fuzzyLanding = 30;
    public float cyoteTime = 30;
    public float maxVelocity = 10;
    public GameObject jumpCheck;

    public float m_Smoothing = .05f;

    [HideInInspector]
    public bool down = false;

    public float speedX = 0;
    private float speedY = 0;
    private int jumpDuration = 0;
    public Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;
    private float jumpCooldown = 0;
    private float fuzzyLandingDuration = 0;
    private float cyoteTimeDuration = 0;
    public float defaultGravityScale;
    private float squash = 1;
    private bool jumpHeld = false;
    private int flipDelay = 0;
    private float defaultScale = 1f;

    private bool leftCache = false;
    private bool rightCache = false;
    private bool upCache = false;
    private bool downCache = false;

    public bool canJump = true;
    public bool canPound = true;


    public Hitbox jumpBox;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = transform.localScale.x;
        jumpBox = jumpCheck.GetComponent<Hitbox>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        defaultGravityScale = m_Rigidbody2D.gravityScale;
    }

    public void CheckDirection()
    {
        if (m_Rigidbody2D.velocity.x < -0.01)
        {
            transform.localScale = new Vector3(-1 * defaultScale, defaultScale, defaultScale);
        }

        if (m_Rigidbody2D.velocity.x > 0.01)
        {
            transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, defaultScale, defaultScale);
    }

    public void MoveLeft(float multiplier)
    {
        if (m_Rigidbody2D.velocity.x < -0.01)
        {
            transform.localScale = new Vector3(-1 * defaultScale, defaultScale, defaultScale);
        }
        speedX += moveSpeed * -1 * multiplier;
    }

    public void MoveRight(float multiplier)
    {
        if (m_Rigidbody2D.velocity.x > 0.01)
        {
            transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        }


        speedX += moveSpeed * multiplier;
    }

    public void JumpHold()
    {
        if (jumpDuration > 0)
        {
            m_Rigidbody2D.gravityScale = holdjumpGravityScale;
            jumpHeld = true;
        }
    }

    public void Jump(float multiplier)
    {
        if ((jumpBox.active || cyoteTimeDuration > 0) && jumpCooldown <= 0)
        {
            cyoteTimeDuration = 0;
            squash = 1.2f;
            speedY += jumpForce * multiplier;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            jumpBox.active = false;
            jumpDuration = jumpLength;
            jumpCooldown = 6;
        }
        else
        {
            fuzzyLandingDuration = fuzzyLanding;
        }
    }

    public float GetVelocityY()
    {
        return m_Rigidbody2D.velocity.y;
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            upCache = true;
        }

        if (Input.GetKey("a"))
        {
            leftCache = true;
        }

        if (Input.GetKey("d"))
        {
            rightCache = true;
        }

        if (Input.GetKeyDown("s"))
        {
            if (jumpBox.canPound)
            {
                downCache = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (upCache)
        {
            if (canJump)
            {
                Jump(1);
            }
        }

        if (leftCache)
        {
            MoveRight(1);
        }

        if (rightCache)
        {
            MoveLeft(1);
        }

        if (downCache)
        {
            if (canPound)
            {
                levelManager.Pound();
            }
        }
        upCache = false;
        leftCache = false;
        rightCache = false;
        downCache = false;

        if (flipDelay > 0)
        {
            flipDelay--;
            if (flipDelay == 0)
            {
                Flip();
            }
        }


        //spriteRenderer.size = new Vector2(1/squash, squash);
        squash = ((squash * 6) + 1) / 7;

        if (jumpCooldown > 0)
        {
            jumpBox.active = false;
        }


        if (fuzzyLandingDuration > 0)
        {
            if (jumpBox.active)
            {
                Jump(1);
            }
            fuzzyLandingDuration--;
        }

        cyoteTimeDuration--;
        if (jumpBox.active)
        {
            if (cyoteTimeDuration < 0)
            {
                squash = 0.8f;
            }
            cyoteTimeDuration = cyoteTime;

            
            /*
            if (jumpBox.storedCol != null)
            {
                PhysicsObject po = jumpBox.storedCol.GetComponent<PhysicsObject>();
                if(po != null)
                {
                    //stick to PhysicsObjects
                    transform.position = new Vector3(transform.position.x + po.displacement.x, transform.position.y + po.displacement.y, transform.position.z);
                }
            }
            */
        }

        //Vector3 targetVelocity = new Vector2(speedX, m_Rigidbody2D.velocity.y);
        Vector3 targetVelocity = new Vector2(speedX, m_Rigidbody2D.velocity.y + speedY);


        if(jumpBox.storedCol != null && targetVelocity.y < 0)
        {
            targetVelocity = new Vector2(targetVelocity.x, 0);
        }


        if (!jumpBox.active)
        {
            targetVelocity = new Vector2(targetVelocity.x * 1.2f, targetVelocity.y);
        }
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_Smoothing);


        if (!jumpHeld)
        {
            if (Mathf.Abs(m_Rigidbody2D.velocity.y) < floatyThreshold)
            {
                m_Rigidbody2D.gravityScale = apexGravityScale;
            }
            else
            {
                m_Rigidbody2D.gravityScale = defaultGravityScale;
            }
        }

        if (jumpBox.storedCol != null)
        {
            m_Rigidbody2D.gravityScale = 0;
        }
        else if(jumpBox.active)
        {
            m_Rigidbody2D.mass = 1000000;
        }
        else
        {
            m_Rigidbody2D.mass = 1;
        }


        if (m_Rigidbody2D.velocity.y > maxVelocity)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, maxVelocity);
        }

        if (m_Rigidbody2D.velocity.y < maxVelocity * -1)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, maxVelocity * -1);
        }

        jumpDuration -= 1;
        jumpHeld = false;

        speedX = 0;
        speedY = 0;
        jumpCooldown -= 1;
    }
}
