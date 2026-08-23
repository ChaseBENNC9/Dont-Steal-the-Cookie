using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if ES3_TMPRO && ES3_UGUI

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SaveSlotsManager : ES3SlotManager
{


    // If a file doesn't have a timestamp, it will return have this DateTime.
    static DateTime falseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    public int maximumSaveSlots = 3;
    public GameObject emptySlotTemplate;

    // See Unity's docs for more info: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
    protected override void OnEnable()
    {
         Debug.Log(Application.persistentDataPath);
        // Deactivate the slot template so it's not visible.
        slotTemplate.SetActive(false);
        emptySlotTemplate.SetActive(false);

        // Destroy any existing slots and start from fresh if necessary.
        DestroySlots();
        // Create our save slots if any exist.
        InstantiateSlots();
    }

    // Finds the save slot files and instantiates a save slot for each of them.
    protected override void InstantiateSlots()
    {
        // A list used to store our save slots so we can order them.
        List<(string Name, DateTime Timestamp,string characterName, GameSettings.CharacterSelection characterGender)> slots = new List<(string Name, DateTime Timestamp,string characterName, GameSettings.CharacterSelection characterGender)>();

        // If there are no slots to load, do nothing.
        if (!ES3.DirectoryExists(slotDirectory))
            return;


        // Put each of our slots into a List so we can order them.
        foreach (var file in ES3.GetFiles(slotDirectory))
        {
            // Get the slot name, which is the filename without the extension.
            var slotName = Path.GetFileNameWithoutExtension(file);
            // Get the timestamp so that we can display this to the user and use it to order the slots.
            var timestamp = ES3.GetTimestamp(GetSlotPath(slotName)).ToLocalTime();
            var savedPlayerName = ES3.Load<string>("PlayerName",slotDirectory + Path.GetFileName(file),"Sam");
            var savedCharacter = ES3.Load<GameSettings.CharacterSelection>("SelectedCharacter",slotDirectory+Path.GetFileName(file));
            // Add the data to the slot list.
            slots.Add((Name: slotName, Timestamp: timestamp,characterName:savedPlayerName,characterGender: savedCharacter));
        }

        Debug.Log($"I FOUND {slots.Count} slots the maximum amount is currently {maximumSaveSlots} so there is {maximumSaveSlots - slots.Count} extra spaces ");
        // Now order the slots by the timestamp.
        // slots = slots.OrderByDescending(x => x.Timestamp).ToList();

        // Now create the slots.
        foreach (var slot in slots)
            InstantiateSlot(slot.Name, slot.Timestamp,slot.characterName,slot.characterGender);
        
        int remainingSpaces = maximumSaveSlots - slots.Count;
        if (remainingSpaces > 0)
        {
            for(int index = 0 ; index < remainingSpaces; index++)
            {
                Debug.Log($"Slot_{maximumSaveSlots - remainingSpaces + index+1}");
                InstantiateEmptySlot($"Slot_{maximumSaveSlots - remainingSpaces + index+1}");

            }
        }
    }

    // Instantiates a single save slot with a given slot name and timestamp.
    public SaveSlot InstantiateSlot(string slotName, DateTime timestamp,string name, GameSettings.CharacterSelection character)
    {
        // Create an instance of our slot.
        var slot = Instantiate(slotTemplate, slotTemplate.transform.parent);

        // Add it to our slot list.
        slots.Add(slot);

        // Ensure that we make it active as the template will be inactive.
        slot.SetActive(true);

        var es3SelectSlot = slot.GetComponent<SaveSlot>();
        es3SelectSlot.nameLabel.text = slotName.Replace('_', ' ');
        es3SelectSlot.characterName.text = name;
        es3SelectSlot.characterGender.sprite = GameSettings.PlayerCharacterSprite(character);

        // If the file doesn't have a timestamp, don't display the timestamp.
        if (timestamp == falseDateTime)
            es3SelectSlot.timestampLabel.text = "";
        // Otherwise, set the label for the timestamp.
        else
            es3SelectSlot.timestampLabel.text = $"{timestamp.ToString("yyyy-MM-dd")}\n{timestamp.ToString("HH:mm:ss")}";

        return es3SelectSlot;
    }
    public SaveSlot InstantiateEmptySlot(string slotName)
    {
        var slot = Instantiate(emptySlotTemplate, emptySlotTemplate.transform.parent);
        slot.SetActive(true);
        var es3SelectSlot = slot.GetComponent<SaveSlot>();
        es3SelectSlot.nameLabel.text = slotName.Replace('_', ' ');
        es3SelectSlot.timestampLabel.text = "";
        es3SelectSlot.characterName.text = "";
        return es3SelectSlot;


    }

    // Creates a new slot by instantiating it in the UI and creating a save file for it if necessary.
    public override ES3Slot CreateNewSlot(string slotName)
    {
        // Get the current timestamp.
        var creationTimestamp = DateTime.Now;
        // Create the slot in the UI.
        var slot = InstantiateSlot(slotName, creationTimestamp);
        // Move the slot to the top of the list.
        slot.MoveToTop();

        // Automatically create a file for the save slot if the option is enabled.
        if (autoCreateSaveFile)
            ES3.SaveRaw("{}", GetSlotPath(slotName));

        // Select the slot if necessary.
        if (selectSlotAfterCreation)
            slot.SelectSlot();

        // Scroll the scroll view to the top of the list.
        ;

        return slot;
    }

    // Shows the dialog displaying an error to the user.
    public override void ShowErrorDialog(string errorMessage)
    {
        errorDialog.transform.Find("Dialog Box/Message").GetComponent<TMP_Text>().text = errorMessage;
        errorDialog.SetActive(true);
    }


    // Destroys all slots which have been created, but doesn't delete their underlying save files.
    protected override void DestroySlots()
    {
        foreach (var slot in slots)
            Destroy(slot);
        slots.Clear();
    }

    // Gets the relative file path of the slot with the given slot name.
    public override string GetSlotPath(string slotName)
    {
        // We convert any whitespace characters to underscores at this point to make the file more portable.
        return slotDirectory + Regex.Replace(slotName, @"\s+", "_") + slotExtension;
    }

    // Scrolls to the top of the list of slots.

}
#endif