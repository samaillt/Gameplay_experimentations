using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TreeGeneration
 * Regroups every function or properties allowing to generate trees on a soecific surface
 * @author tom_samaille
 */
public class TreeGeneration : MonoBehaviour
{
    /** 
     * m_Radius describes minimal radius between each tree.
     */
    public float m_Radius;
    /** 
     * m_RegionSize refers to the size of the region wich will regroup the trees.
     */
	public Vector2 m_RegionSize;
    /** 
     * m_RejectionSamples refers to the number of iteration before rejecting a sample (precision of the sampling).
     */
	public int m_RejectionSamples = 5;
    /** 
     * m_Points is a list of Vector2 representing x and z coordinates of the center of every tree to generate
     */
	List<Vector2> m_Points;
    /** 
     * m_TreeNumber is an integer referring to the number of tree generated
     */
    private int m_TreeNumber;

    /**
     * Generating a tree
     * Creates and initialize a tree GameObject 
     * @param position a Vector2 corresponding to x and z coordinates of the tree to display
     * @author tom_samaille
     */
    private void generateTree(Vector2 position)
    {
        float treeSize = Random.Range(0.5f, 1.5f);
        float treeHeight = Random.Range(3f, 7f);
        Quaternion treeRotation = Quaternion.Euler(0, Random.Range(-45f, 45f), 0);
        GameObject tmpTree = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tmpTree.transform.position = new Vector3(position.x, treeHeight / 2, position.y);
        tmpTree.transform.localScale = new Vector3(treeSize, treeHeight, treeSize);
        tmpTree.transform.rotation = treeRotation;
        tmpTree.transform.name = "Tree";
        tmpTree.AddComponent<FireSensitive>();
        tmpTree.tag = "Tree";
    }

    /**
     * Start is called before the first frame update
     * @author tom_samaille
     */
    void Start()
    {
        m_Radius = 4f;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        m_RegionSize = new Vector2(transform.localScale.x, transform.localScale.z) * 10;
        m_Points = PoissonDiscSampling.GeneratePoints(m_Radius, m_RegionSize, m_RejectionSamples);
        if (m_Points != null) {
            m_TreeNumber = m_Points.Count;
			foreach (Vector2 point in m_Points) {
                generateTree(new Vector2(point.x - m_RegionSize.x / 2, point.y - m_RegionSize.y / 2));
			}
		}
    }

    /**
     * GetTreeNumber a getter af TreeNumber attribute
     * @return an integer referring to the number of trees
     * @author tom_samaille
     */
    public int GetTreeNumber()
    {
        return m_TreeNumber;
    }
}
