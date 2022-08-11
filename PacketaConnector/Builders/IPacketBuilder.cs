using PacketaConnector.DTO.CreateOrders;
using PacketaConnector.Services;

namespace PacketaConnector.Builders;

public interface IPacketBuilder
{
    CreateOrderRequest.createPacket BuildFromCreteOrderData(Packet packetData);
    CreateOrderRequest.createPacket Build();
    PacketBuilder WithApiPassword(string apiPassword);
    PacketBuilder WithNumber(string number);
    PacketBuilder WithName(string name);
    PacketBuilder WithSurname(string surname);
    PacketBuilder WithEmail(string email);
    PacketBuilder WithAddressId(uint addressId);
    PacketBuilder WithValue(decimal value);
    PacketBuilder WithPhone(string phone);
    PacketBuilder WithZip(string zip);
    PacketBuilder WithStreet(string street);
    PacketBuilder WithHouseNumber(string houseNumber);
    PacketBuilder WithCity(string city);
}