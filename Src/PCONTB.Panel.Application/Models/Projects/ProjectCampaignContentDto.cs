using PCONTB.Panel.Application.Common.Functions.Files;
using PCONTB.Panel.Domain.Projects.Campaigns;

namespace PCONTB.Panel.Application.Models.Projects
{
    public class ProjectCampaignContentDto
    {
        public int Order { get; set; }
        public ProjectCampaignContentType Type { get; set; }
        public object? Data { get; set; }

        public static ProjectCampaignContentDto Map(ProjectCampaignContent content)
        {
            return new ProjectCampaignContentDto
            {
                Order = content.Order,
                Type = content.Type,
                Data = content.Data,
            };
        }
    }

    public class TitleDto
    {
        public string Title { get; set; }
    }

    public class SubtitleDto
    {
        public string Subtitle { get; set; }
    }

    public class ParagraphDto
    {
        public string Paragraph { get; set; }
    }

    public class ImageDto
    {
        public FormFile Image { get; set; }
        public byte[] ImageData { get; set; }
    }

    public class QuoteDto
    {
        public string Quote { get; set; }
    }

    public class VideoDto
    {
        public FormFile Video { get; set; }
        public byte[] VideoData { get; set; }
    }

    public class ButtonDto
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }

    public class DividerDto
    {
    }

    public class ListDto
    {
        public List<ListItemDto> Items { get; set; } = [];
    }

    public class ListItemDto
    {
        public string Text { get; set; }
        public int? Order { get; set; }
    }
}