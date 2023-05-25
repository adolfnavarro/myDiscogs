namespace ProjectAdolf.Models;
public class miColeccionDiscos
    {   
        // public Paginacion pagination { get; set; }
        public List<Lanzamiento> releases { get; set; }
    }

public class Lanzamiento
{
    public int id { get; set; }
    // public int instance_id { get; set; }
    // public int rating { get; set; }
}

// public class Paginacion
//     {
        // public int page { get; set; }
        // public int pages { get; set; }
        // public int per_page { get; set; }
    //     public int items { get; set; }

    // }