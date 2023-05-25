
    public class RootGetArtist
    {
        public List<string> namevariations { get; set; }
        public string name { get; set; }
        public string profile { get; set; }
        public string releases_url { get; set; }
        public string resource_url { get; set; }
        public string uri { get; set; }
        public List<string> urls { get; set; }
        public string data_quality { get; set; }
        public int id { get; set; }
        public List<Image> images { get; set; }
        public List<Member> members { get; set; }
        public bool tieneImagenes { get; set; }
        public bool tieneVideos { get; set; }
        public string? link_video { get; set; }   // No estaba originalmente en el JSON. Agregado posteriormente
        public string? link_video2 { get; set; }
        public string? link_video3 { get; set; }
        public string? link_video4 { get; set; }
        
    }

    public class Member
    {
        public bool active { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string resource_url { get; set; }
    }

public class Image
    {
        public string type { get; set; }
        public string uri { get; set; }
        public string resource_url { get; set; }
        public string uri150 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }