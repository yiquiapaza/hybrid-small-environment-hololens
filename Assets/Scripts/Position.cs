using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Position : MonoBehaviour
{

    private float InitialPosition_x;
    private float InitialPosition_y;
    private Vector3 velocity = Vector3.zero;

    private int flag = 0;

    private Vector3 InitialPosition;
    private Vector3 NextPosition;
    void Awake()
    {
        InitialPosition.x = gameObject.transform.position.x;
        InitialPosition.y = gameObject.transform.position.y;
        InitialPosition.z = gameObject.transform.position.z;
    }

    void Start()
    {
        StartCoroutine(WaitOneSecond());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (flag == 1)
        {
            gameObject.transform.position = Vector3.Lerp(InitialPosition, NextPosition, 1);
            InitialPosition = NextPosition;
            flag = 0;
        }
    }

    public IEnumerator ObjectPositionUpdate()
    {
        using (UnityWebRequest client = UnityWebRequest.Get("http://192.168.0.104:3000/position"))
        {

            yield return client.SendWebRequest();
            if (client.isHttpError)
            {
                Debug.Log(client.error);
            }
            else
            {
                UpdatePositionObjects(Util.GenericPositionJson(client.downloadHandler.text));
                Debug.Log(client.downloadHandler.text);
            }
        }
    }

    public IEnumerator WaitOneSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1F);
            yield return StartCoroutine(ObjectPositionUpdate());
        }
    }


    private void UpdatePositionObjects(GenericPosition UpdatePostion)
    {
        if(UpdatePostion.state == 1)
        {
            NextPosition = new Vector3(UpdatePostion.position_x, InitialPosition.y, UpdatePostion.position_y);
            flag = 1;
        }
    }
}
