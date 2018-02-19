using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public Sprite[] Hearts;
    public Image HeartUI;
    [SerializeField] private PlayerMovement player;

    private void Start()
    {
    }

    void Update()
    {
        HeartUI.sprite = Hearts[player.Health];
    }
}
