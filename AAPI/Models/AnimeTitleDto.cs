namespace AAPI.Models;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class Episodes
{
    public string @string { get; set; }
}

public class Franchise
{
    public List<Release> releases { get; set; }
}

public class Hls
{
    public string sd { get; set; }
    public string hd { get; set; }
}

public class List
{
    public string name { get; set; }
    public string preview { get; set; }
    public Hls hls { get; set; }
}

public class Medium
{
    public string url { get; set; }
}

public class Names
{
    public string ru { get; set; }
    public string en { get; set; }
    public string alternative { get; set; }
}

public class Player
{
    public string host { get; set; }
    public Episodes episodes { get; set; }
    public List<List> list { get; set; }
}

public class Posters
{
    public Medium medium { get; set; }
}

public class Release
{
    public string code { get; set; }
    public Names names { get; set; }
}

public class AnimeTitleDto
{
    public string code { get; set; }
    public Names names { get; set; }
    public Posters posters { get; set; }
    public Type type { get; set; }
    public List<string> genres { get; set; }
    public Season season { get; set; }
    public string description { get; set; }
    public Player player { get; set; }
    public List<Franchise> franchises { get; set; } = new List<Franchise>();
}

public class Season
{
    public int year { get; set; }
}

public class Type
{
    public string full_string { get; set; }
}


