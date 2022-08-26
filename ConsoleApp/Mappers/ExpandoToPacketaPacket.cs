using ExpandoConnector.DTO.ExpandoFeed;
using PacketaConnector.Services;

namespace ConsoleApp.Mappers;

public static class ExpandoToPacketaPacket
{
    public static Packet Map(GetExpandoFeedRequest.ordersOrder order, string orderId)
    {
        return new Packet(
            orderId,
            order.customer.firstname,
            order.customer.surname,
            order.customer.email,
            order.customer.address.country switch
            {
                "DE" => 13613,
                "AT" => 80,
                _ => throw new ArgumentOutOfRangeException()
            },
            order.totalPrice,
            order.customer.phone,
            order.customer.address.zip,
            order.customer.address.address1,
            order.customer.address.address2,
            order.customer.address.city
        );
    }
}