using UnityEngine;

public class SiatkaBrud : MonoBehaviour
{
    public SiatkaXZ<GridObjectBrud> gridBrud;
    public int rozmiarSiatki;
    public float cellSize;
    public GameObject droga;
    private GameObject slad;
    public Transform odkurzacz;
    [SerializeField] private Transform Tekst;
    private int px;
    private int pz;

    public void DodawanieBrudu()
    {
        for (int x = 0; x < gridBrud.GetWidth(); x++)
        {
            for (int z = 0; z < gridBrud.GetHeight(); z++)
            {
                slad = Instantiate(droga, gridBrud.GetWorldPosition(x, z), Quaternion.identity);
                gridBrud.GetGridObject(x, z).czyPuste = false;
            }
        }
    }
    public int IleDrogi()
    {
        int iloscDrogi = 0;
        for (int x = 0; x < gridBrud.GetWidth(); x++)
        {
            for (int z = 0; z < gridBrud.GetHeight(); z++)
            {
                if (gridBrud.GetGridObject(x, z).czyPuste == false)
                {
                    iloscDrogi++;
                }

            }
        }
        return iloscDrogi;
    }

    private void Awake()
    {
        gridBrud = new SiatkaXZ<GridObjectBrud>(rozmiarSiatki, rozmiarSiatki, cellSize, Vector3.zero, (SiatkaXZ<GridObjectBrud> g, int x, int z) => new GridObjectBrud(g, x, z), Tekst);
    }
    public class GridObjectBrud
    {
        private SiatkaXZ<GridObjectBrud> grid;
        private int x;
        private int z;
        public int liczbaPrzejsc = 0;
        public bool czyPuste = false;
        private Transform transform;

        public GridObjectBrud(SiatkaXZ<GridObjectBrud> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }
    }
}
