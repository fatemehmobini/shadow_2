using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Door door;
    private bool keyAriaCollected = false;
    private bool keyShadowCollected = false;

    public void KeyCollected(string playerTag)
    {
        if (playerTag == "Player")
        {
            keyAriaCollected = true;
            Debug.Log("Aria Key Collected!");
        }
        else if (playerTag == "Shadow")
        {
            keyShadowCollected = true;
            Debug.Log("Shadow Key Collected!");
        }
        
        CheckBothKeys();
    }
    private void CheckBothKeys()
    {
        if (keyAriaCollected && keyShadowCollected)
        {   Debug.Log("Both Keys Collected! Door will disappear!");
            if (door != null)
            {   door.OpenDoor();
                Debug.Log("Door Disappeared!");
            }
        }
        else
        {
            Debug.Log("Waiting for both keys... Aria: " + keyAriaCollected + ", Shadow: " + keyShadowCollected);
        }
    }
}