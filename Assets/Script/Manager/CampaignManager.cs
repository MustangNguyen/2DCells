using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;


public class CampaignManager : MonoBehaviour {
    

    [SerializeField] private GameObject[] planets;
    
    private float time = 0f;
    private float pixels = 100;
    private int seed = 0;
    private bool override_time = false;
    private List<Color> colors = new List<Color>();
    private List<GameObject> colorBtns = new List<GameObject>();
    private int selectedColorButtonID = 0;
    private GameObject selectedColorButton;
    private void Start()
    {
        OnChangeSeedRandom();
        GetColors();
        // MakeColorButtons();
    }

    private int selected_planet = 0;

    public void OnClickChooseColor()
    {
        for(int i = 0;i<planets.Count();i++){
            selectedColorButton = planets[i];
            selectedColorButtonID = selectedColorButton.GetComponent<ColorChooserButton>().ButtonID;
            ColorPicker.Create(colors[selectedColorButtonID], "Choose color", onColorChanged, onColorSelected, false);
        }
        /////////////////
        // selectedColorButton = EventSystem.current.currentSelectedGameObject;
        // selectedColorButtonID = EventSystem.current.currentSelectedGameObject.GetComponent<ColorChooserButton>().ButtonID;
        // ColorPicker.Create(colors[selectedColorButtonID], "Choose color", onColorChanged, onColorSelected, false);
    }

    private void onColorChanged(Color currentColor)
    {
        colors[selectedColorButtonID] = currentColor;
        SetColor();
    }

    private void onColorSelected(Color finishedColor)
    {
        colors[selectedColorButtonID] = finishedColor;
        SetColor();
    }

    private void GetColors()
    {
        foreach (var btn in colorBtns)
        {
            DestroyImmediate(btn);
        }
        
        colors.Clear();
        colorBtns.Clear();
        colors = planets[selected_planet].GetComponent<IPlanet>().GetColors().ToList();
    }

    private void SetColor()
    {
        //Debug.Log(selected_planet + ":"+planets[selected_planet]);
        selectedColorButton.GetComponent<Image>().color = colors[selectedColorButtonID];
        planets[selected_planet].GetComponent<IPlanet>().SetColors(colors.ToArray());
    }

    public void OnLightPositionChanged(Vector2 pos)
    {
        planets[selected_planet].GetComponent<IPlanet>().SetLight(pos);
    }

    private void UpdateTime(float time)
    {
        planets[selected_planet].GetComponent<IPlanet>().UpdateTime(time);
    }

    public void OnChangeSeedRandom()
    {
        seedRandom();
        planets[selected_planet].GetComponent<IPlanet>().SetSeed(seed);
    }
    private void seedRandom()
    {
        UnityEngine.Random.InitState( System.DateTime.Now.Millisecond );
        seed = UnityEngine.Random.Range(0, int.MaxValue);


    }
    private void Update()
    {
        if (isOnGui()) return;
        selected_planet = 0;
        for (; selected_planet < planets.Count(); ++selected_planet)
        {
            // if (Input.GetMouseButton(0))
            // {
            //     // var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Camera.main.ScreenToViewportPoint(planets[selected_planet].transform.position) + new Vector3(0.5f,0.5f,0f);
            //     var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - Camera.main.ScreenToViewportPoint(planets[selected_planet].transform.position);
            //     OnLightPositionChanged(pos);
            // }
            var pos = planets[0].transform.position - planets[selected_planet].transform.position;
            pos = pos.normalized*0.5f + new Vector3(0.5f,0.5f,0f);
            OnLightPositionChanged(pos);
            Debug.Log(pos);

            time += Time.deltaTime;
            if (!override_time)
            {
                UpdateTime(time);
            }
        }

    }

    private bool isOnGui()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        if (result.Count(x => x.gameObject.GetComponent<Selectable>()) > 0) {
            return true;
        }

        return false;
    }

}
