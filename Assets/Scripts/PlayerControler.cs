using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public GameManager gameManager;

    public AudioManager audioManager;
    public float playerSpeed = 10f;
    private float zOffset;
    public float trackLimit = 1.5f;
    public float jumpForce = 5f;
    public float groundCheckRange = .5f;

    private Animator playerAnimator;
    private Rigidbody rigidbodyRef;

    public bool touchingGround = false;
    public bool isRunning = false;

    Vector2 touchStart;
    Vector2 touchEnd;


    // Start is called before the first frame update
    void Start()
    {
        zOffset = transform.position.z;

        playerAnimator = GetComponent<Animator>();
        rigidbodyRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning == true)
        {
            PlayerMovement();
            GetInputs();
            GetTouchInput();
            CheckIfGrounded();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Finished");
            playerAnimator.Play("Victory");
            audioManager.PlayAudio(3);
            Invoke("OnEndGame", 5f);
            StartRunning(false);
        }
        else if (other.gameObject.CompareTag("Candy"))
        {
            other.gameObject.SetActive(false);
            gameManager.CollectCandy();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            isRunning = false;

            playerAnimator.Play("Death");
            audioManager.PlayAudio(1);

            Invoke("OnGameOver", 3f);
        }
        else if (other.gameObject.CompareTag("Obstacles"))
        {
            playerAnimator.Play("Hit");
            Destroy(other.gameObject);
            gameManager.ReduceCandy();
        }
    }

    private void OnGameOver()
    {
        gameManager.GameOver();
    }

    private void OnEndGame()
    {
        gameManager.EndGame();
    }


    public void StartRunning(bool state)
    {
        if (state == true)
        {
            isRunning = true;
            playerAnimator.Play("Run");
        }
        else
        {
            isRunning = false;
            playerAnimator.Play("Idle");

        }
    }

    private void PlayerMovement()
    {

        Vector3 targetPosition = transform.position;
        targetPosition.x += Time.deltaTime * playerSpeed;
        targetPosition.z = zOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }

    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            zOffset = zOffset + trackLimit;
            audioManager.PlayAudio(4);
            playerAnimator.Play("Jump");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            zOffset = zOffset - trackLimit;
            audioManager.PlayAudio(4);
            playerAnimator.Play("Jump");
           
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlayAudio(4);
            Jump();
        }

        zOffset = Mathf.Clamp(zOffset, -trackLimit, trackLimit);
    }

    private void GetTouchInput()
    {
        if(Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                touchEnd = touch.position;

                float x = touchEnd.x - touchStart.x;
                float y = touchEnd.y - touchStart.y;
                // moving horizontally
                if(Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if(x>0)
                    {
                        // swipe right
                        audioManager.PlayAudio(4);
                        zOffset = zOffset - trackLimit;
                        playerAnimator.Play("Jump");
                    }
                    else
                    {
                        audioManager.PlayAudio(4);
                        zOffset = zOffset + trackLimit;
                        playerAnimator.Play("Jump");
                    }
                }
                else
                {
                    if(y>0)
                    {
                        // jump
                        audioManager.PlayAudio(4);
                        Jump();
                    }
                    else
                    {

                    }
                }

            }
        }
    }


    private void Jump()
    {
        if (touchingGround == true)
        {
            rigidbodyRef.velocity = Vector3.up * jumpForce;
            playerAnimator.Play("JumpHigh");
        }
    }


    private void CheckIfGrounded()
    {
        RaycastHit hit;

        Vector3 rayStartPosition = transform.position;
        rayStartPosition.y = rayStartPosition.y + .1f;

        if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, groundCheckRange))
        {
            if (hit.transform.gameObject.CompareTag("Ground"))
            {
                touchingGround = true;
            }
            else
            {
                touchingGround = false;
            }
        }
        else
        {
            touchingGround = false;
        }

    }


}
