using ExpandoConnector.DTO.ExpandoFeed;
using PacketaConnector.Services;

namespace ConsoleApp.Mappers;

public static class ExpandoToPacketaPacket
{
    public static Packet Map(GetExpandoFeedRequest.ordersOrder order, string orderId)
    {
        return new Packet(
            number: orderId,
            firstname: order.customer.firstname,
            surname: order.customer.surname,
            email: order.customer.email,
            addressId: order.customer.address.country switch
            {
                "DE" => 13613,
                "AT" => 80,
                _ => throw new ArgumentOutOfRangeException()
            },
            totalPrice: order.totalPrice,
            phone: order.customer.phone,
            zip: order.customer.address.zip,
            address: order.customer.address.address1,
            houseNumber: order.customer.address.address2,
            city: order.customer.address.city
        );
    }
}