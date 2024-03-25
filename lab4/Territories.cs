public class Territory {
    public String ?id { get; set; }
    public String ?description { get; set; }
    public string ?regionId { get; set; }
    public Territory(string id, string description, string regionId) {
        this.id = id;
        this.description = description;
        this.regionId = regionId;
    }
    public override String ToString() {
        return id + " " + description + " " + regionId;
    }
}

public class Territories {
    public List<Territory> data;
    public Territories(List<Territory> data) {
        this.data = data;
    }
    public override string? ToString() {
        string to_return = "";
        foreach(Territory r in this.data) {
            to_return += r.ToString();
            to_return += "\n";
        }
        return to_return;
    }
}