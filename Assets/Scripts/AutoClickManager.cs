using Unity.Mathematics;
using UnityEngine;

public class AutoClickManager : MonoBehaviour
{
    [Header(" Element ")]
    [SerializeField] private Transform rotator;
    [SerializeField] private GameObject bunnyPrefab;
    private int currentBunnyIndex;

    [Header(" Settings ")]
    [SerializeField] private float rotatorSpeed;
    [SerializeField] private float radius;

    [Header(" Data ")]
    [SerializeField] private int level;
    [SerializeField] private float carrotPerSecond;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        LoadData();
    }

    void Start()
    {
        carrotPerSecond = level * 0.1f;
        currentBunnyIndex = 0;
        InvokeRepeating("AddCarrots", 1, 1);
        SpawnBunnies();
        StartAnimatingBunnies();
    }

    // Update is called once per frame
    void Update()
    {
        rotator.Rotate(Vector3.forward * Time.deltaTime * rotatorSpeed);
    }

    private void SpawnBunnies()
    {
        while(rotator.childCount > 0)
        {
            Transform bunny = rotator.GetChild(0);
            bunny.SetParent(null);
            Destroy(bunny.gameObject);
        }

        int bunnyCount = Mathf.Min(level, 36);

        for (int i = 0; i < bunnyCount; i++)
        {
            float angle = i * 360 / bunnyCount;

            Vector2 position = new Vector2();
            position.x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            position.y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            GameObject bunnyInstance = Instantiate(bunnyPrefab, position, Quaternion.identity, rotator);
            bunnyInstance.transform.up = position.normalized;
        }
    }

    public void UpgradeAutoClick()
    {
        level++;
        carrotPerSecond = level * 0.1f;
        if(level <= 36) SpawnBunnies(); 
        ResetBunnyAnimation();
        SaveData();
    }

    private void AddCarrots()
    {
        CarrotManager.instance.AddCarrot(carrotPerSecond);
    }

    private void LoadData()
    {
        level = PlayerPrefs.GetInt("AutoClickLevel");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("AutoClickLevel", level);
    }

    private void StartAnimatingBunnies()
    {
        if(rotator.childCount <= 0) return;
        
        for (int i = 0; i < rotator.childCount; i++)
        {
            LeanTween.cancel(rotator.GetChild(i).gameObject);
        }

        LeanTween.moveLocalY(rotator.GetChild(currentBunnyIndex).GetChild(0).gameObject, .8f, 0.1f)
        .setLoopPingPong(1)
        .setOnComplete(AnimateNextBunny);
    }

    private void AnimateNextBunny()
    {
        currentBunnyIndex++;
        if(currentBunnyIndex >= rotator.childCount)
        {
            ResetBunnyAnimation();
        }
        else StartAnimatingBunnies();
    }

    private void ResetBunnyAnimation()
    {
        currentBunnyIndex = 0;
        float delay = Mathf.Max(10 - rotator.childCount, 0);

        LeanTween.delayedCall(delay, StartAnimatingBunnies);
    }
}
