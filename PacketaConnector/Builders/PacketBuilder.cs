using PacketaConnector.DTO.CreateOrders;
using PacketaConnector.Services;

namespace PacketaConnector.Builders;

public class PacketBuilder : IPacketBuilder
{
    private CreateOrderRequest.createPacket packet;

    public PacketBuilder()
    {
        packet = new CreateOrderRequest.createPacket()
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

    public CreateOrderRequest.createPacket BuildFromCreteOrderData(Packet packetData, string apiPassword)
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
        return packet;
    }

    public PacketBuilder WithApiPassword(string apiPassword)
    {
        packet.apiPassword = apiPassword;
        return this;
    }

    public PacketBuilder WithNumber(string number)
    {
        packet.packetAttributes.number = number;
        return this;
    }

    public PacketBuilder WithName(string name)
    {
        packet.packetAttributes.name = name;
        return this;
    }

    public PacketBuilder WithSurname(string surname)
    {
        packet.packetAttributes.surname = surname;
        return this;
    }

    public PacketBuilder WithEmail(string email)
    {
        packet.packetAttributes.email = email;
        return this;
    }

    public PacketBuilder WithAddressId(uint addressId)
    {
        packet.packetAttributes.addressId = addressId;
        return this;
    }

    public PacketBuilder WithValue(decimal value)
    {
        packet.packetAttributes.value = value;
        return this;
    }

    public PacketBuilder WithPhone(string phone)
    {
        packet.packetAttributes.phone = phone;
        return this;
    }

    public PacketBuilder WithZip(string zip)
    {
        packet.packetAttributes.zip = zip;
        return this;
    }

    public PacketBuilder WithStreet(string street)
    {
        packet.packetAttributes.street = street;
        return this;
    }

    public PacketBuilder WithHouseNumber(string houseNumber)
    {
        packet.packetAttributes.houseNumber = houseNumber;
        return this;
    }

    public PacketBuilder WithCity(string city)
    {
        packet.packetAttributes.city = city;
        return this;
    }
}