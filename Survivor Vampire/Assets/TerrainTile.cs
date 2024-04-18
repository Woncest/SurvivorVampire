using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePositon;
    [SerializeField] Vector3Int tileStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = tileStartPosition;
        GetComponentInParent<WorldScroll>().Add(gameObject, tilePositon);
    }
}
