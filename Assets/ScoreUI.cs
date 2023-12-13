using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = Gun.score.ToString();
    }
}
