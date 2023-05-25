public class RootInfoDisco
    {
        public int id { get; set; }
        public string status { get; set; }
        public int year { get; set; }
        public List<Image> images { get; set; }
        public string thumb { get; set; }
        
        public List<Artist> artists { get; set; }
        public string title { get; set; }
        public string artists_sort { get; set; }
        public string notes { get; set; }
        public double? lowest_price { get; set; }
        public double? price_mint { get; set; }  // No estaba originalmente en el json . Lo añado yo de otra consulta 
        // public string resource_url { get; set; }
        public string uri { get; set; }
        // public string artists_sort { get; set; }
        // public List<Label> labels { get; set; }
        // public List<object> series { get; set; }
        // public List<Company> companies { get; set; }
        // public List<Format> formats { get; set; }
        // public string data_quality { get; set; }
        // public Community community { get; set; }
        // public int format_quantity { get; set; }
        // public DateTime date_added { get; set; }
        // public DateTime date_changed { get; set; }
        public int num_for_sale { get; set; }
        
        public string country { get; set; }
        // public string released { get; set; }
        
        // public string released_formatted { get; set; }
        // public List<Identifier> identifiers { get; set; }
            public List<Video> videos { get; set; }
        // public List<string> genres { get; set; }
        // public List<string> styles { get; set; }
        // public List<Tracklist> tracklist { get; set; }
        // public List<Extraartist> extraartists { get; set; }
            
        
        // public int estimated_weight { get; set; }
        // public bool blocked_from_sale { get; set; }
    }

public class Artist
    {
        public string name { get; set; }
        public string anv { get; set; }
        public string join { get; set; }
        public string role { get; set; }
        public string tracks { get; set; }
        public int id { get; set; }
        public string resource_url { get; set; }
    }


    public class Video
    {
        public string uri { get; set; }
        // public string title { get; set; }
        // public string description { get; set; }
        // public int duration { get; set; }
        // public bool embed { get; set; }
    }

