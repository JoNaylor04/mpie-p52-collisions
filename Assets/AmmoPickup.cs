using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoPickup : MonoBehaviour
{
    // Ammo variables
    public int ammoCount = 0; // Total ammo collected
    public int maxAmmo = 20; // Maximum ammo player can use

    // Shooting variables
    public float shootRange = 50f; // Maximum range for the raycast

    private void Update()
    {
        // Check if the left mouse button is pressed and the player has ammo left
        if (Input.GetMouseButtonDown(0) && ammoCount > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Decrease ammo count
        ammoCount--;

        // Create a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit result;


        if (Physics.Raycast(ray, out result, shootRange))
        {
            GameObject hitObject = result.collider.gameObject;

    
            if (hitObject.CompareTag("Target")) 
            {
    
                Animation animation = hitObject.transform.parent.GetComponent<Animation>();
                if (animation != null)
                {
                    animation.Play("LowerBridge");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;
        GameObject parent = collidedObject.transform.parent ? collidedObject.transform.parent.gameObject : collidedObject;

        if (parent.name.Contains("AmmoBox"))
        {
            ammoCount = Mathf.Min(ammoCount + 20, maxAmmo); // Add ammo up to the max limit
            parent.SetActive(false);
        }
    }
}
