using System.Collections;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class GridExample : MonoBehaviour
    {
        [SerializeField] private Grid<Vector3> xyGrid;
        [SerializeField] private Grid<int> xzGrid;


        public void Start()
        {
            xyGrid = new Grid<Vector3>(10, 10, 10, GridOrientation.XY, transform.position,
                    (grid, x, y) => new Vector3(x, 0, y))
                .GridGizmos(false, Color.cyan);

            this.DoAfter(5, () =>
                xzGrid = new Grid<int>(10, 10, 10, GridOrientation.XZ, transform.position,
                        (grid, x, y) => x * grid.Height * y)
                    .GridGizmos(true, Color.red));

            StartCoroutine(testCor());
        }

        public IEnumerator testCor()
        {
            yield return this.DoAfter(10, () =>
            {
                xyGrid.GridGizmos(false, Color.black);
                xzGrid.GridGizmos(true, Color.blue);
            });
            Debug.Log("Waited");
        }
    }
}
