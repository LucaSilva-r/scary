using UnityEngine;
using UnityEngine.Events;

public class CounterTrigger : MonoBehaviour
{
    public int targetCount = 10;
    public UnityEvent OnCountReached;

    private int _count = 0;

    public int Count => _count;

    public void Increment()
    {
        _count++;
        if (_count >= targetCount)
        {
            OnCountReached?.Invoke();
        }
    }

    public void ResetCounter()
    {
        _count = 0;
    }
}
