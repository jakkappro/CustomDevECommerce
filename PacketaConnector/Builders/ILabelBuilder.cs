using PacketaConnector.DTO.GenerateLabels;

namespace PacketaConnector.Builders;

public interface ILabelBuilder
{
    GenerateLabelRequest.packetLabelPdf BuildFromCreteOrderData(uint id, string apiPassword);
    LabelBuilder WithApiPassword(string apiPassword);
    LabelBuilder WithId(uint id);
    GenerateLabelRequest.packetLabelPdf Build();
}