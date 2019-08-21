using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
namespace yz
{
    [RequireComponent(typeof(RadarImageData))]
    public class RadarImage : Image
    {
        private RadarImageData m_data;

        protected override void Awake()
        {
            m_data = GetComponent<RadarImageData>();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            if (m_data == null || m_data.Strength == null || m_data.Strength.Length < 3)
            {
                base.OnPopulateMesh(vh);
                return;
            }
            var edges = m_data.Strength.Length;

            var r = GetPixelAdjustedRect();
            var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);

            Color32 color32 = color;
            vh.Clear();

            var angleDelta = Mathf.PI * 2f / edges;
            var angle = Mathf.PI * 0.5f;
            var halfWidth = r.width * 0.5f;
            var halfHeight = r.height * 0.5f;

            vh.AddVert(new Vector3(0f, 0f, 0f), color32, new Vector2(0.5f, 0.5f));
            for (int i = 0; i < edges; i++)
            {
                var x = Mathf.Cos(angle) * halfWidth * m_data.Strength[i].value;
                var y = Mathf.Sin(angle) * halfHeight * m_data.Strength[i].value;
                vh.AddVert(new Vector3(x, y, 0f), color32, new Vector2((x + halfWidth) / r.width, (y + halfHeight) / r.height));
                angle += angleDelta;
            }

            for (int i = 1; i < edges; i++)
            {
                vh.AddTriangle(i, i + 1, 0);
            }
            vh.AddTriangle(edges, 1, 0);
        }

        public void Redraw()
        {
            SetVerticesDirty();
        }

#if UNITY_EDITOR
        private void Update()
        {
            Redraw();
        }
#endif
    }
}