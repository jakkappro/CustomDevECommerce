using System.Text.Json;
using PacketaConnector.DTO.GetPacketStatus;
using PacketaConnector.Interfaces;
using ServiceReference1;
using Webtrace;

namespace PacketaConnector.Services;

public class SpsCarrier : ICarrier
{
    private readonly WebshipWebServiceClient webshipClient;
    private readonly WebtracePortTypeClient webtraceClient;

    public SpsCarrier()
    {
        webshipClient = new WebshipWebServiceClient();
        webtraceClient = new WebtracePortTypeClient(WebtracePortTypeClient.EndpointConfiguration.WebtracePort,
            "http://t-t.sps-sro.sk/service_soap.php/wsdl");
    }

    public async Task<string> CreatePackage(Packet packet)
    {
        var shipment = new WebServiceShipment
        {
            cod = new Cod
            {
                codretbankacc = "123",
                codretbankcode = "123",
                codvalue = "123"
            },
            deliveryaddress = new ShipmentAddress
            {
                city = packet.City,
                contactPerson = packet.Firstname + " " + packet.Surname,
                country = "",
                email = packet.Email,
                mobile = packet.Phone,
                name = packet.Firstname + " " + packet.Surname,
                phone = packet.Phone,
                street = packet.Address,
                zip = packet.Zip
            },
            insurvalue = "1",
            notifytype = "nope",
            packages = new[]
            {
                new WebServicePackage
                {
                    reffnr = "1",
                    weight = "0.5"
                }
            },
            pickupaddress = null,
            productdesc = "krehke :D",
            recipientpay = false,
            recipientpaySpecified = true,
            returnshipment = false,
            returnshipmentSpecified = true,
            returnlabel = true,
            returnlabelSpecified = true,
            saturdayshipment = false,
            saturdayshipmentSpecified = true,
            servicename = "nevim",
            deliveryremark = "hehe",
            shipmentpickup = new ShipmentPickup
            {
                pickupstartdatetime = DateTime.Now,
                pickupenddatetime = DateTime.Today.Add(TimeSpan.FromDays(1.0))
            },
            tel = null,
            telSpecified = false,
            units = "kg",
            deliverytype = null,
            services = null,
            codattribute = 0
        };

        // TODO: figure out what 0 means :D
        var response = await webshipClient.createCifShipmentAsync("AC ECOM", "AC ECOM", shipment, 0);

        return "";
    }

    public Task<string> GetLabel(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<GetPacketStatusResponse.response> GetPackageInfo(string id)
    {
        var sdgLandnr = int.Parse(id[..2]);
        var sdgMandnr = int.Parse(id[2..4]);
        var sdgLfdnr = int.Parse(id[4..]);
        var kundenr = 12077;

        var getOrder = await webtraceClient.getShipmentAsync(sdgLandnr, sdgMandnr, sdgLfdnr, kundenr.ToString());

        Console.WriteLine(JsonSerializer.Serialize(getOrder));
        return null;
    }
}