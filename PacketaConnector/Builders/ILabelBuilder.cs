using PacketaConnector.DTO.GenerateLabels;

namespace PacketaConnector.Builders;

public interface ILabelBuilder
{
    GenerateLabelRequest.packetLabelPdf BuildFromCreateOrderData(string id, string apiPassword);
    LabelBuilder WithApiPassword(string apiPassword);
    LabelBuilder WithId(string id);
    GenerateLabelRequest.packetLabelPdf Build();
}