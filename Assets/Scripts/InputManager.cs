using Unity.Mathematics;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [Header(" Actions ")]
    public static Action onCarrotClicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowRayCast();
        }
    }

    private void ThrowRayCast()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit.collider == null) return;

        onCarrotClicked?.Invoke();
    }
}
