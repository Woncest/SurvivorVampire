using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBars : MonoBehaviour
{
    [SerializeField] List<GameObject> chargeBars;

    public GameObject ActivateChargeBar(Sprite sprite){
        foreach (var chargeBar in chargeBars){
            if(!chargeBar.activeInHierarchy){
                chargeBar.SetActive(true);
                ChangeSprite(sprite, chargeBar.GetComponent<IconHolder>().sprite);
                return chargeBar;
            }
        }
        return null;
    }

    public void ChangeSprite(Sprite newSprite, SpriteRenderer spriteRenderer)
    {
        // Get the current bounds of the SpriteRenderer
        Bounds currentBounds = spriteRenderer.bounds;

        // Set the new sprite
        spriteRenderer.sprite = newSprite;

        // Get the new bounds of the SpriteRenderer with the new sprite
        Bounds newBounds = spriteRenderer.bounds;

        // Calculate the scaling factor
        Vector3 scale = spriteRenderer.transform.localScale;
        float widthScaleFactor = currentBounds.size.x / newBounds.size.x;
        float heightScaleFactor = currentBounds.size.y / newBounds.size.y;
        float scalingFactor = Mathf.Min(widthScaleFactor, heightScaleFactor);

        // Apply the scaling factor
        spriteRenderer.transform.localScale = scale * scalingFactor;
    }
}
