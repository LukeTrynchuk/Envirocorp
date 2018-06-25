using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FireBullet.Enviro.Utilities;

namespace FireBullet.Enviro.Board
{
    /// <summary>
    /// The Hex Mesh component will generate the
    /// required mesh for a single hex.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMesh : MonoBehaviour
    {
        #region Public Variables
        public Mesh Mesh => m_mesh;
        #endregion

        #region Private Variables
        private Mesh m_mesh;
        private List<Vector3> m_vertices;
        private List<int> m_triangles;
        private List<Color> m_colors;
        private MeshCollider m_meshCollider;
        #endregion

        #region Main Methods
        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = m_mesh = new Mesh();
            m_mesh.name = "Hex Mesh";
            m_vertices = new List<Vector3>();
            m_triangles = new List<int>();
            m_colors = new List<Color>();
        }

        public void Triangulate(HexCell[] cells)
        {
            ClearOldData();
            TriangulateCells(cells);
            UpdateMesh();
            CreateMeshCollider();
        }
        #endregion

        #region Utility Methods
        private void UpdateMesh()
        {
            m_mesh.vertices = m_vertices.ToArray();
            m_mesh.triangles = m_triangles.ToArray();
            m_mesh.colors = m_colors.ToArray();

            m_mesh.RecalculateNormals();
        }

        private void CreateMeshCollider()
        {
            m_meshCollider = m_meshCollider ?? gameObject.AddComponent<MeshCollider>();
            m_meshCollider.sharedMesh = Mesh;
        }

		private void TriangulateCells(HexCell[] cells)
		{
            foreach (HexCell cell in cells)
                Triangulate(cell);
		}
  
        private void Triangulate(HexCell cell)
        {
            Vector3 center = cell.transform.localPosition;
            for (int i = 0; i < 6; i++)
            {
                AddTriangle(center,
                            center + HexMetrics.corners[i],
                            center + HexMetrics.corners[i + 1]);

                AddTriangleColor(cell.m_Color);
            }
        }

        void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = m_vertices.Count;
            m_vertices.Add(v1);
            m_vertices.Add(v2);
            m_vertices.Add(v3);
            m_triangles.Add(vertexIndex);
            m_triangles.Add(vertexIndex + 1);
            m_triangles.Add(vertexIndex + 2);
        }

        private void ClearOldData()
        {
            m_mesh.Clear();
            m_vertices.Clear();
            m_triangles.Clear();
            m_colors.Clear();
        }

        private void AddTriangleColor(Color color)
        {
            m_colors.Add(color);
            m_colors.Add(color);
            m_colors.Add(color);
        }
        #endregion
    }
}
