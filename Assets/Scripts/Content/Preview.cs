using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour
{
    public static Preview Instance;

    public Properties properties;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI EndDate;
    public TextMeshProUGUI Description;

    public TextMeshProUGUI ClientName;
    public TextMeshProUGUI Phone;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Time;


    public void Init(Properties props)
    {
        properties = props;
        Name.text = props.ProjectName;
        EndDate.text = props.EndDate;
        Description.text = props.Description;
        ClientName.text = props.ClientName;
        Phone.text = props.phoneNumber;

        Time.text = props.time;
        float cost = 0;
        foreach (var obj in props.Services)
        {
            cost += obj.Cost;
        }
        Cost.text = cost.ToString() + "$";

        ClearServiceList(ServicePlates);

        ServicePlates = FillServicePlatesList(props.Services, ServiceSpawnPlace);
    }

    public void DeleteProject()
    {
        DataProcessor.Instance.DeletePlate(properties);
        Destroy(gameObject);
    }
    [Header("Resources")]
    public ServicePlate ResourcePlate;

    private List<ServicePlate> ServicePlates = new List<ServicePlate>();

    public Transform ServiceSpawnPlace;
    private List<ServicePlate> FillServicePlatesList(List<Service> dataList, Transform spawnPlace)
    {
        List<ServicePlate> list = new List<ServicePlate>();

        foreach (var item in dataList)
        {
            var obj = Instantiate(ResourcePlate, spawnPlace);
            obj.GetComponent<RectTransform>().SetSiblingIndex(0);
            obj.Init(item,null);
            list.Add(obj);

            obj.Blocker.SetActive(true);
        }
        return list;
    }
    private void ClearServiceList(List<ServicePlate> list)
    {
        foreach(var obj in list)
        {
            Destroy(obj.gameObject);
        }
        list.Clear();
    }
    private void Awake()
    {
        Instance = this;
    }
    public EditPlate editPlateWindow;
    public void OpenEditWindow()
    {
        var obj = Instantiate(editPlateWindow, transform);
        obj.Init(properties,this);
    }
}
