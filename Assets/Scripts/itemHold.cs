using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform holdPosition; // Position to hold the object
    private GameObject heldItem; // The currently held item

    void Update()
    {
        //CheckItem();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldItem == null)
            {
                // Try to grab the item
                TryToGrabItem();
            }
            else
            {
                // Release the item
                ReleaseItem();
            }
        }

        // If holding an item, update its position
        if (heldItem != null)
        {
            heldItem.transform.position = holdPosition.position;
        }
    }

    void TryToGrabItem()
    {
        // Create a layer mask that includes all layers except "Ignore Raycast"
        int layerMask = ~LayerMask.GetMask("Ignore Raycast");
        float distance = Vector3.Distance(mainCamera.transform.position, holdPosition.transform.position);

        // Raycast to check for items, excluding the "Ignore Raycast" layer
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance, layerMask))
        {
            if (hit.collider.CompareTag("Item"))
            {
                heldItem = hit.collider.gameObject; // Hold the item
                // Optionally disable the collider or physics of the item
                // heldItem.GetComponent<Collider>().enabled = false;
            }
        }
    }

    void ReleaseItem()
    {
        // Release the item
        heldItem = null;
        // Optionally enable the collider or physics of the item
        // heldItem.GetComponent<Collider>().enabled = true;
    }


    void CheckItem(){
        // Create a layer mask that includes all layers except "Ignore Raycast"
        int layerMask = ~LayerMask.GetMask("Ignore Raycast");    
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 100f, layerMask))
        {
            Debug.Log(hit.collider.tag);
        }
    }
}