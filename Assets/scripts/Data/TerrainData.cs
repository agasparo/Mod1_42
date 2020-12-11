using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdateData {
    
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public float minHeight {

    	get {
    		return (meshHeightCurve.Evaluate(0) * meshHeightMultiplier);
    	}
    }

    public float maxHeight {

    	get {
    		return (meshHeightCurve.Evaluate(1) * meshHeightMultiplier);
    	}
    }
}
