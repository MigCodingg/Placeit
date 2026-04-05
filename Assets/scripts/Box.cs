using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int _maxPushes = 3;

    private int _pushCount = 0;

    public bool CanBePushed()
    {
        return _pushCount < _maxPushes;
    }

    public void RegisterPush()
    {
        _pushCount++;
    }

    public void SetMaxPushes(int value)
    {
    _maxPushes = value;
    _pushCount = 0;
    
    }

}