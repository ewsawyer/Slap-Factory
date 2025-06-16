using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BoxQualities : MonoBehaviour
{
    [Header("Appearance")]
    [FormerlySerializedAs("BaseSprite")] [SerializeField] private SpriteRenderer CircleSprite;
    [FormerlySerializedAs("FrostingSprite")] [SerializeField] private SpriteRenderer SquareSprite;
    [FormerlySerializedAs("SprinklesSprite")] [SerializeField] private SpriteRenderer TriangleSprite;
    [SerializeField] private TextMeshProUGUI LeftCharTMP;
    [SerializeField] private TextMeshProUGUI RightCharTMP;
    [SerializeField] private SpriteRenderer BoxSprite;
    
    [Header("Shapes")]
    [FormerlySerializedAs("Base")] [SerializeField] public Qualities.Circle Circle;
    [FormerlySerializedAs("Frosting")] [SerializeField] public Qualities.Square Square;
    [FormerlySerializedAs("Sprinkles")] [SerializeField] public Qualities.Triangle Triangle;

    [Header("Alphanumeric")] 
    [SerializeField] public char LeftChar;
    [SerializeField] public char RightChar;

    [Header("Debug")]
    public bool WasValidWhenPassingPlayer;

    private BoxShape _circleShape;
    private BoxShape _squareShape;
    private BoxShape _triangleShape;
    
    // Start is called before the first frame update
    void Start()
    {
        SetChars(LeftChar, RightChar);
        SetCircle(Circle);
        SetSquare(Square);
        SetTriangle(Triangle);

        _circleShape = CircleSprite.GetComponent<BoxShape>();
        _squareShape = SquareSprite.GetComponent<BoxShape>();
        _triangleShape = TriangleSprite.GetComponent<BoxShape>();
    }
    
    public void OnEndBox()
    {
        RuleManager.Instance.OnEndBox(this);
    }

    private void SetChars(char left, char right)
    {
        // Set left char if it's present
        if (LeftChar != '\0')
        {
            LeftCharTMP.text = left + "";
            CircleSprite.enabled = false;
            TriangleSprite.enabled = false;
        }

        // Set right char if it's present
        if (RightChar != '\0')
        {
            RightCharTMP.text = right + "";
            SquareSprite.enabled = false;
        }
    }

    private void SetCircle(Qualities.Circle b)
    {
        // If Any, just pick a random one
        if (b == Qualities.Circle.Any)
            b = (Qualities.Circle)Random.Range(0, Enum.GetValues(typeof(Qualities.Circle)).Length - 1);
        
        Circle = b;
        CircleSprite.color = ColorManager.Instance.GetCircleColor(b);
    }
    
    private void SetSquare(Qualities.Square f)
    {
        // If Any, just pick a random one
        if (f == Qualities.Square.Any)
            f = (Qualities.Square)Random.Range(0, Enum.GetValues(typeof(Qualities.Square)).Length - 1);
        
        Square = f;
        SquareSprite.color = ColorManager.Instance.GetSquareColor(f);
    }
    
    private void SetTriangle(Qualities.Triangle s)
    {
        // If Any, just pick a random one
        if (s == Qualities.Triangle.Any)
            s = (Qualities.Triangle)Random.Range(0, Enum.GetValues(typeof(Qualities.Triangle)).Length - 1);
        
        Triangle = s;
        TriangleSprite.color = ColorManager.Instance.GetTriangleColor(s);
    }

    public void SetSpritesEnabled(bool on)
    {
        CircleSprite.enabled = on;
        SquareSprite.enabled = on;
        TriangleSprite.enabled = on;
        BoxSprite.enabled = on;
        LeftCharTMP.enabled = on;
        RightCharTMP.enabled = on;
    }

    public void DestroyBox()
    {
        _circleShape.BreakOff();
        _squareShape.BreakOff();
        _triangleShape.BreakOff();
        BoxSprite.GetComponent<Juice>().Play();
        LeftCharTMP.enabled = false;
        RightCharTMP.enabled = false;
        this.enabled = false;
    }
}
