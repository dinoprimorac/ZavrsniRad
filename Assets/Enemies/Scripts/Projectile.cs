using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 6f;
    [SerializeField] private float ignoreOwnerTime = 0.1f;

    private Vector3 _vel;
    private int _damage;
    private GameObject _owner;
    private float _t0;
    private float _tSpawn;
    private Collider _col;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    public void Launch(Vector3 dir, float speed, int damage, GameObject owner)
    {
        _vel = dir.normalized * speed;
        _damage = damage;
        _owner = owner;
        _t0 = Time.time;
        _tSpawn = Time.time;

        _col = GetComponent<Collider>();
        if (_owner)
        {
            // ignore collisions with owner's colliders
            foreach (var c in _owner.GetComponentsInChildren<Collider>())
                if (c && _col) Physics.IgnoreCollision(_col, c, true);
        }
    }

    private void Update()
    {
        transform.position += _vel * Time.deltaTime;
        if (Time.time - _t0 > lifetime) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ignore owner / immediate spawn overlap
        if (_owner && (other.transform.root.gameObject == _owner || Time.time - _tSpawn < ignoreOwnerTime))
            return;

        if (other.TryGetComponent<IDamageable>(out var d))
            d.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
