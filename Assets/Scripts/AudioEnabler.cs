using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioEnabler : MonoBehaviour
{
    [SerializeField] private Image _onButton;
    [SerializeField] private Image _offButton;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _parameterName = "MusicVolume";

    private Color32 _selectedColor = new Color32(78, 171, 87, 255); 
    private Color32 _notSelectedColor = new Color32(171, 78, 87, 255);

    private void Awake()
    {
        _mixer.GetFloat(_parameterName, out float volume);

        if (volume == -80)
        {
            DisableAudio();
        }
        else
        {
            EnableAudio();
        }
    }

    public void EnableAudio()
    {
        _onButton.color = _selectedColor;
        _offButton.color = _notSelectedColor;

        _mixer.SetFloat(_parameterName, 0);
    }

    public void DisableAudio()
    {
        _offButton.color = _selectedColor;
        _onButton.color = _notSelectedColor;

        _mixer.SetFloat(_parameterName, -80);
    }
}
