using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundChoice : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _allThisTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        if (PlayerPrefs.HasKey("Background"))
        {
            foreach (var text in _allThisTexts)
            {
                bool isCurrentBackground = _allThisTexts.IndexOf(text) == PlayerPrefs.GetInt("Background");
                text.gameObject.SetActive(isCurrentBackground);
            }
        }
    }

    public void SelectBackground(int backgroundIndex)
    {
        PlayerPrefs.SetInt("Background", backgroundIndex);

        foreach (var text in _allThisTexts)
        {
            bool isCurrentBackground = _allThisTexts.IndexOf(text) == backgroundIndex;
            text.gameObject.SetActive(isCurrentBackground);
        }
    }
}
