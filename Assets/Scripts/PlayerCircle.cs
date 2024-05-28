using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCircle : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private List<Color32> _colors = new List<Color32>();

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _colors = new List<Color32>(GameController.Instance.Colors);
        _spriteRenderer.color = _colors[0];
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                print("s");
                _spriteRenderer.color = _colors.IndexOf(_spriteRenderer.color) == _colors.Count - 1 ? 
                    _colors[0] : _colors[_colors.IndexOf(_spriteRenderer.color) + 1];
            }
        }
    }
}
