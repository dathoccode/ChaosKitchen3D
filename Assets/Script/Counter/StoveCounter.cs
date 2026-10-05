using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State { 
        Idle,
        Frying, 
        Burning,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer = 0f;
    private float burningTimer = 0f;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        switch (state) 
        { 
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax,
                });
                if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    SwitchState(State.Burning);
                    burningTimer = 0;
                    burningRecipeSO = GetBurningRecipeSOFromInput(GetKitchenObject().GetKitchenObjectSO());
                }
                break;
            case State.Burning:
                burningTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = burningTimer / burningRecipeSO.burningTimerMax,
                });
                if (burningTimer >= burningRecipeSO.burningTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    SwitchState(State.Burned);
                    fryingTimer = 0;
                }

                break;
            case State.Burned:
                break;
        }
    }
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object an it can be fried
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player is carrying a kitchen object that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOFromIput(GetKitchenObject().GetKitchenObjectSO());
                    
                    fryingTimer = 0f;
                    SwitchState(State.Frying);
                }
            }
        }
        else
        {
            // There's a kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying a kitchen object
                if (player.GetKitchenObject().TryGetPlate(out Plate plate))
                {
                    if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        SwitchState(State.Idle);
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0
                        });
                        ResetCuttingProgress();
                    }
                }
            }
            else
            {
                // Player is not carrying a kitchen object, pick up the kitchen object from the counter
                SwitchState(State.Idle);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0
                });
                GetKitchenObject().SetKitchenObjectParent(player);
                ResetCuttingProgress();
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOFromIput(input);
        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOFromIput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO cuttingRecipeSO in fryingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOFromInput(KitchenObjectSO input)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == input)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    private void ResetCuttingProgress()
    {
        fryingTimer = 0;
        
    }
    
    private void SwitchState(State newState)
    {
        state = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = this.state
        });
    }
}
