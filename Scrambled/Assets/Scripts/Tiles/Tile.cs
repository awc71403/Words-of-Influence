﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer m_spriteRenderer;
    private string m_name;
    private int m_baseDamage;
    private int m_baseHealth;

    private PlayerManager m_player;

    private TileDatabaseSO.TileData m_data;
    [SerializeField]
    private TileHolder m_occupiedHolder;

    [SerializeField]
    private Unit m_horizontalUnit;
    private int m_horizontalDamage;
    private int m_horizontalHealth;
    [SerializeField]
    private bool m_isFirstHorizontal;

    [SerializeField]
    private Unit m_verticalUnit;
    private int m_verticalDamage;
    private int m_verticalHealth;
    [SerializeField]
    private bool m_isFirstVertical;
    [SerializeField]
    private bool m_isSingleTile;

    private GameObject m_UI;

    private int m_column;
    private int m_row;

    #region Initialization
    public void Setup(TileDatabaseSO.TileData data, PlayerManager owner) {
        m_data = data;

        m_name = data.m_name;
        m_baseDamage = data.m_attack;
        m_baseHealth = data.m_health;

        m_player = owner;

        m_UI = GameManager.m_singleton.CreateTracker();
        m_UI.GetComponent<UITracker>().TrackObject = gameObject;
    }
    #endregion

    #region Getter/Setter
    public PlayerManager GetPlayer {
        get { return m_player; }
    }

    public TileHolder OccupiedHolder {
        get { return m_occupiedHolder; }
        set { m_occupiedHolder = value; }
    }

    public TileDatabaseSO.TileData GetData {
        get { return m_data; }
    }

    public int GetDamage {
        get { return m_baseDamage; }
    }

    public int GetHealth {
        get { return m_baseHealth; }
    }

    public Unit HorizontalUnit {
        get { return m_horizontalUnit; }
        set { m_horizontalUnit = value; }
    }

    public int HorizontalDamage {
        get { return m_horizontalDamage; }
        set { m_horizontalDamage = value; }
    }

    public int HorizontalHealth {
        get { return m_horizontalHealth; }
        set { m_horizontalHealth = value; }
    }

    public bool IsFirstHorizontal {
        get { return m_isFirstHorizontal; }
        set { m_isFirstHorizontal = value; }
    }

    public Unit VerticalUnit {
        get { return m_verticalUnit; }
        set { m_verticalUnit = value; }
    }

    public int VerticalDamage {
        get { return m_verticalDamage; }
        set { m_verticalDamage = value; }
    }

    public int VerticalHealth {
        get { return m_verticalHealth; }
        set { m_verticalHealth = value; }
    }

    public bool IsFirstVertical {
        get { return m_isFirstVertical; }
        set { m_isFirstVertical = value; }
    }

    public bool IsSingleTile {
        get { return m_isSingleTile; }
        set { m_isSingleTile = value; }
    }

    public string GetName {
        get { return m_name; }
    }
    #endregion

    #region Mouse
    private void OnMouseOver() {
        UIManager.m_singleton.UpdateTile(this);

        if (Input.GetKeyDown(KeyCode.E)) {
            if (m_player.GetPhotonView.IsMine) {
                Debug.Log("Sell called");
                UIManager.m_singleton.CloseUnitUI();
                UIManager.m_singleton.CloseTileUI();
                m_player.OnSoldTile(this);
            }
        }
    }

    private void OnMouseExit() {
        UIManager.m_singleton.CloseTileUI();
    }
    #endregion

    #region Remove
    public void RemoveTileUnits() {
        Debug.Log("RemoveTileUnits");
        if (m_isSingleTile) {
            m_player.MyUnits.Remove(m_horizontalUnit);
            Destroy(m_horizontalUnit.gameObject);
            m_horizontalUnit = null;
        }

        if (m_isFirstHorizontal && !m_isSingleTile) {
            m_isFirstHorizontal = false;
            m_player.MyUnits.Remove(m_horizontalUnit);
            Destroy(m_horizontalUnit.gameObject);
        }
        m_horizontalDamage = 0;
        m_horizontalHealth = 0;
        m_horizontalUnit = null;

        if (m_isFirstVertical && !m_isSingleTile) {
            m_isFirstVertical = false;
            m_player.MyUnits.Remove(m_verticalUnit);
            Destroy(m_verticalUnit.gameObject);
        }
        m_verticalDamage = 0;
        m_verticalHealth = 0;
        m_verticalUnit = null;

        m_isSingleTile = false;
    }

    public void RemoveHorizontalUnit() {
        Debug.Log("RemoveHorizontalUnit");
        if (m_isSingleTile) {
            m_player.MyUnits.Remove(m_horizontalUnit);
            Destroy(m_horizontalUnit.gameObject);
        }

        if (m_isFirstHorizontal && !m_isSingleTile) {
            m_isFirstHorizontal = false;
            m_player.MyUnits.Remove(m_horizontalUnit);
            Destroy(m_horizontalUnit.gameObject);
        }
        m_horizontalDamage = 0;
        m_horizontalHealth = 0;
        m_horizontalUnit = null;

        m_isSingleTile = false;
    }

    public void RemoveVerticalUnit() {
        Debug.Log("RemoveVerticalUnit");
        if (m_isSingleTile) {
            m_player.MyUnits.Remove(m_horizontalUnit);
            Destroy(m_horizontalUnit.gameObject);
            m_horizontalUnit = null;
        }

        if (m_isFirstVertical && !m_isSingleTile) {
            m_isFirstVertical = false;
            m_player.MyUnits.Remove(m_verticalUnit);
            Destroy(m_verticalUnit.gameObject);
        }
        m_verticalDamage = 0;
        m_verticalHealth = 0;
        m_verticalUnit = null;

        m_isSingleTile = false;
    }
    #endregion
}
