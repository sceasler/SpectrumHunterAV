using UnityEngine;

public class ScriptableVariable<T> : BaseScriptableVariable
{
    [SerializeField] private T value;

    public override object ObjectValue
    {
        get { return value; }
    }

    public T Value
    {
        get { return (T)value; }
        set { this.value = value; }
    }

}
