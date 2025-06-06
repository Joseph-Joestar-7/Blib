using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = 0.02f;
    private ContactFilter2D movementFilter;
    private Vector2 inputVec;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    [SerializeField] private SpriteRenderer blibSprite;
    [SerializeField] private AnimationCurve jumpCurve;
    bool isJumping = false;
    private Vector3 originalScale;

    private int playerLevel = 0;
    public event EventHandler<OnLevelChangedArgs> OnLevelChanged;
    public class OnLevelChangedArgs:EventArgs
    {
        public int level;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        movementFilter = new ContactFilter2D
        {
            useLayerMask = false,
            useTriggers = false
        };
        originalScale=blibSprite.transform.localScale;

        gameInput.OnJumpAction += GameInput_Jump;
        gameInput.OnInteractAction += GameInput_Interact;
    }

    private void GameInput_Interact(object sender, System.EventArgs e)
    {
        
    }


    private void GameInput_Jump(object sender, System.EventArgs e)
    {
        Jump(0.5f, 0.0f);
    }

    private void Update()
    {
        inputVec = gameInput.GetMovementVectorNormalized();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {

    }

    private void Jump(float jumpHeightScale, float jumpPushScale)
    {
        if (!isJumping)
        {
            StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
        }
    }

    private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
    {
        isJumping = true;

        float jumpStartTime = Time.time;
        float jumpDuration = 0.5f;

        playerLevel += 1;
        OnLevelChanged?.Invoke(this, new OnLevelChangedArgs
        {
            level = playerLevel
        });

        while (isJumping)
        {
            float jumpCompletePercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletePercentage =Mathf.Clamp01(jumpCompletePercentage);

            blibSprite.transform.localScale = originalScale + originalScale * jumpCurve.Evaluate(jumpCompletePercentage) * jumpHeightScale;

            if (jumpCompletePercentage == 1.0f)
                break;

            yield return null;
        }
        if(checkIsOnCollider())
        {

        }
        else
        {
            playerLevel = 0;
            OnLevelChanged?.Invoke(this, new OnLevelChangedArgs
            {
                level = playerLevel
            });

        }
        blibSprite.transform.localScale = originalScale;
        isJumping = false;

    }

    private bool checkIsOnCollider()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.1f);

        return hit.collider.GetComponent<Platform>() != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Platform>()!= null)
        {
            

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Platform>() != null )
        {
            
            if(!checkIsOnCollider() && isJumping == false)
            {
                playerLevel = 0;
                
                OnLevelChanged?.Invoke(this, new OnLevelChangedArgs
                {
                    level = playerLevel
                });
            }
            
        }
    }

    private void HandleMovement()
    {
        if (inputVec == Vector2.zero) return;

        bool success = TryMove(inputVec);

        if (!success)
        {
            success = TryMove(new Vector2(inputVec.x, 0));
            if (!success)
            {
                success = TryMove(new Vector2(0, inputVec.y));
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        float castDistance = moveSpeed * Time.fixedDeltaTime + collisionOffset;
        int hits = rb.Cast(direction, movementFilter, castCollisions, castDistance);
        
        if (hits == 0)
        {
            Vector2 newPos = rb.position + inputVec * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
            return true;
        }
        else
        {
            return false;
        }
    }   
}