using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMap : MonoBehaviour
{
    public static LayerMap currentLayerMap;

    [SerializeField] private LayerMask[] layers = new LayerMask[] { };

    private Dictionary<string, LayerMask> layersDictionary;

    private void Awake()
    {
        currentLayerMap = this;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        layersDictionary = new Dictionary<string, LayerMask>();

        foreach (LayerMask layer in layers)
        {
            layersDictionary.Add((LayerMask.LayerToName((int)Mathf.Log(layer.value, 2))), layer);
        }
    }

    public LayerMask GetLayer(string layerName)
    {
        LayerMask returnValue;
        layersDictionary.TryGetValue(layerName, out returnValue);

        return returnValue;
    }
}
