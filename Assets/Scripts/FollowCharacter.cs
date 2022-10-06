using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quick Unity Tutorial by Youtuber Epitome
/// Link: https://www.youtube.com/watch?v=7XVSLpo97k0
/// Demonstrates and explains how to quickly have UI follow a character.
/// </summary>
public class FollowCharacter : MonoBehaviour
{
    [Header("Tweaks")]
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;

    [Header("Logic")]
    [SerializeField] Camera cam;

    void Start()
    {
        cam = Camera.main;
        //cam = Camera.current;
    }

    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);
        if(transform.position != pos)
            transform.position = pos;
    }
}
