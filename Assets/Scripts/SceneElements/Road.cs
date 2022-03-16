
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private RoadElementPool pool;

    private int _currentRoadWidth;
    
    private List<RoadElement> _roadElements = new List<RoadElement>();
    
    public void Initialize(int offset)
    {
        _currentRoadWidth = Mathf.FloorToInt(transform.localScale.x);
        
        int currentRoadDepth = Mathf.FloorToInt(transform.localScale.z);
        for (int i = offset; i < currentRoadDepth; i+=10)
        {
            CreateRow(-0.5f + i/100f);
        }
    }

    public void ClearRoad()
    {
        foreach (var roadElement in _roadElements)
        {
            pool.Inactivate(roadElement);
        }
        _roadElements.Clear();
    }

    private void CreateRow(float z)
    {
        int currentCoveredWidth = 0;
        int minHeight = 2;
        float initialX = -0.5f;
        while (currentCoveredWidth < _currentRoadWidth)
        {
            RoadElement current = null;
            float randomElement = Random.Range(0f, 1f);
            if (randomElement < GameConstants.blockProbability)
            {
                if (_currentRoadWidth - currentCoveredWidth <= 2)
                {
                    if (minHeight == 2)
                    {
                        break;
                    }
                }
                int randomObstacle = Random.Range(0, _currentRoadWidth - currentCoveredWidth < 2 ? 2 : 4);
                current = pool.Get((RoadElementType)randomObstacle);
                if (minHeight > current.height)
                {
                    minHeight = current.height;
                }
            }
            else if (randomElement < GameConstants.blockProbability + GameConstants.coinProbability)
            {
                current = pool.Get(RoadElementType.Coin);
                minHeight = 0;
            }

            if (current == null)
            {
                currentCoveredWidth++;
                minHeight = 0;
            }
            else
            {
                _roadElements.Add(current);
                Transform currentTransform;
                (currentTransform = current.transform).SetParent(transform);
                currentTransform.localPosition = new Vector3(initialX + (currentCoveredWidth + current.width/2f) * 0.125f, currentTransform.localPosition.y, z);
                currentCoveredWidth += current.width;
            }
        }
    }
}
