using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{

    public int drawAmount = 3;
    public float limitSizePerPiece = 2;
    public float limitPieces = 4;
    public float offsetMark;
    public Transform parentToRope;
    public Camera convert;
    public GameObject ropeTexture;
    public bool onOf = true;
    public float zAxis;
    private Vector2 mouseStart;
    private List<Rope> drawnObjects = new List<Rope>();
    private bool procces = false;
    public Slider webAmounts;

    private TimeManager timeManager;

    void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Start()
    {
        webAmounts.maxValue = drawAmount * limitPieces;

    }

    void Update()
    {
        int allWebs = 0;
        foreach (Rope rope in drawnObjects)
        {
            allWebs += rope.ropePieces.Count;
        }
        webAmounts.value = webAmounts.maxValue - allWebs;

        if (onOf && !procces)
        {
            procces = true;
            StartDrawing();
           // Time.timeScale = 0.5f;
           // Time.fixedDeltaTime = Time.timeScale * 0.05f;

            //timeManager.SetSlowmotion(true);
        }
        if (Input.GetMouseButton(0) && onOf)
        {
            if (drawnObjects[0].ropePieces.Count < limitPieces)
            {
                WhileDrawing();
                drawnObjects[0].ropePieces[0].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (drawnObjects.Count > 0)
            {
                drawnObjects[0].ActivateMovement();
                StartCoroutine(SetRopeType(drawnObjects[0]));
                BuildBoxColliders();
            }
            procces = false;
            // Time.timeScale = 1f;
            // Time.fixedDeltaTime = Time.unscaledDeltaTime;

            //timeManager.SetSlowmotion(false);
        }
        if (drawnObjects.Count > drawAmount)
        {
            drawnObjects[drawnObjects.Count - 1].DestroyRope();
            drawnObjects.RemoveAt(drawnObjects.Count - 1);
        }
    }

    private void StartDrawing()
    {
        Vector3 mouseSet = Input.mousePosition;
        mouseSet.z = zAxis;
        Vector3 mousePosition = convert.ScreenToWorldPoint(mouseSet);
        mouseStart = mousePosition;
        drawnObjects.Insert(0, new Rope());
        drawnObjects[0].ropePieces.Insert(0, Instantiate(ropeTexture, mousePosition, transform.rotation, parentToRope));
    }

    private void WhileDrawing()
    {
        //This is the obect rotation

        Vector3 mouseSet = Input.mousePosition;
        mouseSet.z = zAxis;
        Vector3 mousePosition = convert.ScreenToWorldPoint(mouseSet);
        Vector2 dot = mousePosition - drawnObjects[0].ropePieces[0].transform.position;
        float angle = -Mathf.Atan2(dot.x, dot.y) * Mathf.Rad2Deg;// Radials to degrees
        drawnObjects[0].ropePieces[0].transform.eulerAngles = new Vector3(0, 0, angle);// Set the angle that was converted to degrees as Z axis
                                                                                       //This is the object scale

        float dist = Vector2.Distance(mousePosition, mouseStart);
        drawnObjects[0].ropePieces[0].GetComponent<SpriteRenderer>().size = new Vector2(drawnObjects[0].ropePieces[0].GetComponent<SpriteRenderer>().size.x, dist);
        //This will spawn a new part of the rope if it's reached its limit

        if (drawnObjects[0].ropePieces[0].GetComponent<SpriteRenderer>().size.y > limitSizePerPiece)
        {
            mouseStart = mousePosition;
            drawnObjects[0].ropePieces[0].GetComponent<SpriteRenderer>().size = new Vector2(drawnObjects[0].ropePieces[0].GetComponent<SpriteRenderer>().size.x, limitSizePerPiece - 0.001f);
            //still need the right pos help pls!
            Vector3 dir = mousePosition - drawnObjects[0].ropePieces[0].transform.position;
            Ray2D cast = new Ray2D(drawnObjects[0].ropePieces[0].transform.position, dir);
            Vector3 spawnPos = cast.GetPoint(drawnObjects[0].ropePieces[0].GetComponent<SpriteRenderer>().size.y - offsetMark);
            spawnPos.z = 0;
            drawnObjects[0].ropePieces.Insert(0, Instantiate(ropeTexture, spawnPos, transform.rotation, parentToRope));
            drawnObjects[0].ropePieces[0].GetComponent<HingeJoint2D>().connectedBody = drawnObjects[0].ropePieces[1].GetComponent<Rigidbody2D>();
        }
    }

    private void BuildBoxColliders()
    {
        foreach (Rope piece in drawnObjects)
        {
            foreach (GameObject smallerPiece in piece.ropePieces)
            {
                if (smallerPiece != null)
                {
                    Vector2 scale = new Vector2(smallerPiece.GetComponent<BoxCollider2D>().size.x, smallerPiece.GetComponent<SpriteRenderer>().size.y - offsetMark);
                    smallerPiece.GetComponent<BoxCollider2D>().size = scale;
                    Vector3 vectorB = new Vector3(0, smallerPiece.GetComponent<SpriteRenderer>().size.y, 0);
                    float dist = Vector2.Distance(smallerPiece.transform.position, smallerPiece.transform.up + vectorB);
                    Vector2 offset = new Vector2(smallerPiece.GetComponent<BoxCollider2D>().offset.x, dist / dist / 2);
                    smallerPiece.GetComponent<BoxCollider2D>().offset = offset;
                }
            }
        }
    }

    private IEnumerator SetRopeType(Rope afectedObj)
    {
        yield return new WaitForSeconds(2);
        afectedObj.GetRopeType();
    }

}
