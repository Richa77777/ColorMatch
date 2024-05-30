using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RecordLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recordText;
    [SerializeField] private DifficultiesDatabase _difficultiesDatabase;

    private void Awake()
    {
        StartCoroutine(ShowRecords());
    }

    private IEnumerator ShowRecords()
    {
        while (true)
        {
            for (int i = 0; i < _difficultiesDatabase.Difficulties.Count; i++)
            {
                string recordKey = "Record" + _difficultiesDatabase.Difficulties.ElementAt(i).name;

                if (PlayerPrefs.HasKey(recordKey))
                {
                    _recordText.text = $"YOUR RECORD ({_difficultiesDatabase.Difficulties.ElementAt(i).name}): {PlayerPrefs.GetInt(recordKey)}";
                }
                else
                {
                    _recordText.text = $"YOUR RECORD ({_difficultiesDatabase.Difficulties.ElementAt(i).name}): 0";
                }

                yield return new WaitForSeconds(3f);
            }

            yield return null;
        }
    }
}