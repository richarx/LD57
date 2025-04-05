using InspectorAttribute;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MouseControls : MonoBehaviour
{
    const float ScreenDistanceRatio = 0.5f; // How much of the screen you need to traverse to do the movement.
    const int MaxSpeedStored = 32;

    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Direction4 direction;
    [SerializeField] RailMovement target;

    public static UnityEvent<float> OnComplete = new();

    readonly List<float> storedSpeeds = new();

    float currentMove;
    Vector3 previousMousePos;
    bool over;

    void Awake()
    {
        previousMousePos = Input.mousePosition;
    }

    void Update()
    {
        Vector2 mouseMoveThisFrame = (previousMousePos - Input.mousePosition);

        float mouseMoveInDirThisFrame = direction.GetValueInDirection(mouseMoveThisFrame);

        currentMove = Mathf.Max(0, currentMove + mouseMoveInDirThisFrame);

        previousMousePos = Input.mousePosition;

        float screenSizeInDir = (direction is Direction4.Down or Direction4.Up)
                              ? Screen.height
                              : Screen.width;

        float ratio = currentMove / screenSizeInDir / ScreenDistanceRatio;

        target.MoveAt(ratio);

        float speed = mouseMoveInDirThisFrame / screenSizeInDir / ScreenDistanceRatio / Time.deltaTime;

        if (speed > 0)
            storedSpeeds.Add(speed);

        if (storedSpeeds.Count > MaxSpeedStored)
            storedSpeeds.RemoveAt(0);

        if (!over && ratio >= 1f)
        {
            float normalizedSpeed = Mathf.Clamp01(Mathf.InverseLerp(0, 25, storedSpeeds.Average()));

            OnComplete.Invoke(normalizedSpeed);

            over = true;
        }

        if (over && ratio < 1f)
            over = false;
    }
}
