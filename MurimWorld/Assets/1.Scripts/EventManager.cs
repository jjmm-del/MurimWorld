using UnityEngine;
using System;
public static class EventManager
{
    public static Action<BuildingType> OnBuildingClicked;

    public static void TriggerBuildingClicked(BuildingType buildingType)
    {
        OnBuildingClicked?.Invoke(buildingType);
    }
}
