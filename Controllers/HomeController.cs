using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectAdolf.Models;
// using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProjectAdolf.Controllers;



public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        
    }

    public async Task<IActionResult> Index()
    {   
        try
        {
            MyGlobals.listaID=ConsultarIdMiColeccion();
            MyGlobals.miColeccion= await Task.Run(() =>ConsultarMiColeccion());
            return View();
        }
        catch (System.Exception)
        {
            return View("---     Demasiadas consultas consecutivas a Discogs. Espere 25 segundos y vuelva a recargar la pàgina porfavor       ---");
            throw;
        }
        
    }

    // public IActionResult Index()
    // {   
    //     MyGlobals.listaID=ConsultarIdMiColeccion();
    //     MyGlobals.miColeccion=ConsultarMiColeccion();
    //     return View();
    // }



    public List<int> ConsultarIdMiColeccion()
{
    string apiUrl = "https://api.discogs.com/users/dirtynoise/collection/folders/0/releases";
    var client = new HttpClient();
            // Agregamos los elementos al header
    client.DefaultRequestHeaders.Add("User-Agent", "myDiscogsClient/1.0");
    var response = client.GetAsync(apiUrl).Result;
    var content = response.Content.ReadAsStringAsync().Result;
    Console.WriteLine(content);
    miColeccionDiscos miColeccion = JsonSerializer.Deserialize<miColeccionDiscos>(content);
    List<int> listaID=new List<int>();
    foreach (Lanzamiento disco in miColeccion.releases)
    {
        listaID.Add(disco.id);
 
    }

    return listaID;
 }




public double consultarPrecio(int IdDisco)  // De un disco en estado Mint                      
{      
    string apiUrl = String.Format("https://api.discogs.com/marketplace/price_suggestions/{0}",IdDisco);
    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("User-Agent", "myDiscogsClient/1.0");
    client.DefaultRequestHeaders.Add("Authorization", "Discogs token=VPHXTEnHeHVDERtqHWCJAcaWLlHFBKwWxIirDWIC");
    var response = client.GetAsync(apiUrl).Result;
    var content = response.Content.ReadAsStringAsync().Result;
    Dictionary<string, MintM> diccionario = JsonSerializer.Deserialize<Dictionary<string, MintM>>(content);  //  El json que retorna es muy parecido a un diccionario y las clases del conversor no funcionaban, se me ocurrio deserializarlo en un diccionario y ha funcionado 
    double precio=diccionario["Mint (M)"].value;
    precio=Math.Round(precio,2);        
    return precio;
}

public RootGetArtist ConsultarArtista(int IdArtista)
{
    string apiUrl = String.Format("https://api.discogs.com/artists/{0}",IdArtista);
    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("User-Agent", "myDiscogsClient/1.0");
    client.DefaultRequestHeaders.Add("Authorization", "Discogs token=VPHXTEnHeHVDERtqHWCJAcaWLlHFBKwWxIirDWIC");
    var response = client.GetAsync(apiUrl).Result;
    var content = response.Content.ReadAsStringAsync().Result;
    RootGetArtist infoArtista = JsonSerializer.Deserialize<RootGetArtist>(content);  

    return infoArtista;
}
    






    public List<RootInfoDisco> ConsultarMiColeccion()
    {   
    
    List<RootInfoDisco> listaDiscos=new List<RootInfoDisco>();
        foreach (int Iddisco in MyGlobals.listaID)
    {   
        string apiUrl = String.Format("https://api.discogs.com/releases/{0}",Iddisco);
        var client = new HttpClient();
                // Agregamos los elementos al header
        client.DefaultRequestHeaders.Add("User-Agent", "myDiscogsClient/1.0");
        client.DefaultRequestHeaders.Add("Authorization", "Discogs token=VPHXTEnHeHVDERtqHWCJAcaWLlHFBKwWxIirDWIC");
        var response = client.GetAsync(apiUrl).Result;

        var content = response.Content.ReadAsStringAsync().Result;
        RootInfoDisco infoDisco = JsonSerializer.Deserialize<RootInfoDisco>(content);
 
        infoDisco.price_mint=consultarPrecio(Iddisco);
        listaDiscos.Add(infoDisco);
    }
    return listaDiscos;
    }


    public async Task<IActionResult> Artistas()
    {   
        List<RootInfoDisco> listaDiscos=MyGlobals.miColeccion;
        List<RootGetArtist> listaArtistas=new List<RootGetArtist>();
        List<int> listaIdArtistas=new List<int>();
        foreach (RootInfoDisco disco in listaDiscos)
        {   

            RootGetArtist artista=ConsultarArtista(disco.artists[0].id);
            
            if (listaIdArtistas.Contains(artista.id)==false) // Si no apareix a la llista d'id artista  es que no esta repetit i per tant el podrem afegir
            {   
                 if (disco.videos!=null)
                {   artista.tieneVideos=true;
                
                try
                {
                    if(disco.videos[0]!=null)
                    {
                        string linkVideo=disco.videos[0].uri;
                    artista.link_video=linkVideo.Substring(32,11);    //  Li agrego a artista el codi  del video a youtube de un dels seus discos .... // AMPLIAR AMB INFO
                    }
                    if(disco.videos[1]!=null)
                    {
                        string linkVideo=disco.videos[1].uri;
                    artista.link_video2=linkVideo.Substring(32,11);    //  Li agrego a artista el codi  del video a youtube de un dels seus discos .... // AMPLIAR AMB INFO
                    }
                }
                catch
                {

                }
                
                // Console.WriteLine(artista.link_video);
                // Console.WriteLine(artista.name);
                
                if (artista.images==null)
                {
                    Console.WriteLine("No tiene imagenes");
                    artista.tieneImagenes=false;
                }
                else{
                    Console.WriteLine(artista.images[0].uri);
                    artista.tieneImagenes=true;
                }
                }
                else{
                    artista.tieneVideos=false;
                }
                
                if (artista.profile!=null)
                {
                    listaArtistas.Add(artista);
                }
                
            }
            listaIdArtistas.Add(artista.id);
           
        }
        return View(listaArtistas);
    }

    public IActionResult Discos()
    {   
        return View(MyGlobals.miColeccion);
    }



    public IActionResult Privacy()
    {   
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
