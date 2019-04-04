using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_EnemyShoot : MonoBehaviour {

    public GameObject bullet;

	public void Shoot()
    {
        GameObject curentBul = Instantiate(bullet);
        curentBul.GetComponent<OLD_MoveToTarget>().Shoot(this.transform);
    }

}
