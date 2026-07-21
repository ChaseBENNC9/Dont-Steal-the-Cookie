using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string roomName;
    public List<Node> roomNodes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        roomName = gameObject.name;
        roomNodes = transform.GetComponentsInChildren<Node>().ToList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    
}
