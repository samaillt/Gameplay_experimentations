using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * PoissonDiscSampling functions
 * Regroups every function or properties allowing to generate elements randomly on a region without interfering with each others. Inspired by Sebastian Lague tutorial : https://www.youtube.com/watch?v=7WcmyxyFO7o
 * @author tom_samaille
 */
public static class PoissonDiscSampling {

    /**
     * GeneratePoints 
     * Generate valid poitns on a region
     * @param radius float argument correponding to the minimal radius between each point.
     * @param sampleRegionSize Vector2 argument correponding to the size of the region wich will regroup the points.
     * @param numSamplesBeforeRejection integer argument correponding to the number of iteration before rejecting a sample (precision of the sampling).
     * @return List<Vector2> corresponding to the points added on the region
     * @author tom_samaille
     */
    public static List<Vector2> GeneratePoints(float radius, Vector2 sampleRegionSize, int numSamplesBeforeRejection)
    {
        float cellSize = radius/Mathf.Sqrt(2);
        int[,] grid = new int[Mathf.CeilToInt(sampleRegionSize.x/cellSize), Mathf.CeilToInt(sampleRegionSize.y/cellSize)];
        List<Vector2> points = new List<Vector2>();
        List<Vector2> spawnPoints = new List<Vector2>();
        spawnPoints.Add(sampleRegionSize/10); // divide by 10 instead of 2 because we want first spawn point to not be in the center
        while (spawnPoints.Count > 0)
        {
			int spawnIndex = Random.Range(0,spawnPoints.Count);
			Vector2 spawnCentre = spawnPoints[spawnIndex];
			bool candidateAccepted = false;
            for (int i = 0; i < numSamplesBeforeRejection; i++)
            {
                float angle = Random.value * Mathf.PI * 2;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCentre + dir * Random.Range(radius, 2*radius);
                if (IsValid(candidate, sampleRegionSize, cellSize, radius, points, grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int)(candidate.x/cellSize),(int)(candidate.y/cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;
                }
            }
            if (!candidateAccepted)
            {
				spawnPoints.RemoveAt(spawnIndex);
			}
		}
        return points;
    }

    /**
     * IsValid 
     * Return true if a point (candidate) is valid and false if not
     * @param candidate Vector2 argument correponding to the current point we want to test validity
     * @param sampleRegionSize Vector2 argument correponding to the size of the region wich will regroup the points.
     * @param cellSize float argument to the size of each grid cell
     * @param radius float argument correponding to the minimal radius between each point.
     * @param points List<Vector2> corresponding to the current points added on the region
     * @param grid int[,] corresponding to the actual grid
     * @return bool true if a point (candidate) is valid and false if not
     * @author tom_samaille
     */
    static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
    {
        if (candidate.x >=0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 && candidate.y < sampleRegionSize.y && (Mathf.Sqrt(Mathf.Pow(candidate.x - sampleRegionSize.x / 2, 2) + Mathf.Pow(candidate.y - sampleRegionSize.y / 2, 2)) > 10f)) // Last condition to remove (just to avoid trees to spawn in a specific circle in the center)
        {
            // Debug.Log("x : " + candidate.x + " y : " + candidate.y);
            int cellX = (int)(candidate.x/cellSize);
            int cellY = (int)(candidate.y/cellSize);
            int searchStartX = Mathf.Max(0,cellX -2);
            int searchEndX = Mathf.Min(cellX+2,grid.GetLength(0)-1);
            int searchStartY = Mathf.Max(0,cellY -2);
            int searchEndY = Mathf.Min(cellY+2,grid.GetLength(1)-1);
            for (int x = searchStartX; x <= searchEndX; x++)
            {
                for (int y = searchStartY; y <= searchEndY; y++)
                {
                    int pointIndex = grid[x,y] - 1;
                    if (pointIndex != -1)
                    {
                        float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
                        if (sqrDst < radius*radius)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }
}