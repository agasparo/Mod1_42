using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public enum DrawMode {NoiseMap, Mesh};
	public DrawMode drawMode;

	public TerrainData terrainData;
	public NoiseData noiseData;
    public TextureData textureData;

    public Material terrainMaterial;
    public static float[,] earthMap;

    public int mapWidth;
    public int mapHeight;

    public bool autoUpdate;

    void Start()
    {
        DrawMapInEditor();
    }

    public MapData GenerateMap() {

    	float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);
        textureData.updateMeshHeights(terrainMaterial, terrainData.minHeight, terrainData.maxHeight);
        return new MapData(noiseMap);
    }

    public void DrawMapInEditor() {

        MapData mapData = GenerateMap();

        earthMap = mapData.noiseMap;
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.noiseMap));
        if (drawMode == DrawMode.Mesh)
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve));
    }

    void OnValuesUpdated() {

        if (!Application.isPlaying) {

            DrawMapInEditor();
        }
    }

    void OnTextureValuesUpdated() {

        textureData.ApplyToMaterial(terrainMaterial);
    }

    void OnValidate() {

    	if (mapWidth < 1)
    		mapWidth = 1;
    	if (mapHeight < 1)
    		mapHeight = 1;

        if (terrainData != null) {

            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }

        if (noiseData != null) {

            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }

        if (textureData != null) {

            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
    }
}