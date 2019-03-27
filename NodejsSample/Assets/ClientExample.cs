using UnityEngine;
using WebSocketSharp;
using System;

public class ClientExample : MonoBehaviour {
    
    private WebSocket ws;
    private Vector3 beforePos = Vector3.zero;
    
    void OnGUI() {
        if (GUILayout.Button("Connect")) {
            this.ws = new WebSocket("ws://127.0.0.1:8080");
            this.ws.OnMessage += (object sender, MessageEventArgs e) => {
                print (e.Data);
            };
            this.ws.Connect ();
        }
        
        /*
        if (GUILayout.Button("Send")) {
            this.ws.Send (System.DateTime.Now.ToString ());
        }
        */
        
        if (GUILayout.Button("Close")) {
            this.ws.Close ();
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(ws == null)
        {
            return;
        }
        Vector3 pos = Input.mousePosition;
        pos.z = 10;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        transform.position = worldPos;
        if(worldPos != beforePos)
        {
            beforePos = worldPos;

            PlayerPosition playerPosition = new PlayerPosition();
            playerPosition.playerId = 0;
            playerPosition.position = worldPos;
            playerPosition.date = DateTime.Now;
            this.ws.Send(JsonUtility.ToJson(playerPosition));
        }
    }
}

struct PlayerPosition
{
    public int playerId;
    public Vector3 position;
    public DateTime date;
}