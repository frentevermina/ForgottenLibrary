using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemigoVida : VidaDefault
{
    public static Action<float> EventoEnemigoDerrotado;

    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPosicion;

    [Header("Restos")]
    [SerializeField] private GameObject restos;

    [Header("Quest")]
    [SerializeField] private bool enemigoQuest;

    private EnemigoBarraVida _enemigoBarraVidaCreada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemyMovement _enemigoMovimiento;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;
    private EnemigoLoot _enemigoLoot;

    private void Awake()
    {
        _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
        _enemigoMovimiento = GetComponent<EnemyMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IAController>();
        _enemigoLoot = GetComponent<EnemigoLoot>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }

    private void CrearBarraVida()
    {
        _enemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPosicion);
        ActualizarBarraVida(Salud, saludMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        _enemigoBarraVidaCreada.ModificarSalud(vidaActual, vidaMax);
    }

    protected override void PersonajeDerrotado()
    {
        DesactivarEnemigo();
        EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        if (enemigoQuest)
        {
            QuestManager.Instance.AñadirProgreso(QuestManager.Instance.Quest1.ID, 1);
            QuestManager.Instance.AñadirProgreso(QuestManager.Instance.Quest2.ID, 1);
            QuestManager.Instance.AñadirProgreso(QuestManager.Instance.Quest3.ID, 1);
        }
    }

    private void DesactivarEnemigo()
    {
        Rigidbody2D enemigoStatic = GetComponent<Rigidbody2D>();
        enemigoStatic.bodyType = RigidbodyType2D.Static;
        restos.SetActive(true);
        _enemigoBarraVidaCreada.gameObject.SetActive(false);
        _spriteRenderer.enabled = false;
        _enemigoMovimiento.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = true;
        _enemigoInteraccion.DesactivarSpriteSeleccion();
    }

}
