using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickSomethingUp;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    [SerializeField] LayerMask counterLayerMask;


    [Header("Player Physical Properties")]
    private readonly float playerHeight = 2f;
    [SerializeField] private float playerRadius = 0.6f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float interactDistance = 2f;

    [SerializeField] private Transform holdPoint;
    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;
    private Vector3 lastInteractDir;
    private bool isWalking;
    public bool IsWalking { get => isWalking; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }
    
    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            isWalking = false;
            return;
        }

        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector3 moveDir = GameInput.Instance.GetMovementVector().normalized;
        moveDir = new Vector3(moveDir.x, 0f, moveDir.y);
        isWalking = false;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 playerBottom = transform.position + Vector3.up * playerRadius;
        Vector3 playerTop = transform.position + Vector3.up * (playerHeight - playerRadius);

        bool canMove = !Physics.CapsuleCast(playerBottom, playerTop, 
            playerRadius, moveDir, moveDistance, counterLayerMask);

        if (!canMove)
        {
            //try move only in the X direction
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = Mathf.Abs(moveDir.x) > .2f && !Physics.CapsuleCast(playerBottom, playerTop, 
                playerRadius, moveDirX, moveDistance, counterLayerMask);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = Mathf.Abs(moveDir.z) > .2f && !Physics.CapsuleCast(playerBottom, playerTop, 
                    playerRadius, moveDirZ, moveDistance, counterLayerMask);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = canMove && moveDir != Vector3.zero;
        if (moveDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }

    private void HandleInteractions()
    {
        Vector3 moveDir = GameInput.Instance.GetMovementVector().normalized;
        moveDir = new Vector3(moveDir.x, 0f, moveDir.y);

        bool isInteracting = Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance, counterLayerMask);
        if (isInteracting)
        {
            if (hit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter counter)
    {
        selectedCounter = counter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return holdPoint;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnPickSomethingUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
