public class SiatkaObiektow
{
    private int x;
    private int z;
    public int liczbaPrzejsc = 10;
    public bool czyPosprzatane = false;
    public bool czySciana = false;
    public bool czyPuste = true;

    public SiatkaObiektow(SiatkaXZ<SiatkaObiektow> grid, int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public void SetPosprzatanie()
    {
        this.czyPosprzatane = true;
        this.czyPuste = false;
    }
}

