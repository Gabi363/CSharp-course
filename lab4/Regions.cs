public class Region {
    public String ?id { get; set; }
    public String ?description { get; set; }
    public Region(string id, string description) {
        this.id = id;
        this.description = description;
    }
    public override String ToString() {
        return id + " " + description;
    }
}

public class Regions {
    public List<Region> data;
    public Regions(List<Region> list) {
        this.data = new List<Region>(list);
    }
    public override string? ToString() {
        string to_return = "";
        foreach(Region r in this.data) {
            to_return += r.ToString();
            to_return += "\n";
        }
        return to_return;
    }
}