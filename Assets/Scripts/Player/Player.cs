using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    private void Update()
    {
        inputVec = gameInput.GetMovementVectorNormalized();
    }

    private void FixedUpdate()
    {
        HandleMovement();
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