﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    public static UIManager m_singleton;

    [SerializeField]
    private Image m_unitUI;
    [SerializeField]
    private Image m_unitImage;
    [SerializeField]
    private TextMeshProUGUI m_unitHpText;
    [SerializeField]
    private TextMeshProUGUI m_unitDmgText;

    [SerializeField]
    private Image m_tileUI;
    [SerializeField]
    private Image m_tileImage;
    [SerializeField]
    private TextMeshProUGUI m_tileHpText;
    [SerializeField]
    private TextMeshProUGUI m_tileDmgText;

    [SerializeField]
    private TextMeshProUGUI m_expText;
    [SerializeField]
    private Slider m_expBar;

    [SerializeField]
    private TextMeshProUGUI m_versusText;

    [SerializeField]
    private TextMeshProUGUI m_unitsText;

    #endregion

    #region Initialization
    private void Awake() {
        if (m_singleton) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        m_singleton = this;
    }
    #endregion

    #region Update
    #endregion

    #region Player
    public void UpdatePlayerLevel() {
        PlayerManager player = PlayerManager.m_localPlayer;
        int currentExp = player.GetCurrentExp;
        int expThreshold = player.GetExpThreshold[player.GetLevel - 1];
        m_expText.text = $"LV {player.GetLevel}: {currentExp}/{expThreshold}";
        m_expBar.value = (float)currentExp / (float)expThreshold;
    }

    public void Versus(string name) {
        m_versusText.gameObject.SetActive(true);
        m_versusText.text = $"Versus: {name}";
    }

    public void ClearVersus() {
        m_versusText.gameObject.SetActive(false);
    }
    #endregion

    #region Unit
    public void UpdateUnit(Unit unit) {
        m_unitUI.gameObject.SetActive(true);
        m_unitImage.sprite = unit.GetComponent<SpriteRenderer>().sprite;
        m_unitHpText.text = $"{unit.GetCurrentHealth}/{unit.GetMaxHealth}";
        m_unitDmgText.text = unit.GetDamage.ToString();
    }

    public void CloseUnitUI() {
        m_unitUI.gameObject.SetActive(false);
    }
    #endregion

    #region Tiles
    public void UpdateTile(Tile tile) {
        m_tileUI.gameObject.SetActive(true);
        m_tileImage.sprite = tile.GetComponent<SpriteRenderer>().sprite;
        m_tileHpText.text = tile.GetHealth.ToString();
        m_tileDmgText.text = tile.GetDamage.ToString();
    }

    public void CloseTileUI() {
        m_tileUI.gameObject.SetActive(false);
    }

    public void UpdateTiles() {
        m_unitsText.text = $"Tiles: {PlayerManager.m_localPlayer.GetTilesInPlay}/{PlayerManager.m_localPlayer.GetLevel * PlayerManager.TilesPerLevel}";
    }
    #endregion
}
