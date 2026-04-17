using UnityEngine;

public class UIShopManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform shopPanel;

    [Header(" Settings ")]
    private Vector2 openPos;
    private Vector2 closePos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openPos = Vector2.zero;
        closePos = new Vector2(shopPanel.rect.width, 0);
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        LeanTween.cancel(shopPanel);
        LeanTween.move(shopPanel, openPos, .2f).setEase(LeanTweenType.easeInOutSine);

    }

    public void Close()
    {
        LeanTween.cancel(shopPanel);
        LeanTween.move(shopPanel, closePos, .2f).setEase(LeanTweenType.easeInOutSine);
    }
}
