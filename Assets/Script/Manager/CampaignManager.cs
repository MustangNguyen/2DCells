using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CampaignManager : Singleton<CampaignManager> {
    

    [SerializeField] private GameObject[] planets;
    [SerializeField] private SelectedPlanet selectedPlanet;
    [SerializeField] private int planetIndex = 1;
    [SerializeField] private float moveCameraDuration = 1f;
    [SerializeField] private float cameraSize = 3f;
    private List<SelectedPlanet> listSelectPlanets;
    private float time = 0f;
    // private float pixels = 100;
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
        //AudioManager.Instance.StartCampaignBackGround();
        listSelectPlanets = new();
        foreach(var planet in planets){
            listSelectPlanets.Add(planet.gameObject?.GetComponent<SelectedPlanet>());
        }
    }

    private int selected_planet = 0;
#region Shader
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
        // temporary
        seed = 1202034323;
    }
    #endregion
    private void Update()
    {
        // if (isOnGui()) return;
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

            time += Time.deltaTime;
            if (!override_time)
            {
                UpdateTime(time);
            }
        }

    }
    // may be useful later
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
    #region Campaign function
    public void BackToMainMenu(){
        SceneLoadManager.Instance.LoadScene(SceneName.MainMenu,true);
    }
    public void OnPlanetSelect(SelectedPlanet selectPlanet){
        for(int i = 0; i< listSelectPlanets.Count();i++){
            if(listSelectPlanets[i] == selectPlanet){
                selectedPlanet = listSelectPlanets[i];
                planetIndex = i;
                StartCoroutine(IEMoveCameraToTarget(listSelectPlanets[i].transform,cameraSize,moveCameraDuration));
            }
        }
    }
    public void OnPlanetSelect(int index){
        selectedPlanet = listSelectPlanets[index];
                planetIndex = index;
                StartCoroutine(IEMoveCameraToTarget(listSelectPlanets[index].transform,cameraSize,moveCameraDuration));
    }
    private IEnumerator IEMoveCameraToTarget(Transform target, float cameraSize,float duration = 2f){
        Camera mainCamera = Camera.main;
        Vector3 lastCameraPosition = mainCamera.transform.position;
        float defaultCameraSize = mainCamera.orthographicSize;
        float elapsedTime  = 0f;
        while(elapsedTime < duration){
            mainCamera.transform.position = Vector2.Lerp(lastCameraPosition,target.position,elapsedTime/duration);
            mainCamera.orthographicSize = Mathf.Lerp(defaultCameraSize,cameraSize,elapsedTime/duration);
            yield return new WaitForSeconds(Time.deltaTime);
            elapsedTime+=Time.deltaTime;
        }
        mainCamera.transform.position = new Vector3(target.position.x,target.position.y,-10f);
        mainCamera.orthographicSize = cameraSize;
    }

    public void OnChangePlanetButtonLeftClick(){
        if(planetIndex-1>0)
            OnPlanetSelect(--planetIndex);
    }
    public void OnChangePlanetButtonRightClick(){
        if(planetIndex+1<listSelectPlanets.Count())
            OnPlanetSelect(++planetIndex);
    }
    #endregion
}

public class CampaignChapter{
    public List<CampaignLevel> listCampaignLevel;
}

[Serializable]
public class PlanetOOP{
    public string planetId;
    public string planetName;
    public int planetOrder;
    public List<NodeOOP> planetNodes = new();
}

