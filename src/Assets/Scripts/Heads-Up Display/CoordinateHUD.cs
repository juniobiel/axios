using TMPro;
using UnityEngine;

public class CoordinateHUD : MonoBehaviour
{
    private const string HEIGHT_TEXT_PREFIX = "ALTURA[Y]";

    [SerializeField] private RectTransform ScoreIcon;
    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text HeightText;

    private int ScoreDivider = 100;

    private int IconInitialOffset = 100;
    private int IconOffsetFactor = 25;
    private Vector2 IconOffset = Vector2.zero;

    private Transform Target;

    private void OnEnable()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        ScoreText.text = $"{(int)(Target.position.x / ScoreDivider)}";
        IconOffset.x = -(IconInitialOffset + (IconOffsetFactor * (ScoreText.text.Length - 1)));
        ScoreIcon.anchoredPosition = IconOffset;

        HeightText.text = $"{(int)Target.position.y}:{HEIGHT_TEXT_PREFIX}";
    }
}
