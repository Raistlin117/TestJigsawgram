using UnityEngine;

namespace Gameplay.Puzzle
{
    public class PuzzleGenerator : MonoBehaviour
    {
        [SerializeField] public Sprite _pieceMask;
        [SerializeField] public Material _puzzleMaterial;
        [SerializeField] [Range(0.5f, 1f)] public float _overlapFactor = 0.76f;

        public void Generate(Texture2D art, int columns, int rows)
        {
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);

            float fullWidth = _pieceMask.bounds.size.x;
            float fullHeight = _pieceMask.bounds.size.y;

            float stepX = fullWidth * _overlapFactor;
            float stepY = fullHeight * _overlapFactor;

            float totalWidth = (columns - 1) * stepX + fullWidth;
            float totalHeight = (rows - 1) * stepY + fullHeight;

            Vector2 uvScale = new Vector2(fullWidth / totalWidth, fullHeight / totalHeight);
            Vector3 centeringOffset =
                new Vector3(totalWidth / 2f - fullWidth / 2f, totalHeight / 2f - fullHeight / 2f, 0f);

            _puzzleMaterial.SetTexture("_ArtTex", art);
            _puzzleMaterial.SetTexture("_MainTex", _pieceMask.texture);

            Vector2[] maskVerts2D = _pieceMask.vertices;
            Vector3[] maskVerts3D = new Vector3[maskVerts2D.Length];

            for (int i = 0; i < maskVerts2D.Length; i++)
            {
                maskVerts3D[i] = maskVerts2D[i];
            }

            int[] maskTris = System.Array.ConvertAll(_pieceMask.triangles, t => (int)t);
            Vector2[] uv0 = _pieceMask.uv;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    var piece = new GameObject($"Piece_{x}_{y}");
                    piece.transform.SetParent(transform, false);

                    Vector3 pos = new Vector3(x * stepX, y * stepY, 0f) - centeringOffset;
                    piece.transform.localPosition = new Vector3(pos.x, pos.y, ((y * columns) + x) * -0.0001f);

                    var uv1 = new Vector2[uv0.Length];
                    Vector2 uvOffset = new Vector2((x * stepX) / totalWidth, (y * stepY) / totalHeight);

                    for (int i = 0; i < uv0.Length; i++)
                    {
                        uv1[i] = new Vector2(uv0[i].x * uvScale.x + uvOffset.x, uv0[i].y * uvScale.y + uvOffset.y);
                    }

                    var mesh = new Mesh { name = "PuzzlePieceMesh" };

                    mesh.vertices = maskVerts3D;
                    mesh.triangles = maskTris;
                    mesh.uv = uv0;
                    mesh.uv2 = uv1;

                    piece.AddComponent<MeshFilter>().mesh = mesh;
                    piece.AddComponent<MeshRenderer>().material = _puzzleMaterial;
                    piece.AddComponent<BoxCollider2D>();
                    piece.AddComponent<PuzzlePiece>().Setup(piece.transform.position);
                }
            }
        }
    }
}