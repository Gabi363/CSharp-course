public class OrderDetails {
    public String ?orderId { get; set; }
    public String ?productId { get; set; }
    public string ?unitprice { get; set; }
    public string ?quantity { get; set; }
    public string ?discount { get; set; }

    public OrderDetails(string orderId, string productId, string unitprice, string quantity, string discount) {
        this.orderId = orderId;
        this.productId = productId;
        this.quantity = quantity;
        this.unitprice = unitprice;
        this.discount = discount;
    }
    public override String ToString() {
        return orderId + " " + productId + " " + unitprice + " " + quantity + " " + discount;
    }
}

public class OrdersDetails {
    public List<OrderDetails> data;
    public OrdersDetails(List<OrderDetails> data) {
        this.data = data;
    }
    public override string? ToString() {
        string to_return = "";
        foreach(OrderDetails r in this.data) {
            to_return += r.ToString();
            to_return += "\n";
        }
        return to_return;
    }
}