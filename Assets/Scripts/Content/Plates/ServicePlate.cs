using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServicePlate : MonoBehaviour
{
    public Service properties;
    public GameObject Blocker;
    public InputFieldChanger Name;
    public InputFieldChanger Price;

    public Button DeleteButton;

    public void Init(Service props,EditPlate editPlate)
    {
        if (editPlate == null)
        {
            DeleteButton.gameObject.SetActive(false);
        }
        else
        {
            DeleteButton.onClick.AddListener(() =>
            {
                editPlate.DeleteServicePlate(this);

            });
        }
        Name.ChangeText(props.Name);
        Price.ChangeText(props.Cost.ToString());
    }
    public Service GetData()
    {
        properties.Name = Name.text;
        float.TryParse(Price.text, out var price);
        properties.Cost = price;
        return properties;
    }
}
