using UnityEngine;
using System;

public interface IWeapon
{
    int CurrentMag { get; }
    int MagSize { get; }
    event Action<int, int> OnMagChanged;

    void Init(WeaponInventory inventory, Camera playerCam);
    void TryShoot();
}
