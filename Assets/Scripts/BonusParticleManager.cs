using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class BonusParticleManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject bonusParticlePrefab;
    private ObjectPool<GameObject> particlePool;

    [Header(" Managers ")]
    [SerializeField] private CarrotManager carrotManager;

    void Awake()
    {
        particlePool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var obj = Instantiate(bonusParticlePrefab, transform);
                return obj;
            },
            actionOnGet: (obj) =>
            {
                obj.SetActive(true);
            },
            actionOnRelease: (obj) =>
            {
                obj.SetActive(false);
            },
            actionOnDestroy: (obj) =>
            {
                Destroy(obj);
            },
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 30
        );
    }

    private void OnEnable()
    {
        InputManager.onCarrotClickedPosition += CarrotClickedCallback;
    }

    private void OnDisable()
    {
        InputManager.onCarrotClickedPosition -= CarrotClickedCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CarrotClickedCallback(Vector2 clickedPos)
    {
        var particle = particlePool.Get();
        particle.transform.position = clickedPos;
        particle.GetComponentInChildren<TMP_Text>().text = $" +{carrotManager.GetCarrotIncrement().ToString()}";
        StartCoroutine(ReturnAfterTime(particle, 0.6f));
    }

    private IEnumerator ReturnAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        particlePool.Release(obj);
    }


}
