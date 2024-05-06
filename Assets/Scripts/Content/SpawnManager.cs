using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private List<BasePlate> AllPlates = new List<BasePlate>();

    [SerializeField] private BasePlate plate;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform spawnPreviewPoint;
    [SerializeField] private GameObject emptyObject;

    [SerializeField] private GameObject CreateDataWindow;
    [SerializeField] private Transform CreateDataSpawnPlace;

    public void SpawnCreateDataWindow()
    {
        Instantiate(CreateDataWindow, CreateDataSpawnPlace);
    }
    public void Start()
    {
        if (DataProcessor.Instance != null)
            SpawnAllPlates();
    }

    private void Awake()
    {
        Instance = this;
    }
    public void SpawnAllPlates()
    {
        ClearPlates();
        foreach (var item in DataProcessor.Instance.allData.properties)
        {
            if (Filter(item))
            {
                var obj = Instantiate(plate, spawnPoint);
                obj.Init(item, spawnPreviewPoint);
                AllPlates.Add(obj);
                obj.gameObject.GetComponent<RectTransform>().SetSiblingIndex(0);
            }
        }
        if (AllPlates.Count == 0) emptyObject.SetActive(true); else emptyObject.SetActive(false);
    }
    private void ClearPlates()
    {
        foreach (var plate in AllPlates)
            Destroy(plate.gameObject);
        AllPlates.Clear();
    }
    public void ChangeSpawnState(string state)
    {
        SpawnState = state;
        SpawnAllPlates();
    }
    public string SpawnState = "Active";
    private bool Filter(Properties props)
    {
        /*        DateTime.TryParse(props.EndDate, out DateTime date);
                if ((date >= DateTime.Now) != SpawnState)
                    return false;*/

        if (SpawnState != props.Status)
            return false;
        return true;
    }
}
