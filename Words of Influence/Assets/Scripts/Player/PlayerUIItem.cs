﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;
using System;

public class PlayerUIItem : MonoBehaviourPunCallbacks {

    #region Variables
    private PhotonView m_PV;
    [SerializeField]
    private TextMeshProUGUI m_name;
    [SerializeField]
    private TextMeshProUGUI m_letters;

    private int m_hp;
    private ProgressBar m_hpBar;

    #endregion

    #region Initialization
    private void Awake() {
        m_hpBar = GetComponentInChildren<ProgressBar>();
        m_hpBar.maxValue = PlayerManager.m_startingHP;
        m_hp = (int)m_hpBar.maxValue;
        Debug.Log(m_hp);
    }
    #endregion

    #region Update
    private void Update() {
        if (m_hpBar.invert) {
            if (m_hpBar.currentPercent.AlmostEquals(m_hp, .95f)) {
                m_hpBar.currentPercent = m_hp;
                m_hpBar.isOn = false;
            }
        }
    }
    #endregion

    #region Getter
    public int GetHP {
        get { return m_hp; }
    }

    public string GetName {
        get { return m_name.text; }
    }
    #endregion

    #region UI
    public void SetUp(PlayerManager player) {
        m_name.text = player.GetPhotonView.Owner.NickName;
        m_letters.text = m_name.text.Substring(0, 2);
    }

    public void UpdateHP(int hp) {
        m_hp = hp;
        m_hpBar.isOn = true;
        m_hpBar.invert = true;
        Debug.Log($"Update HP: HP now {m_hp}");
        PlayerUIList.m_singleton.UpdateRanking();
    }
    #endregion


}
