public class EmployeeTerritory {
    public String ?employeeId { get; set; }
    public String ?territoryId { get; set; }
    public EmployeeTerritory(string employeeId, string territoryId) {
        this.employeeId = employeeId;
        this.territoryId = territoryId;
    }
    public override String ToString() {
        return employeeId + " " + territoryId;
    }
}

public class EmployeeTerritories {
    public List<EmployeeTerritory> data;
    public EmployeeTerritories(List<EmployeeTerritory> data) {
        this.data = data;
    }
    public override string? ToString() {
        string to_return = "";
        foreach(EmployeeTerritory r in this.data) {
            to_return += r.ToString();
            to_return += "\n";
        }
        return to_return;
    }
}