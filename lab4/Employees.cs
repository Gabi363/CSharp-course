public class Employee {
    public String ?employeeId { get; set; }
    public String ?lastName { get; set; }
    public String ?firstName { get; set; }
    public String ?title { get; set; }
    public String ?titleOfCourtesy { get; set; }
    public String ?birthDate { get; set; }
    public String ?hireDate { get; set; }
    public String ?address { get; set; }
    public String ?city { get; set; }
    public String ?region { get; set; }
    public String ?postalCode { get; set; }
    public String ?country { get; set; }
    public String ?homePhone { get; set; }
    public String ?extension { get; set; }
    public String ?photo { get; set; }
    public String ?notes { get; set; }
    public String ?reportsTo { get; set; }
    public String ?photoPath { get; set; }
    // public Employee(string employeeId, string lastName, string firstName, string title, string titleOfCourtesy, string birthDate, string hireDate, string address, string city, string region, string postalCode, string country, string homePhone, string extension, string photo, string notes, string reportsTo, string photoPath) {
    //     this.employeeId = employeeId;
    //     this.lastName = lastName;
    //     this.firstName = firstName;
    //     this.title = title;
    //     this.titleOfCourtesy = titleOfCourtesy;
    //     this.birthDate = birthDate;
    //     this.hireDate = hireDate;
    //     this.address = address;
    //     this.city = city;
    //     this.region = region;
    //     this.postalCode = postalCode;
    //     this.country = country;
    //     this.homePhone = homePhone;
    //     this.extension = extension;
    //     this.photo = photo;
    //     this.notes = notes;
    //     this.reportsTo = reportsTo;
    //     this.photoPath = photoPath;
    // }
    public Employee (string[] array) {
        this.employeeId = array[0];
        this.lastName = array[1];
        this.firstName = array[2];
        this.title = array[3];
        this.titleOfCourtesy = array[4];
        this.birthDate = array[5];
        this.hireDate = array[6];
        this.address = array[7];
        this.city = array[8];
        this.region = array[9];
        this.postalCode = array[10];
        this.country = array[11];
        this.homePhone = array[12];
        this.extension = array[13];
        this.photo = array[14];
        this.notes = array[15];
        this.reportsTo = array[16];
        this.photoPath = array[17];
    }
    public override String ToString() {
        return employeeId + " " + lastName 
            + "\n" + firstName + "\n" + title + "\n" + titleOfCourtesy + "\n" + birthDate + "\n" + hireDate  
            + "\n" + address + "\n" + city + "\n" + region + "\n" + postalCode + "\n" + country + "\n" 
            + homePhone + "\n" + extension + "\n" + photo + "\n" + notes + "\n" + reportsTo 
            + "\n" + photoPath + "\n";
    }
}

public class Employees {
    public List<Employee> data;
    public Employees(List<Employee> data) {
        this.data = data;
    }
    public override string? ToString() {
        string to_return = "";
        foreach(Employee r in this.data) {
            to_return += r.ToString();
            to_return += "\n";
        }
        return to_return;
    }
}