using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCat : MonoBehaviour
{
    [SerializeField]
    GameObject followCat;

    private void Awake()
    {
        followCat = GameObject.Find("CatDemo");
    }

    void Update()
    {
        transform.position = followCat.transform.position + new Vector3 (0, 0, -15);
    }
}
