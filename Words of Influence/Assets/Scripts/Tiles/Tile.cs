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
    private TileHolder m_occupiedHolder;

    private Unit m_horizontalUnit;
    private int m_horizontalDamage;
    private int m_horizontalHealth;
    private bool m_isFirstHorizontal;

    private Unit m_verticalUnit;
    private int m_verticalDamage;
    private int m_verticalHealth;
    private bool m_isFirstVertical;

    private bool m_isSingleTile;

    private int m_column;
    private int m_row;

    #region Initialization
    public void Setup(TileDatabaseSO.TileData data, PlayerManager owner) {
        m_name = data.m_name;
        m_baseDamage = data.m_attack;
        m_baseHealth = data.m_health;

        m_player = owner;
    }
    #endregion

    #region Getter/Setter
    public TileHolder OccupiedHolder {
        get { return m_occupiedHolder; }
        set { m_occupiedHolder = value; }
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

    #region Update
    public void UpdateUI() {
        
    }
    #endregion

    #region Remove
    public void RemoveTileUnit(bool isHorizontal) {
        if (m_isSingleTile) {
            Debug.Log($"Tile called singleTile remove. There is now {m_player.MyUnits.Count} units left.");
            m_player.MyUnits.Remove(m_horizontalUnit);
        }

        if (isHorizontal) {
            if (m_isFirstHorizontal && !m_isSingleTile) {
                //May cause a bug since list always deletes from from to back and we may have multiple copies of the same Unit (especially single letter)?
                m_isFirstHorizontal = false;
                m_player.MyUnits.Remove(m_horizontalUnit);
            }
            m_horizontalDamage = 0;
            m_horizontalHealth = 0;
            m_horizontalUnit = null;
        }
        else {
            if (m_isFirstVertical && !m_isSingleTile) {
                //May cause a bug since list always deletes from from to back and we may have multiple copies of the same Unit (especially single letter)?
                m_isFirstVertical = false;
                m_player.MyUnits.Remove(m_verticalUnit);
            }
            m_verticalDamage = 0;
            m_verticalHealth = 0;
            m_verticalUnit = null;
        }
    }
    #endregion
}
