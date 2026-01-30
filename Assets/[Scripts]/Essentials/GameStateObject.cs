using UnityEngine;

// Game state class provides
// an action when game state changes
// default state comparison and current state
public abstract class GameStateObject<T> : MonoBehaviour, 
    IEnumGameState<T>, 
    ISubscribeEvents<IChangeState<T>> where T : System.Enum
{
    [SerializeField] private T currentState;
    private System.Action<T> OnChangeStateAction;

    // Changing states
    public virtual void ChangeState(T newState)
    {
        currentState = newState;
        OnChangeStateAction?.Invoke(newState);
    }

    // Default compare state
    public virtual bool CompareState(T checkState) => checkState.Equals(currentState);

    // Events
    public void SubcribeEvents(IChangeState<T> action)
    {
        OnChangeStateAction += action.OnChangeState;
    }

    public void UnsubcribeEvents(IChangeState<T> action)
    {
        OnChangeStateAction -= action.OnChangeState;
    }
}

// Interface for changing state
public interface IChangeState<T> where T : System.Enum
{
    void OnChangeState(T newState);
}