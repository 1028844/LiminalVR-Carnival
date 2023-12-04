using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] Gun gun;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = gun.score.ToString();
    }
}
