using InspectorAttribute;
using SceneLoading;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MouseControls : MonoBehaviour
{
    const int MaxSpeedStored = 1;

    [ConditionalHide(HideCondition.IsPlaying, HideType.Readonly)]
    [SerializeField] Direction4 direction;
    [Range(0f, 1f)]
    [SerializeField] float screenDistanceRatio = 0.2f;
    [Space]
    [SerializeField] RailMovement target;
    [Space]
    public float noInputDuration;

    public static UnityEvent<float> OnComplete = new();

    readonly List<float> storedSpeeds = new();

    float currentMove;
    Vector3? previousMousePos;

    private float inputLimiter = 0f;

    bool isMobile;

    void Awake()
    {
        isMobile = Application.isMobilePlatform;

        target.MoveAt(0);
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
            previousMousePos = null;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            previousMousePos = null;
    }

    void Update()
    {
        inputLimiter += Time.deltaTime;
        if (inputLimiter < noInputDuration)
        {
            target.MoveAt(0);
            return;
        }

        if (SceneLoaderManager.IsTransitioning)
        {
            target.MoveAt(0);
            return;
        }

        if (isMobile && Input.GetMouseButton(0) == false)
        {
            target.MoveAt(0);
            previousMousePos = null;
            return;
        }

        if (previousMousePos == null)
        {
            target.MoveAt(0);
            currentMove = 0;
            previousMousePos = Input.mousePosition;
            return;
        }

        Vector2 mouseMoveThisFrame = (previousMousePos.Value - Input.mousePosition);

        float mouseMoveInDirThisFrame = direction.GetValueInDirection(mouseMoveThisFrame);

        currentMove = Mathf.Max(0, currentMove + mouseMoveInDirThisFrame);

        previousMousePos = Input.mousePosition;

        //float screenSizeInDir = (direction is Direction4.Down or Direction4.Up)
        //                      ? Screen.height
        //                      : Screen.width;
        float screenSizeInDir = Screen.height;

        float ratio = currentMove / screenSizeInDir / screenDistanceRatio;

        target.MoveAt(ratio);

        float speed = mouseMoveInDirThisFrame / screenSizeInDir / screenDistanceRatio / Time.deltaTime;

        if (speed > 0)
            storedSpeeds.Add(speed);

        if (storedSpeeds.Count > MaxSpeedStored)
            storedSpeeds.RemoveAt(0);

        if (ratio >= 1f)
        {
            float normalizedSpeed = Mathf.Clamp01(Mathf.InverseLerp(0, 25, storedSpeeds.Average()));

            OnComplete.Invoke(normalizedSpeed);

            enabled = false;
        }
    }
}
