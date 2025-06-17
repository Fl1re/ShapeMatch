using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int maxCapacity = 7;

    private List<ShapeData> currentShapes = new();
    private List<GameObject> slotInstances = new();

    public UnityEvent OnLose;

    private void OnEnable()
    {
        ShapeSpawnerEvents.OnMatch += OnMatchHappened;
    }

    private void OnDisable()
    {
        ShapeSpawnerEvents.OnMatch -= OnMatchHappened;
    }

    private void OnMatchHappened()
    {
        foreach (Transform child in transform)
        {
            var shape = child.GetComponent<Shape>();
            if (shape != null)
            {
                shape.DecreaseUnfreezeCounter();
            }
        }
    }
    
    public void AddShape(Shape shape)
    {
        var data = shape.ToShapeData();

        currentShapes.Add(data);
        
        GameObject uiGO = Instantiate(slotPrefab, slotContainer);
        uiGO.GetComponent<ShapeSlotUI>().Setup(
            baseSprite: data.BaseSprite,
            iconSprite: data.IconSprite,
            color: data.Color,
            iconOffset: data.IconOffset,
            backOffset: data.BackOffset
        );
        slotInstances.Add(uiGO);

        bool removed = TryRemoveMatchingTriplet();

        if (!removed && currentShapes.Count >= maxCapacity)
        {
            OnLose?.Invoke();
        }
    }

    private bool TryRemoveMatchingTriplet()
    {
        for (int i = 0; i < currentShapes.Count; i++)
        {
            ShapeData a = currentShapes[i];
            var matches = new List<int> { i };

            for (int j = 0; j < currentShapes.Count; j++)
            {
                if (i == j) continue;
                if (a.Matches(currentShapes[j]))
                    matches.Add(j);
            }

            if (matches.Count >= 3)
            {
                RemoveMatches(matches);
                return true;
            }
        }

        return false;
    }

    private void RemoveMatches(List<int> indices)
    {
        indices.Sort((a, b) => b.CompareTo(a));

        foreach (int index in indices)
        {
            Destroy(slotInstances[index]);
            slotInstances.RemoveAt(index);
            currentShapes.RemoveAt(index);
            
        }
        
        ShapeSpawnerEvents.NotifyMatch();
    }
    
    public void Clear()
    {
        foreach (var slot in slotInstances)
        {
            Destroy(slot);
        }

        slotInstances.Clear();
        currentShapes.Clear();
    }
}
