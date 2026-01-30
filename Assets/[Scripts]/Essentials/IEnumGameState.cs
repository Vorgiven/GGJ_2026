
public interface IEnumGameState<T> where T : System.Enum
{
    public void ChangeState(T newState);
    public bool CompareState(T checkState);
}
