using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{

    private readonly float plateOffSet = .1f;

    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private GameObject plateVisualPrefab;
    private List<GameObject> plateVisualList;
    

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        GameObject plateVisual = Instantiate(plateVisualPrefab, plateCounter.GetKitchenObjectFollowTransform());
        plateVisual.transform.localPosition += new Vector3(0, plateOffSet * plateVisualList.Count, 0);
        plateVisualList.Add(plateVisual);
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateVisual = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(plateVisual);
        Destroy(plateVisual);
    }
}
