using System.Collections;
using System.Collections.Generic;
using Skills;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillPlayerButtons : MonoBehaviour
{
    [SerializeField] private List<Button> _buttons;
    
    public SkillActivator Activator;
    private void Start()
    {
        _buttons[0].onClick.AddListener(() =>
        {
            Activator.Lying = !Activator.Lying;
            if (Activator.CanLying)
            {
                var imageColor = Activator.Lying
                    ? _buttons[0].image.color = Color.blue
                    : _buttons[0].image.color = Color.yellow;
            }
        });
        _buttons[1].onClick.AddListener(() =>
        {
            Activator.Sitting = !Activator.Sitting;
            if (Activator.CanSitting)
            {
                var imageColor = Activator.Sitting
                    ? _buttons[1].image.color = Color.blue
                    : _buttons[1].image.color = Color.yellow;
            }
        });
        _buttons[2].onClick.AddListener(() =>
        {
            Activator.Sleeping = !Activator.Sleeping;
            if (Activator.CanSleeping)
            {
                var imageColor = Activator.Sleeping
                    ? _buttons[2].image.color = Color.blue
                    : _buttons[2].image.color = Color.yellow;
            }
        });
        _buttons[3].onClick.AddListener(() =>
        {
            Activator.Ready = !Activator.Ready;
            if (Activator.CanReady)
            {
                var imageColor = Activator.Ready
                    ? _buttons[3].image.color = Color.blue
                    : _buttons[3].image.color = Color.yellow;
            }
        });
        _buttons[4].onClick.AddListener(() =>
        {
            Activator.Wave = !Activator.Wave;
            if (Activator.CanWave)
            {
                var imageColor = Activator.Wave
                    ? _buttons[4].image.color = Color.blue
                    : _buttons[4].image.color = Color.yellow;
            }
        });
        _buttons[5].onClick.AddListener(() =>
        {
            Activator.Salsa = !Activator.Salsa;
            if (Activator.CanSalsa)
            {
                var imageColor = Activator.Salsa
                    ? _buttons[5].image.color = Color.blue
                    : _buttons[5].image.color = Color.yellow;
            }
        });
        _buttons[6].onClick.AddListener(() =>
        {
            Activator.HipHop = !Activator.HipHop;
            if (Activator.CanHipHop)
            {
                var imageColor = Activator.HipHop
                    ? _buttons[6].image.color = Color.blue
                    : _buttons[6].image.color = Color.yellow;
            }
        });
        SetTextButton("Lying",_buttons[0]);
        SetTextButton("Sitting",_buttons[1]);
        SetTextButton("Sleep",_buttons[2]);
        SetTextButton("Ready",_buttons[3]);
        SetTextButton("Wave",_buttons[4]);
        SetTextButton("Salsa",_buttons[5]);
        SetTextButton("HipHop",_buttons[6]);

        foreach (var button in _buttons)
        {
            button.image.color = Color.gray;
        }
    }


    public void SetTextButton(string text, Button btn)
    {
        btn.gameObject.GetComponentInChildren<TMP_Text>().text = text;
    }

    public List<Button> GetPlayerButtons()
    {
        return _buttons;
    }
}
