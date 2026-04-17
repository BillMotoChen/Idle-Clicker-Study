using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Carrot : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform carrotRendereTransform;
    [SerializeField] private Image filledImage;

    [Header(" Setting ")]
    [SerializeField] private float fillRate;
    private bool isFrenzyMode;

    [Header(" Actions ")]
    public static Action onFrenzyModeStarted;
    public static Action onFrenzyModeStopped;

    void OnEnable()
    {
        InputManager.onCarrotClicked += CarroctClickedCallback;
    }

    void OnDisable()
    {
        InputManager.onCarrotClicked -= CarroctClickedCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CarroctClickedCallback()
    {
        Animate();
        if (!isFrenzyMode) Fill();
    }

    private void Animate()
    {
        carrotRendereTransform.localScale = Vector3.one * .8f;
        LeanTween.cancel(carrotRendereTransform.gameObject);
        LeanTween.scale(carrotRendereTransform.gameObject, Vector3.one * .7f, .15f).setLoopPingPong(1);
    }

    private void Fill()
    {

        filledImage.fillAmount += fillRate;
        if (filledImage.fillAmount >= 1)
        {
            StartFrenzyMode();
        }
    }

    private void StartFrenzyMode()
    {
        onFrenzyModeStarted?.Invoke();
        isFrenzyMode = true;
        LeanTween.value(1, 0, 5).setOnUpdate((value => filledImage.fillAmount = value))
        .setOnComplete(StopFrenzyMode);
    }

    private void StopFrenzyMode()
    {
        onFrenzyModeStopped?.Invoke();
        isFrenzyMode = false;
    }
}
