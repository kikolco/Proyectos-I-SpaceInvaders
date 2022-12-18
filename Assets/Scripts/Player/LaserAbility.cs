using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAbility : MonoBehaviour
{
    private float laserTime = 4;
    [SerializeField] private PlayerController playerController;

    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        laserTime -= Time.deltaTime;
        if (laserTime <= 0)
        {
            laserTime = 4.0f;
            playerController.laserAbility = false;
            gameObject.SetActive(false);
        }
    }
}
