using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

    public GameObject bullet;

	public void Shoot()
    {
        GameObject curentBul = Instantiate(bullet);
        curentBul.GetComponent<MoveToTarget>().Shoot(this.transform);
    }

}
