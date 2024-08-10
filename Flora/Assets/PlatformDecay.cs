using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDecay : MonoBehaviour
{
    public int platformLifespan;
    public GameObject associatedSeed;
    
    public void DecreaseLifespan()
    {
        platformLifespan -= 1;
        CheckLifespan();
    }

    public void CheckLifespan()
    {
        if(platformLifespan <= 0) 
        {
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            collider.enabled = false;
            Destroy(associatedSeed);
            Destroy(gameObject);
        }
    }
}
