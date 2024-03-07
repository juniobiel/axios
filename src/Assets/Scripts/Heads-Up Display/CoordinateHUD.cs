using TMPro;
using UnityEngine;

public class CoordinateHUD : MonoBehaviour
{
    private const string SCORE_TEXT_PREFIX = "SCORE[X]";
    private const string HEIGHT_TEXT_PREFIX = "ALTURA[Y]";

    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text HeightText;

    private Transform Target;

    private void OnEnable()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        ScoreText.text = $"{(int)Target.position.x}:{SCORE_TEXT_PREFIX}";
        HeightText.text = $"{(int)Target.position.y}:{HEIGHT_TEXT_PREFIX}";
    }
}
