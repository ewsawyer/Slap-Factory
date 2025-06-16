using System;
using System.Collections;
using System.Collections.Generic;
using Qualities;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class RuleManager14 : RuleManager
{

    // References to rules
    [SerializeField] private Image ImgTriangle;
    [SerializeField] private Image ImgCircle;
    [SerializeField] private Image ImgSquare1;
    [SerializeField] private Image ImgSquare2;
    
    private Triangle _triangle;
    private Circle _circle;
    private Square _square1;
    private Square _square2;

    protected override void Start()
    {
        base.Start();
        Restart();
    }

    public void Restart()
    {
        // Randomly select colors for the rules. Each shape will be a different color
        int numColors = Enum.GetValues(typeof(Triangle)).Length - 1;
        int start = Random.Range(0, numColors - 1);
        _triangle = (Triangle)start;
        _circle = (Circle)((start + 1) % numColors);
        _square1 = (Square)((start + 2) % numColors);
        _square2 = (Square)((start + 3) % numColors);
        
        // Set the color of the image to match the rules
        ImgTriangle.color = ColorManager.Instance.GetTriangleColor(_triangle);
        ImgCircle.color = ColorManager.Instance.GetCircleColor(_circle);
        ImgSquare1.color = ColorManager.Instance.GetSquareColor(_square1);
        ImgSquare2.color = ColorManager.Instance.GetSquareColor(_square2);
        
        // Set up the boxes
        BoxQualities[] dqs = Stages[0].Spawner.GetBoxPrefabs();
        dqs[0].Triangle = _triangle;
        dqs[1].Circle = _circle;
        dqs[1].Square = _square1;
        dqs[2].Triangle = _triangle;
        dqs[2].Square = _square2;
        Stages[0].Spawner.SetBoxQualities(dqs);
    }
    
    public override bool IsValid(BoxQualities dq)
    {
        if (StageNum == 0)
            return dq.Triangle != _triangle;

        if (StageNum == 1)
            return dq.Triangle != _triangle && !(dq.Circle == _circle && dq.Square == _square1);
        
        if (StageNum == 2)
            return dq.Square == _square2 || (dq.Triangle != _triangle && !(dq.Circle == _circle && dq.Square == _square1));

        return true;
    }
}
