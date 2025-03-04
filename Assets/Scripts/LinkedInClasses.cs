using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class LinkedInClasses
{
    [JsonProperty("full_name")]
    public string full_name;

    [JsonProperty("title")]
    public string title;

    [JsonProperty("experiences")]
    public List<WorkHistory> workHistory;

    [JsonProperty("education")]
    public List<Education> education;

}

[System.Serializable]
public class WorkHistory
{
    [JsonProperty("company")]
    public string company;

    [JsonProperty("title")]
    public string title;

}

[System.Serializable]
public class Education
{
    [JsonProperty("degree_name")]
    public string degree;

    [JsonProperty("school")]
    public string school;

}