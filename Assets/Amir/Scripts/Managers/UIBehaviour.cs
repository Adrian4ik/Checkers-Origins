using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip _menuChoose;
    [SerializeField] AudioClip _menuTransit;
    [SerializeField] AudioClip _menuBack;
    [SerializeField] Canvas _mainScreen;
    [Space]
	[SerializeField] List<Canvas> _otherScreens;

    private void OnValidate()
    {
        int screen = 1;

        _mainScreen = transform.GetChild(0).GetComponent<Canvas>();
        _otherScreens.Clear();

        while (screen < transform.childCount)
        {
            Canvas canvas;
            
            if (transform.GetChild(screen).TryGetComponent(out canvas))
                _otherScreens.Add(canvas);

            screen++;
        }
    }
}
