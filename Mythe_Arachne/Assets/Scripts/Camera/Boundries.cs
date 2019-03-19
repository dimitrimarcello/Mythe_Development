using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundries : MonoBehaviour {

    [SerializeField] float devideTo;

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position, new Vector3(1920 / devideTo, 1080 / devideTo, 0));
	}
}
