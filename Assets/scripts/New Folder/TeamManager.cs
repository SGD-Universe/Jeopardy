using UnityEngine;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour
{
    public GameObject originalObject;
    private List<GameObject> cloneObjects = new List<GameObject>();

    public void AddTeam()
    {
        // Instantiate a clone of the original object
        GameObject clone = Instantiate(originalObject, transform.position, transform.rotation);
        cloneObjects.Add(clone); // Add the clone to the list
    }

    public void RemoveTeam()
    {
        // Check if there are any clones to remove
        if (cloneObjects.Count > 0)
        {
            // Get the index of the last clone in the list
            int lastIndex = cloneObjects.Count - 1;

            // Get a reference to the last clone object
            GameObject lastClone = cloneObjects[lastIndex];

            // Check if the last clone object exists before attempting to destroy it
            if (lastClone != null)
            {
                // Destroy the last clone object
                Destroy(lastClone);
            }

            // Remove the last clone from the list
            cloneObjects.RemoveAt(lastIndex);
        }
    }
}
