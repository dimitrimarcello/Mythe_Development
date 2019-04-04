using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TVCamera : MonoBehaviour
{
    [SerializeField] GameObject player, points;
    [SerializeField] float x_distance = 35.5f, y_distance = 20f, CamSpeed = .2f;
    [SerializeField] List<float> dist_tmp;
    [SerializeField] Vector3 targetPos;
    [SerializeField] List<Vector2> savedPoints, v_tmp;
    bool calculated;
    int childCount;

    // Use this for initialization
    void Awake()
    {
        if (points != null)
        {
            childCount = points.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                savedPoints.Add(new Vector2(points.transform.GetChild(i).transform.position.x,
                    points.transform.GetChild(i).transform.position.y));
            }
        }
        else
        {
            Debug.LogWarning("Please set a points gameobject.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTo();
        getPos();
    }

    void moveTo()
    {
        if (new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)) != new Vector2(Mathf.Round(targetPos.x), Mathf.Round(targetPos.y)))
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, CamSpeed);
        }
        else
        {
            calculated = false;
            v_tmp.Clear();
            dist_tmp.Clear();
        }
    }

    void getPos()
    {
        if (player != null)
        {
            if ((player.transform.position.x > this.transform.position.x + (x_distance / 2) ||
                this.transform.position.x - (x_distance / 2) > player.transform.position.x ||
                this.transform.position.y + (y_distance / 2) < player.transform.position.y ||
                this.transform.position.y - (y_distance / 2) > player.transform.position.y) && !calculated)
            {
                calculated = true;
                //Debug.DrawLine(transform.position, player.transform.position, Color.blue);
                StartCoroutine(updateCurrentCam());
            }
        }
        else
        {
            Debug.LogWarning("A player object is required.");
        }
    }

    IEnumerator updateCurrentCam()
    {
        for (int i = 0; i < childCount; i++)
        {
            v_tmp.Add(player.transform.position - new Vector3(savedPoints[i].x, savedPoints[i].y, 0));
            dist_tmp.Add(Mathf.Abs(v_tmp[i].x) + Mathf.Abs(v_tmp[i].y));
            //Debug.DrawLine(transform.position, new Vector3(savedPoints[i].x, savedPoints[i].y), Color.red, 0.5f);
            if (i + 1 == childCount)
            {
                int minDist = dist_tmp.IndexOf(dist_tmp.Min());
                //Debug.DrawLine(transform.position, new Vector3(savedPoints[minDist].x, savedPoints[minDist].y), Color.green, 0.5f);
                targetPos = new Vector3(savedPoints[minDist].x, savedPoints[minDist].y, -10);
                //Debug.Log(targetPos + "Success!");
            }
        }
        yield return null;
    }
}
