using UnityEngine;
using UnityEngine.UI;

namespace HuntroxGames.Utils
{
    public class UIGradient : BaseMeshEffect
    {

        [SerializeField] private Color firstColor = Color.white;
        [SerializeField] private Color secondColor = Color.white;
        [SerializeField,Range(-180,180)] private float angle = 0f;
        [SerializeField] private float offset = 0f;
        [SerializeField] private bool ignoreRatio = true;

            

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!enabled)
                return;
            LinearGradient(vh);

        }


        private void LinearGradient(VertexHelper vh)
        {
            var rect = graphic.rectTransform.rect;
            var angleRad = angle * Mathf.Deg2Rad;
            var dir = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            if (!ignoreRatio)
            {
                dir.x *= rect.width / rect.height;
                dir = dir.normalized;
            }
            
            var vertex = default(UIVertex);

            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                var percent = (vertex.position.x * dir.x + vertex.position.y * dir.y + offset) / rect.height;
                vertex.color *= Color.LerpUnclamped(firstColor, secondColor, percent);
                vh.SetUIVertex(vertex, i);
            }
        }
    }
}
