namespace PacketaConnector.Services;

public class Packet
{
    public Packet(string apiPassword, string number, string firstname, string surname, string email, uint addressId,
        decimal totalPrice, string phone, string zip, string address, string houseNumber, string city)
    {
        ApiPassword = apiPassword;
        Number = number;
        Firstname = firstname;
        Surname = surname;
        Email = email;
        AddressId = addressId;
        TotalPrice = totalPrice;
        Phone = phone;
        Zip = zip;
        Address = address;
        HouseNumber = houseNumber;
        City = city;
    }

    public string ApiPassword { get; }
    public string Number { get; }
    public string Firstname { get; }
    public string Surname { get; }
    public string Email { get; }
    public uint AddressId { get; }
    public decimal TotalPrice { get; }
    public string Phone { get; }
    public string Zip { get; }
    public string Address { get; }
    public string HouseNumber { get; }
    public string City { get; }
}