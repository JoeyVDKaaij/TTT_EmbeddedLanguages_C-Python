using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FPSCounter : MonoBehaviour
{
    [Header("FPS Counter Settings")]
    [SerializeField, Tooltip("Set the delay until it can count again.")]
    private float countDelay = 0.0f;

    private float _timer;
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= countDelay)
        {
            _text.SetText((Time.frameCount / Time.time).ToString("0"));
            _text.SetText((1 / Time.deltaTime).ToString("0"));
            
            _timer = 0.0f;
        }
    }
}
