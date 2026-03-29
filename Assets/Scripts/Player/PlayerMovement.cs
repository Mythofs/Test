using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerControl control;
    private Vector2 input;
    private Vector2 buffer;
    private bool isMoving;
    private float speed = 4f;
    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask longGrassLayer;
    private Action<InputAction.CallbackContext> onCancelInput;

    private void Awake()
    {
        control = new PlayerControl();
        animator = GetComponent<Animator>();
        onCancelInput = ctx =>
        {
            input = Vector2.zero;
            buffer = Vector2.zero;
        };
    }
    private void OnEnable()
    {
        control.Enable();
        control.Player.Move.performed += OnMove;
        control.Player.Move.canceled += onCancelInput;
    }
    private void OnDisable()
    {
        control.Player.Move.performed -= OnMove;
        control.Player.Move.canceled -= onCancelInput;
        control.Disable();
    }
    void Update()
    {
        if(!isMoving && input != Vector2.zero)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;
            Vector3 direction = new Vector3(input.x, input.y, 0);
            if(direction != Vector3.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                Vector3 target = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, 0);
                if(isWalkable(target))
                    StartCoroutine(Move(target));
            }
        }
        animator.SetBool("isMoving", isMoving);
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        if(!isMoving)
            input = context.ReadValue<Vector2>();
        else if (context.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 raw = context.ReadValue<Vector2>();
            if (Mathf.Abs(raw.x) > Mathf.Abs(raw.y))
                raw.y = 0;
            else
                raw.x = 0;
            buffer = raw.normalized;
        }
    }
    private IEnumerator Move(Vector3 target)
    {
        isMoving = true;
        while(Vector3.Distance(target, transform.position) > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = new Vector3(SnapX(target.x), SnapY(target.y), 0);
        isMoving = false;
        if(buffer != Vector2.zero)
        {
            input = buffer;
            buffer = Vector2.zero;
        }
        checkEncounters();
        
    }
    private bool isWalkable(Vector3 target)
    {
        if(Physics2D.OverlapCircle(target, 0.2f, solidObjectsLayer) != null)
            return false;
        return true;
    }
    private void checkEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, longGrassLayer) != null)
        {
            if(UnityEngine.Random.Range(1, 101) <= 10)
                Debug.Log("Encountered a wild pokemon");
        }
    }
    float SnapY(float y)
    {
        return Mathf.Round(y - 0.8f) + 0.8f;
    }
    float SnapX(float x)
    {
        return Mathf.Round(x - 0.5f) + 0.5f;
    }
}
