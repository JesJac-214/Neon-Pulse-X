using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;



public class EquipmentBase 
{
    public int ammo;
    public virtual void Use(GameObject vehicle)
    {

    }
    public EquipmentBase()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        ammo = 0;
    }
}

public class SpeedBoost : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 3;
    }
    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpeedBoost();
            ammo--;
        }
    }
}
public class CannonBall : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 5;
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnCannonBall();
            ammo--;
        }
    }
}

public class Wall : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 3;
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnWall();
            ammo--;
        }
    }
}



public class IceBeam : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 3;
    }

    public override void Use(GameObject vehicle)
    {
        if(ammo > 0)
        {
            ammo--;
        }
    }
}

public class BeamOfLight : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 3;
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0) 
        {
            ammo--;
        }
    }
}