using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TiposDeAtaque
{
    Melee,
    Embestida
}
public class IAController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Configuración")]
    [Tooltip("Rango de detección del Enemigo")] 
    [SerializeField] private float rangoDeteccion;
    [Tooltip("Rango de Ataque del Enemigo")]
    [SerializeField] private float rangoDeAtaque;
    [Tooltip("Rango de Embestida del Enemigo")]
    [SerializeField] private float rangoDeEmbestida;
    [Tooltip("Velocidad a la que se mueve el Enemigo")] 
    [SerializeField] private float velocidadMovimiento;
    [Tooltip("Velocidad a la que embiste el Enemigo")]
    [SerializeField] private float velocidadEmbestida;
    [Tooltip("La capa en la que el Enemigo buscará")] 
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [Tooltip("Daño que hace el Enemigo")]
    [SerializeField] private float daño;
    [Tooltip("Tiempo que tarda el enemigo en volver a atacar")]
    [SerializeField] private float tiempoEntreAtaques;
    [Tooltip("El tipo de ataque que realiza el Enemigo")]
    [SerializeField] private TiposDeAtaque tipoAtaque;


    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoAtaque;
    [SerializeField] private bool mostrarRangoEmbestida;

    private BoxCollider2D _boxCollider2D;
    private float tiempoParaSiguienteAtaque;

    //PROPIEDADES
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida
                                                            ? rangoDeEmbestida :
                                                              rangoDeAtaque;
    public float RangoDeteccion => rangoDeteccion;

    public float VelocidadMovimiento => velocidadMovimiento;
    public float Daño => daño;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public Transform PersonajeReferencia { get; set; }
    public EnemyMovement EnemigoMovimiento { get; set; }
    public IAEstado EstadoActual { get; set; }


    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        EstadoActual = estadoInicial;
        EnemigoMovimiento = GetComponent<EnemyMovement>();
    }
    private void Update()
    {
        EstadoActual.EjecutarEstado(this);
    }
    public void CambiarEstado(IAEstado nuevoEstado)
    {
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }

    }

    public void AtaqueMelee(float cantidad)
    {
        if(PersonajeReferencia != null)
        {
            AplicarDañoAlPJ(cantidad);
        }
    }

    public void AtaqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    private IEnumerator IEEmbestida(float cantidad)
    {
        Vector3 personajePosicion = PersonajeReferencia.position;
        Vector3 posicionInicial = transform.position;
        Vector3 direccionHaciaPJ = (personajePosicion - posicionInicial).normalized;
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPJ * 1f;
        _boxCollider2D.enabled = false;


        float transicionDeAtaque = 0f;
        while (transicionDeAtaque <= 1f)
        {        

            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + 
                                    transicionDeAtaque) * 4f;
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque,
                                              interpolacion);
            yield return null;
        }

        if(PersonajeReferencia != null)
        {
            AplicarDañoAlPJ(cantidad);
        }

        _boxCollider2D.enabled = true;
    }


    public void AplicarDañoAlPJ(float cantidad)
    {
        float dañoPorRealizar = 0;
        if (Random.value < stats.PorcentajeEsquiva / 100)
        {
            return;
        }

        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar);
    }

    public bool PersonajeEnRangoAtaque(float rango)
    {
        float distanciaHaciaPJ = (PersonajeReferencia.position - 
                                  transform.position).sqrMagnitude;
        if (distanciaHaciaPJ < Mathf.Pow(rango, 2))
        {
            return true;
        }

        return false;
    }

    public bool EsTiempoDeAtacar()
    {
        if(Time.time > tiempoParaSiguienteAtaque)
        {
            return true;
        }



        return false;
    }

    public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
    }

    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

        if (mostrarRangoAtaque)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }

        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }

    








}
