using UnityEngine;

public interface IState<T> where T : MonoBehaviour
{
    public IState<T> DoState(T enemy);
}