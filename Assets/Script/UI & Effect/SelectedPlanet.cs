using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectedPlanet : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public PlanetMapManager planetMapManager;
    private void Start() {
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPlanetSelect();
    }
    public void OnPlanetSelect(){
        CampaignManager.Instance.OnPlanetSelect(this);
    }
}
