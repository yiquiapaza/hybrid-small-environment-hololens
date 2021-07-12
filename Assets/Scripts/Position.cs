using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Position : MonoBehaviour
{

    private float InitialPosition_x;
    private float InitialPosition_y;
    private float InitialPosition_z;

    void Awake()
    {
        InitialPosition_x = gameObject.transform.position.x;
        InitialPosition_y = gameObject.transform.position.y;
        InitialPosition_z = gameObject.transform.position.z;
    }

    void Start()
    {
        StartCoroutine(WaitOneSecond());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ObjectPositionUpdate()
    {
        using (UnityWebRequest client = UnityWebRequest.Get("http://192.168.0.104:3000/"))
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
            yield return new WaitForSeconds(0.5F);
            yield return StartCoroutine(ObjectPositionUpdate());
        }
    }


    private void UpdatePositionObjects(GenericPosition UpdatePostion)
    {
        gameObject.transform.position = new Vector3(
            InitialPosition_x + UpdatePostion.position_x,
            InitialPosition_y,
            InitialPosition_z + UpdatePostion.position_y);
    }
}
