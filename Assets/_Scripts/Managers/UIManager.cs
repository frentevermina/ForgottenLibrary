using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Sigleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelDescripcion;
    //
    [SerializeField] private GameObject panelMapa;
    //
    [SerializeField] private GameObject panelDoctorQuest;
    [SerializeField] private GameObject panelPersonajeQuest;
    [SerializeField] private GameObject panelContenedor;


    [Header("SubPaneles")]

    [SerializeField] private GameObject questEtiquetaInventarioDeseleccionado;
    [SerializeField] private GameObject questEtiquetaInventarioSeleccionado;
    [SerializeField] private GameObject questEtiquetaPersonajeQuestDeseleccionado;
    [SerializeField] private GameObject questEtiquetaPersonajeQuestSeleccionado;

    [SerializeField] private GameObject inventarioEtiquetaInventarioDeseleccionado;
    [SerializeField] private GameObject inventarioEtiquetaInventarioSeleccionado;
    [SerializeField] private GameObject inventarioEtiquetaPersonajeQuestDeseleccionado;
    [SerializeField] private GameObject inventarioEtiquetaPersonajeQuestSeleccionado;

    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image temperaturaPlayer;
   // [SerializeField] private Image municionPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI temperaturaTMP;
    [SerializeField] private TextMeshProUGUI municionTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasFavorTMP;
    

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDañoTMP;
    [SerializeField] private TextMeshProUGUI statDefResTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statEsoterismoTMP;
    [SerializeField] private TextMeshProUGUI statOcultismoTMP;
    [SerializeField] private TextMeshProUGUI statMedicinaTMP;
    [SerializeField] private TextMeshProUGUI statBuscarTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpRequeridaTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoResistenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoPercepcionTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoSuerteTMP;
    [SerializeField] private TextMeshProUGUI atributosDisponibles;

    [Header("Scipts Asociados")]
    [SerializeField] private PlayerMovement playerMovement;

    private float vidaActual;
    private float vidaMax;
    private float temperaturaActual;
    private float temperaturaMax;
    private float expActual;
    private float expRequeridaNuevoNivel;

    private int municionActual;
    private int municionMax;

    public bool InventarioAbierto = false;
    public bool DiarioAbierto = false;

    void Update()
    {
        ActualizarUipersonaje();
        ActualizarPanelStats();
      /*  if (Input.GetButtonDown("Inventory") && InventarioAbierto == true)
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            CerrarInventario();
            levelManager.UnPauseNormal();
                return;
        }   

        if (Input.GetButtonDown("Inventory") && InventarioAbierto == false)
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            AbrirInventario();
            levelManager.PauseNormal();
                return;
        }*/

            // uIManager.AbrirCerrarPanelInventario();                                         
        
    }
    private void ActualizarUipersonaje()
    {
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount,
            vidaActual / vidaMax, 10 * Time.deltaTime);
        temperaturaPlayer.fillAmount = Mathf.Lerp(temperaturaPlayer.fillAmount,
            temperaturaActual / temperaturaMax, 10 * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount,
            expActual / expRequeridaNuevoNivel, 10 * Time.deltaTime);
       /* municionPlayer.fillAmount = Mathf.Lerp(municionPlayer.fillAmount,
            municionActual / municionMax, 10f * Time.deltaTime);*/

        // vidaTMP.text = $"{vidaActual}/{vidaMax}";
        // temperaturaTMP.text = $"{temperaturaActual}/{temperaturaMax}";
        expTMP.text = $"{((expActual / expRequeridaNuevoNivel) * 100):F0}%";
        nivelTMP.text = $"Nivel {stats.Nivel}";
        monedasFavorTMP.text = MonedasFavorManager.Instance.MonedasTotales.ToString();
        municionTMP.text = $"{municionActual}";
    }

    private void ActualizarPanelStats()
    {
        if (panelStats.activeSelf == false)
        {
            return;
        }

        statDañoTMP.text = stats.Daño.ToString();
        statDefResTMP.text = stats.Defensa.ToString();
        statCriticoTMP.text = stats.PorcentajeCritico.ToString();
        statBloqueoTMP.text = stats.PorcentajeEsquiva.ToString();
        statEsoterismoTMP.text = stats.Esoterismo.ToString();
        statOcultismoTMP.text = stats.Ocultismo.ToString();
        statMedicinaTMP.text = stats.Medicina.ToString();
        statBuscarTMP.text = stats.Buscar.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();



        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoResistenciaTMP.text = stats.Resistencia.ToString();
        atributoPercepcionTMP.text = stats.Percepcion.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoSuerteTMP.text = stats.Suerte.ToString();
        atributosDisponibles.text = stats.PuntosDisponibles.ToString();
    }
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }

    public void ActualizarTemperaturaPersonaje(float pTemperaturaActual, float pTemperaturaMax)
    {
        temperaturaActual = pTemperaturaActual;
        temperaturaMax = pTemperaturaMax;
    }

    public void ActualizarMunicionPersonaje(int pMunicionActual, int pMunicionMax)
    {
        municionActual = pMunicionActual;
        municionMax = pMunicionMax;
    }

    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        panelStats.SetActive(!panelStats.activeSelf);
        

    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    //**************
    public void CerrarPanelDescripcion()
    {
        panelDescripcion.SetActive(!panelDescripcion.activeSelf);
    }
    ///****************

    


    public void AbrirInventario()
    {
        CerrarDiario();

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.PauseNormal();
        panelContenedor.SetActive(false);


        inventarioEtiquetaInventarioDeseleccionado.SetActive(false);
        inventarioEtiquetaInventarioSeleccionado.SetActive(true);
        panelInventario.SetActive(true);
        panelDescripcion.SetActive(false);
        DiarioAbierto = false;
        
        InventarioAbierto = true;
    }

    public void CerrarInventario()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.UnPauseNormal();
        panelContenedor.SetActive(true);


        inventarioEtiquetaInventarioDeseleccionado.SetActive(true);
        inventarioEtiquetaInventarioSeleccionado.SetActive(false);
        panelInventario.SetActive(false);
        panelDescripcion.SetActive(false);

        
        InventarioAbierto = false;
    }

    public void AbrirDiario()
    {
        CerrarInventario();

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.PauseNormal();
        panelContenedor.SetActive(false);


        InventarioAbierto = false;
        panelPersonajeQuest.SetActive(true);

        questEtiquetaPersonajeQuestDeseleccionado.SetActive(false);
        questEtiquetaPersonajeQuestSeleccionado.SetActive(true);
        DiarioAbierto = true;
    }

    public void CerrarDiario()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.UnPauseNormal();
        panelContenedor.SetActive(true);


        panelPersonajeQuest.SetActive(false);

        questEtiquetaPersonajeQuestDeseleccionado.SetActive(true);
        questEtiquetaPersonajeQuestSeleccionado.SetActive(false);
        DiarioAbierto = false;
    }

    public void AbrirCerrarPanelInventario()
    {
       

        panelInventario.SetActive(!panelInventario.activeSelf);
        panelDescripcion.SetActive(false);
        panelPersonajeQuest.SetActive(false);

        inventarioEtiquetaInventarioDeseleccionado.SetActive(false);
        inventarioEtiquetaInventarioSeleccionado.SetActive(true);
        inventarioEtiquetaPersonajeQuestDeseleccionado.SetActive(true);
        inventarioEtiquetaPersonajeQuestSeleccionado.SetActive(false);

        
   


    }

    public void AbrirCerrarPanelPersonajeQuest()
    {
        

        panelPersonajeQuest.SetActive(!panelPersonajeQuest.activeSelf);
        panelDescripcion.SetActive(false);
        panelInventario.SetActive(false);
        questEtiquetaPersonajeQuestDeseleccionado.SetActive(false);
        questEtiquetaPersonajeQuestSeleccionado.SetActive(true);
        questEtiquetaInventarioDeseleccionado.SetActive(true);
        questEtiquetaInventarioSeleccionado.SetActive(false);

        



    }

    public void OpenCloseDocQuests()
    {
        panelDoctorQuest.SetActive(!panelDoctorQuest.activeSelf);
    }

    public void OpenPanelInteraction(InteractionExtraNPC typeInteraction)
    {
        switch (typeInteraction)
        {
            case InteractionExtraNPC.Quests:
                OpenCloseDocQuests();
                break;
            case InteractionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
            case InteractionExtraNPC.Crafting:
                break;
        }
    }

    #endregion




}
