public class Order {
    public String ?orderId { get; set; }
    public String ?customerId { get; set; }
    public String ?employeeId { get; set; }
    public String ?orderDate { get; set; }
    public String ?requiredDate { get; set; }
    public String ?shippedDate { get; set; }
    public String ?shipVia { get; set; }
    public String ?freight { get; set; }
    public String ?shipName { get; set; }
    public String ?shipAddress { get; set; }
    public String ?shipCity { get; set; }
    public String ?shipRegion { get; set; }
    public String ?shipPostalCode { get; set; }
    public String ?shipCountry { get; set; }
    public Order (string[] array) {
        this.orderId = array[0];
        this.customerId = array[1];
        this.employeeId = array[2];
        this.orderDate = array[3];
        this.requiredDate = array[4];
        this.shippedDate = array[5];
        this.shipVia = array[6];
        this.freight = array[7];
        this.shipName = array[8];
        this.shipAddress = array[9];
        this.shipCity = array[10];
        this.shipRegion = array[11];
        this.shipPostalCode = array[12];
        this.shipCountry = array[13];
    }
    public override String ToString() {
        return orderId + " " + customerId 
            + "\n" + employeeId + "\n" + orderDate + "\n" + requiredDate + "\n" + shippedDate + "\n" + shipVia  
            + "\n" + freight + "\n" + shipName + "\n" + shipAddress + "\n" + shipCity + "\n" + shipRegion + "\n" 
            + shipPostalCode + "\n" + shipCountry + "\n";
    }
}

public class Orders {
    public List<Order> data;
    public Orders(List<Order> data) {
        this.data = data;
    }
    public override string? ToString() {
        string to_return = "";
        foreach(Order r in this.data) {
            to_return += r.ToString();
            to_return += "\n";
        }
        return to_return;
    }
}