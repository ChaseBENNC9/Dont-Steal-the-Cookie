#if ES3_TMPRO && ES3_UGUI

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A Component added to a save slot to allow it to be selected, deleted, and un-deleted.
/// </summary>
public class SaveSlot : ES3Slot
{
    public TMP_Text characterName;
    public TMP_Text characterGender;

#region Initialisation and Clean-up

    // See Unity's docs for more info: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
    public override void OnEnable()
    {
        // Add the button press listeners.
        selectButton.onClick.AddListener(TrySelectSlot);
        deleteButton.onClick.AddListener(MarkSlotForDeletion);
        undoButton.onClick.AddListener(UnmarkSlotForDeletion);
    }

    // See Unity's docs for more info: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDisable.html
    public override void OnDisable()
    {
        // Remove all button press listeners.
        selectButton.onClick.RemoveAllListeners();
        deleteButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();

        // If this slot is marked for deletion, delete it.
        if (markedForDeletion)
            DeleteSlot();
    }

#endregion

#region Select methods

    // Called when the Select Slot button is pressed.
    protected override void TrySelectSlot()
    {
        // Manage the confirmation dialog if necessary.
        if(mgr.showConfirmationIfExists)
        {
            if (confirmationDialog == null)
                Debug.LogError("The confirmationDialog field of this ES3SelectSlot Component hasn't been set in the inspector.", this);

            // Display a confirmation dialog if we're overwriting a save slot.
            if (ES3.FileExists(GetSlotPath()))
            {
                // Show the dialog.
                confirmationDialog.SetActive(true);
                // Register the event for the confirmation button.
                confirmationDialog.GetComponent<ES3SlotDialog>().confirmButton.onClick.AddListener(OverwriteThenSelectSlot);
                return;
            }
        }

        SelectSlot();
    }

    // Selects a slot and calls post-selection events if applicable.
    public override void SelectSlot()
    {
        // Hide the confirmation dialog if it's open.
        confirmationDialog?.SetActive(false);

        // Set the path used by Auto Save.
        ES3SlotManager.selectedSlotPath = GetSlotPath();

        // When the default path used by Easy Save's methods.
        ES3Settings.defaultSettings.path = ES3SlotManager.selectedSlotPath;

        // If we've specified an event to be called after the user selects a slot, invoke it.
        mgr.onAfterSelectSlot?.Invoke();

        // If we've specified a scene to load after the user selects a slot, load it.
        if (!string.IsNullOrEmpty(mgr.loadSceneAfterSelectSlot))
            SceneManager.LoadScene(mgr.loadSceneAfterSelectSlot);
    }

#endregion

#region Delete methods

    // Marks a slot to be deleted and displays an undo button.
    protected override void MarkSlotForDeletion()
    {
        markedForDeletion = true;
        // Make the Undo button visible and hide the Delete button.
        undoButton.gameObject.SetActive(true);
        deleteButton.gameObject.SetActive(false);
    }

    // Unmarks a slot to be deleted and displays an delete button again.
    protected override void UnmarkSlotForDeletion()
    {
        markedForDeletion = false;
        // Make the Undo button visible and hide the Delete button.
        undoButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(true);
    }

    // Deletes the existing data for a slot and then selects it.
    protected override void OverwriteThenSelectSlot()
    {
        DeleteSlot();
        // Create the new slot.
        var newSlot = mgr.CreateNewSlot(nameLabel.text);
        // Select the new slot.
        newSlot.SelectSlot();
    }

    // Deletes a save slot.
    public override void DeleteSlot()
    {
        // Delete the file linked to this slot from both disk and cache.
        ES3.DeleteFile(GetSlotPath(), new ES3Settings(ES3.Location.Cache));
        ES3.DeleteFile(GetSlotPath(), new ES3Settings(ES3.Location.File));
        // Destroy this slot.
        Destroy(this.gameObject);
    }

#endregion

#region Utility methods

    // Gets the relative file path of the slot with the given slot name.
    public override string GetSlotPath()
    {
        // Get the slot path from the manager.
        return mgr.GetSlotPath(nameLabel.text);
    }

    // Moves this slot to the top of the slots List ScrollView.
    public void MoveToTop()
    {
        transform.SetSiblingIndex(1);
    }

#endregion
}

#endif