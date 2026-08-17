using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;
using System.Collections;
using System.Threading.Tasks;

[Serializable] public class SequenceEvent
{
    public WalkSequence sequence;
    public UnityEvent endEvent;
}
public class SequenceManager : MonoBehaviour
{

    public static SequenceManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<SequenceEvent> sequences;
    [SerializeField] SequenceEvent current;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy the duplicate
            return;
        }
        Instance = this;
        

    }
     void Start()
    {
;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public WalkSequence TriggerWalkSequence()
    {
        if (sequences.Count > 0)
        {
            var r = sequences[0];
            sequences.Remove(r);
            current = r;
            return r.sequence;
            
        }
        else return null;
    }
    public void TriggerSectionTwo()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().TeleportToLocation(new Vector3(25,player.transform.position.y,21));
    }

    public void TriggerNow()
    {
        GameObject.Find("Mum").GetComponent<MumController>().SetMumState(1);
    }
    public void MakeMumSay(string message)
    {
        print("MESSAgGEGE");

        GameObject.Find("Mum").GetComponent<MumController>().SayMessage(GameSettings.FormatMessage(message));
    }
    public async void TriggerEndSequence()
    {
        print("TRIGGERENDSEQUENCE");
        current.endEvent.Invoke();
        await EndSequence();
        print("AFTER 3");
        GameObject.Find("Mum").GetComponent<MumController>().SpeechBubble.SetActive(false);
        if (current.sequence.autoTrigger)
            {
                GameObject.Find("Mum").GetComponent<MumController>().SetMumState(1);
            }

    }
    private async Awaitable EndSequence()
    {
        print("AWAITABLEWAITFOR3SECONDS");

        await Awaitable.WaitForSecondsAsync(3);
    }

}
