using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(PlayerScore))]
public class PlayerCircle : MonoBehaviour
{
    [SerializeField] private AudioClip _dropPop;

    private List<Color32> _colors = new List<Color32>();

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private PlayerScore _playerScore;

    private Coroutine _dropHitCor;
    private int _currentColorIndex = 0;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _playerScore = GetComponent<PlayerScore>();
    }

    private void Start()
    {
        _colors = new List<Color32>(GameController.Instance.Colors.Take(GameController.Instance.CurrentDifficulty.NumberOfColors));
        _spriteRenderer.color = _colors[_currentColorIndex];
    }

    private void OnEnable()
    {
        GameController.Instance.OnLose += DisableScript;
    }

    private void OnDisable()
    {
        GameController.Instance.OnLose -= DisableScript;
    }

    private void Update()
    {
        ChangeColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Drop drop = collision.GetComponent<Drop>();

        if (drop != null)
        {
            if (_dropHitCor == null)
                _dropHitCor = StartCoroutine(DropHit());

            _audioSource.PlayOneShot(_dropPop);

            if (drop.SpriteRenderer.color == _spriteRenderer.color)
            {
                _playerScore.AddScore(GameController.Instance.ScoreForMatch);
                // Действия при совпадении цветов
            }
            else
            {
                _playerScore.AddScore(GameController.Instance.ScoreForMismatch);
                GameController.Instance.OnMiss?.Invoke();
                // Действия при несовпадении цветов
            }


            // Действия с каплей
            Vector3 dropPosition = drop.transform.position;
            Vector3 dropHitRotation = Vector3.zero;

            if (dropPosition.x == 0)
            {
                dropHitRotation = new Vector3(-90, 0, 0);
            }
            else
            {
                dropHitRotation = dropPosition.x > 0 ? new Vector3(-45, 90, 0) : new Vector3(-45, -90, 0);
            }

            dropPosition.y -= 0.75f;

            drop.Destroy();
            GameController.Instance.DropsController.DropPool.ReturnToPool(drop);
            GameController.Instance.DropHitController.OnDestroyDrop?.Invoke(dropPosition, dropHitRotation, drop.SpriteRenderer.color);
        }
    }

    private void ChangeColor()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (_currentColorIndex == _colors.Count - 1)
                {
                    _spriteRenderer.color = _colors[0];
                    _currentColorIndex = 0;
                    return;
                }

                _spriteRenderer.color = _colors[++_currentColorIndex];
            }
        }
    }

    private IEnumerator DropHit()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale / 1.5f;

        for (float t = 0; t < 0.1f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, t / 0.1f);
            yield return null;
        }

        transform.localScale = endScale;

        yield return null;

        for (float t = 0; t < 0.1f; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(endScale, startScale, t / 0.1f);
            yield return null;
        }

        transform.localScale = startScale;
        _dropHitCor = null;
    }

    private void DisableScript()
    {
        this.enabled = false;
    }
}
