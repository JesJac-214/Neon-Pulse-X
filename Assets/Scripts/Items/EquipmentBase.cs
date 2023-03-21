using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Rendering;

public class EquipmentBase 
{
    public int ammo;
    public String weaponName;
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
        weaponName = "base";
    }
}

public class SpeedBoost : EquipmentBase
{
    public override void Initialize()
    {
        weaponName = "SpeedBoost";
        ammo = 1;
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
        ammo = 3;
        weaponName = "CannonBall"; 
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
        weaponName = "Wall";
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
        ammo = 2;
        weaponName = "IceBeam";
    }

    public override void Use(GameObject vehicle)
    {
        if(ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnIcebeamBullet();
            ammo--;
        }
    }
}

public class Mine : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 3;
        weaponName = "Mine";
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0) 
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnMine();
            ammo--;
        }
    }
}
public class EMP : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 1;
        weaponName = "EMP";
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnEMP();
            ammo--;
        }
    }
}

public class HackingDevice : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 1;
        weaponName = "HackingDevice";
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnHackingDevice();
            ammo--;
        }
    }
}

public class Shield : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 1;
        weaponName = "Shield";
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnShield();
            ammo--;
        }
    }
}

public class SoundWave : EquipmentBase
{
    public override void Initialize()
    {
        ammo = 3;
        weaponName = "SoundWave";
    }

    public override void Use(GameObject vehicle)
    {
        if (ammo > 0)
        {
            vehicle.GetComponent<VehicleWeaponItemLogic>().SpawnSoundWave();
            ammo--;
        }
    }
}