using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStructure
{
    public string EventId;
    public string Description;
    public string ConditionType;
    public string IsRepeatable;
    public string IsAuto;
    public string[] Conditions;
    public string[] Results;
    public string RepeatResult;
    public string ProgressId;
    public string NextEvent;
}
