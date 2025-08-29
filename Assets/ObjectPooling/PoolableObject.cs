using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public ObjectPool parent;

    public virtual void OnDisable()
    {
        parent.ReturnToPool(this);
    }
}
