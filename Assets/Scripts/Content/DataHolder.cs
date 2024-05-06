using System;
using System.Collections.Generic;

[Serializable]
public class Root
{
    public bool onBoarding;

    public List<Properties> properties = new List<Properties>();
}

[Serializable]
public class Properties
{
    public string ProjectName;
    public int ID;
    public string Description;
    public string EndDate = "";

    public string time = "";
    public string ClientName;
    public string phoneNumber;
    public string ClientDescription;
    public string Status = "Active";
    public List<Service> Services = new List<Service>();
}
[Serializable]
public class Service
{
    public Service() { }
    public Service(Service serv)
    {
        Name = serv.Name;
        Cost = serv.Cost;
    }
    public string Name;
    public float Cost;
    public int ID;
}
