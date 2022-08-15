using PacketaConnector.DTO.GenerateLabels;

namespace PacketaConnector.Builders;

public class LabelBuilder : ILabelBuilder
{
    private readonly GenerateLabelRequest.packetLabelPdf _label;

    public LabelBuilder()
    {
        _label = new GenerateLabelRequest.packetLabelPdf()
        {
            format = "A6 on A4",
            offset = 0
        };
    }

    public GenerateLabelRequest.packetLabelPdf BuildFromCreateOrderData(uint id, string apiPassword)
    {
        return new LabelBuilder().WithApiPassword(apiPassword)
            .WithId(id)
            .Build();
    }

    public LabelBuilder WithApiPassword(string apiPassword)
    {
        _label.apiPassword = apiPassword;
        return this;
    }

    public LabelBuilder WithId(uint id)
    {
        _label.packetId = id;
        return this;
    }

    public GenerateLabelRequest.packetLabelPdf Build()
    {
        return _label;
    }
}