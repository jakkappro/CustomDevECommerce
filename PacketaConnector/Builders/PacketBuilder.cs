using PacketaConnector.DTO.CreateOrders;
using PacketaConnector.Services;

namespace PacketaConnector.Builders;

public class PacketBuilder : IPacketBuilder
{
    private readonly CreateOrderRequest.createPacket _packet;

    public PacketBuilder()
    {
        _packet = new CreateOrderRequest.createPacket()
        {
            packetAttributes = new CreateOrderRequest.createPacketPacketAttributes
            {
                eshop = "AzetCool",
                weight = 0.99m,
                sender_id = 331585,
                security = new CreateOrderRequest.createPacketPacketAttributesSecurity()
                {
                    allowPublicTracking = 1
                }
            }
        };
    }

    public CreateOrderRequest.createPacket BuildFromCreateOrderData(Packet packetData, string apiPassword)
    {
        return new PacketBuilder().WithApiPassword(apiPassword)
            .WithNumber(packetData.Number)
            .WithName(packetData.Firstname)
            .WithSurname(packetData.Surname)
            .WithEmail(packetData.Email)
            .WithAddressId(packetData.AddressId)
            .WithValue(packetData.TotalPrice)
            .WithPhone(packetData.Phone)
            .WithZip(packetData.Zip)
            .WithStreet(packetData.Address)
            .WithHouseNumber(packetData.HouseNumber)
            .WithCity(packetData.City).Build();
    }

    public CreateOrderRequest.createPacket Build()
    {
        return _packet;
    }

    public PacketBuilder WithApiPassword(string apiPassword)
    {
        _packet.apiPassword = apiPassword;
        return this;
    }

    public PacketBuilder WithNumber(string number)
    {
        _packet.packetAttributes.number = number;
        return this;
    }

    public PacketBuilder WithName(string name)
    {
        _packet.packetAttributes.name = name;
        return this;
    }

    public PacketBuilder WithSurname(string surname)
    {
        _packet.packetAttributes.surname = surname;
        return this;
    }

    public PacketBuilder WithEmail(string email)
    {
        _packet.packetAttributes.email = email;
        return this;
    }

    public PacketBuilder WithAddressId(uint addressId)
    {
        _packet.packetAttributes.addressId = addressId;
        return this;
    }

    public PacketBuilder WithValue(decimal value)
    {
        _packet.packetAttributes.value = value;
        return this;
    }

    public PacketBuilder WithPhone(string phone)
    {
        _packet.packetAttributes.phone = phone;
        return this;
    }

    public PacketBuilder WithZip(string zip)
    {
        _packet.packetAttributes.zip = zip;
        return this;
    }

    public PacketBuilder WithStreet(string street)
    {
        _packet.packetAttributes.street = street;
        return this;
    }

    public PacketBuilder WithHouseNumber(string houseNumber)
    {
        _packet.packetAttributes.houseNumber = houseNumber;
        return this;
    }

    public PacketBuilder WithCity(string city)
    {
        _packet.packetAttributes.city = city;
        return this;
    }
}