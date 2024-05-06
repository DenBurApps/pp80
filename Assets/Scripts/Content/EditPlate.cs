using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPlate : MonoBehaviour
{
    public Properties properties = new Properties();
    [Header("window 1")]
    public InputFieldChanger ProjectName;
    public InputFieldChanger ProjectDescription;
    public Calendar calendar;
    public TextMeshProUGUI EndDateTMP;
    public TextMeshProUGUI TimeTMP;

    [Header("window 2")]
    public InputFieldChanger ClientName;
    public InputFieldChanger ClientPhoneNumber;

    [Header("window 3")]
    public Transform ServiceSpawnPlace;
    public ServicePlate ServicePlate;
    public List<ServicePlate> ServicePlates = new List<ServicePlate>();

    public Button ContinueButton;
    private Preview preview;
    public void Init(Properties props,Preview preview)
    {
        properties = props;
        this.preview = preview;
        TimeTMP.text = props.time;
        ProjectName.ChangeText(props.ProjectName);
        ProjectDescription.ChangeText(props.Description);

        ClientName.ChangeText(props.ClientName);
        ClientPhoneNumber.ChangeText(props.phoneNumber);

        ServicePlates = FillServicePlatesList(props.Services, ServiceSpawnPlace);

        if(properties.EndDate != "")
        {
            DateTime.TryParse(properties.EndDate, out DateTime date);
            EndDateTMP.text = properties.EndDate;

            calendar.Init(GetDay, 1, date);
        }

        ContinueButton.onClick.RemoveAllListeners();
        ContinueButton.onClick.AddListener(() => 
        {
            SavePlateData();
            Destroy(gameObject);
        });
        foreach (var item in marks)
        {
            if (item.status.text == properties.Status)
                item.ReturnStatus();
            else
                item.DeactivateCheck();
        }
    }
    public CheckMark[] marks;
    public void ChangeStatus(CheckMark checkedMark)
    {
        foreach (var mark in marks)
        {
            mark.DeactivateCheck();
        }
        properties.Status = checkedMark.ReturnStatus();
    }
    private void Awake()
    {
        calendar.Init(GetDay, 1);

        ContinueButton.onClick.AddListener(() =>
        {
            CreatePlateData();
            Destroy(gameObject);
        });
    }
    public TimeGetter timeGetter;
    public void GetTime()
    {
        TimeTMP.text = timeGetter.GetTime();
        properties.time = timeGetter.GetTime();
    }
    public void SavePlateData()
    {
        FillPlateData();
        DataProcessor.Instance.EditPlate(properties);
        preview.Init(properties);
    }

    public void CreatePlateData()
    {
        FillPlateData();
        DataProcessor.Instance.AddNewPlate(properties);

    }

    private void FillPlateData()
    {
        properties.ProjectName = ProjectName.text;
        properties.Description = ProjectDescription.text;
        properties.ClientName = ClientName.text;
        properties.phoneNumber = ClientPhoneNumber.text;

        properties.Services = GetInfoFromServicePlates(ServicePlates);
    }
    private List<Service> GetInfoFromServicePlates(List<ServicePlate> list)
    {
        List<Service> dataList = new List<Service>();

        foreach (var item in list)
        {
            dataList.Add(item.GetData());
        }
        return dataList;
    }
    private List<ServicePlate> FillServicePlatesList(List<Service> dataList, Transform spawnPlace)
    {
        List<ServicePlate> list = new List<ServicePlate>();

        foreach (var item in dataList)
        {
            var obj = Instantiate(ServicePlate, spawnPlace);
            obj.GetComponent<RectTransform>().SetSiblingIndex(0);
            obj.Init(item,this);
            list.Add(obj);
        }
        return list;
    }

    private void GetDay(Day day)
    {
        var date = day.DateTime.ToString();

        calendar.choosedDays[0] = day.DateTime;
        properties.EndDate = date.Remove(10);
        EndDateTMP.text = date.Remove(10);
        calendar.SetDayStates();
    }

    public void AddnewServicePlate()
    {
        var obj = Instantiate(ServicePlate, ServiceSpawnPlace);
        ServicePlates.Add(obj);
        obj.GetComponent<RectTransform>().SetSiblingIndex(0);
        obj.Init(new Service(), this);
    }
    public void DeleteServicePlate(ServicePlate plate)
    {
        foreach (var obj in ServicePlates)
        {
            if (obj == plate)
            {
                Destroy(plate.gameObject);
                properties.Services.Remove(obj.properties);
                ServicePlates.Remove(obj);
                return;
            }
        }
    }

    public TextMeshProUGUI CostAmount;
    private void FixedUpdate()
    {
        float cost = 0;

        foreach(var obj in ServicePlates)
        {
            float.TryParse(obj.Price.text, out float c);
            cost += c; 
        }
        CostAmount.text = cost.ToString() + " $";

        CheckW1();
        CheckW3();
    }

    public InputFieldChanger[] Fields;
    public Button ContinueW1;
    private void CheckW1()
    {
        bool canEnableButton = true;
        foreach (var field in Fields)
            if (field.text == "")
                canEnableButton = false;
        if(properties.EndDate == "")
            canEnableButton = false;
        if (properties.time == "")
            canEnableButton = false;

        ContinueW1.interactable = canEnableButton;
    }
    private void CheckW3()
    {
        if (ServicePlates.Count == 0)
        {
            ContinueButton.interactable = false;
            return;
        }
        foreach (var obj in ServicePlates)
        {
            if (obj.Name.text == "")
            {
                ContinueButton.interactable = false;
                return;
            }
            if(obj.Price.text == "")
            {
                ContinueButton.interactable = false;
                return;
            }
        }
        ContinueButton.interactable = true; 
    }
}
