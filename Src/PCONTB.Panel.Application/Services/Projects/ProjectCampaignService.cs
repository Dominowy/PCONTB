
using Newtonsoft.Json;
using PCONTB.Panel.Application.Contracts.Services.Projects;
using PCONTB.Panel.Application.Models.Projects;
using PCONTB.Panel.Domain.Projects;
using PCONTB.Panel.Domain.Projects.Campaigns;
using System.Text.Json;

namespace PCONTB.Panel.Application.Services.Projects
{
    public class ProjectCampaignService : IProjectCampaignService
    {
        public async Task<ProjectCampaignDto> GetCampaign(Project project, CancellationToken token)
        {
            var contents = project.Campaing?.CampaignContents
                .Select(c => new ProjectCampaignContentDto
                {
                    Order = c.Order,
                    Type = c.Type,
                    Data = DeserializeData(c.Data, c.Type)
                })
                .OrderBy(c => c.Order)
                .ToList() ?? [];

            return new ProjectCampaignDto 
            {
                Contents = contents
            };
        }

        private static object? DeserializeData(string? json, ProjectCampaignContentType type)
        {
            return type switch
            {
                ProjectCampaignContentType.Title => JsonConvert.DeserializeObject<TitleDto>(json),
                ProjectCampaignContentType.Subtitle => JsonConvert.DeserializeObject<SubtitleDto>(json),
                ProjectCampaignContentType.Paragraph => JsonConvert.DeserializeObject<ParagraphDto>(json),
                ProjectCampaignContentType.Image => JsonConvert.DeserializeObject<ImageDto>(json),
                ProjectCampaignContentType.Quote => JsonConvert.DeserializeObject<QuoteDto>(json),
                ProjectCampaignContentType.Video => JsonConvert.DeserializeObject<VideoDto>(json),
                ProjectCampaignContentType.Button => JsonConvert.DeserializeObject<ButtonDto>(json),
                ProjectCampaignContentType.Divider => JsonConvert.DeserializeObject<DividerDto>(json),
                ProjectCampaignContentType.List => JsonConvert.DeserializeObject<ListDto>(json),
                _ => throw new NotSupportedException($"Unsupported type: {type}")
            };
        }

        public async Task SetCampaign(Project project, ProjectCampaignDto campaign, CancellationToken token)
        {
            var contents = new List<ProjectCampaignContent>();

            if (campaign == null)
            {
                return;
            }

            foreach (var content in campaign.Contents)
            {
                var result = await HandleContent(project, content, content.Type, token);

                contents.Add(result);
            }

            if (project.Campaing == null)
            {
                var newCampaing = new ProjectCampaign(project.Id);

                foreach (var item in contents)
                {
                    newCampaing.AddContent(item);
                }

                project.SetProjectCampaing(newCampaing);
            } 
            else
            {
                var itemToRemove = project.Campaing.CampaignContents.Where(m => !contents.Any(c => c.Order == m.Order)).ToList();

                foreach (var item in itemToRemove)
                {
                    project.Campaing.RemoveContent(item);
                }

                foreach (var item in contents)
                {
                    var existingContent = project.Campaing.CampaignContents.Where(m => m.Order == item.Order).FirstOrDefault();

                    if (existingContent == null)
                    {
                        project.Campaing.AddContent(item);

                    } 
                    else
                    {
                        existingContent.SetType(item.Type);
                        existingContent.SetData(item.Data);
                    }
                }
            }
        }

        private async Task<ProjectCampaignContent> HandleContent(Project project, ProjectCampaignContentDto content, ProjectCampaignContentType contentType, CancellationToken token)
        {
            string? jsonString = null;

            switch (contentType)
            {
                case ProjectCampaignContentType.Title:
                    jsonString = GetData(content.Data);
                    break;
                case ProjectCampaignContentType.Subtitle:
                    jsonString = GetData(content.Data);
                    break;
                case ProjectCampaignContentType.Paragraph:
                    jsonString = GetData(content.Data);
                    break;
                case ProjectCampaignContentType.Image:
                    jsonString = await GetImage(content, token);
                    break;
                case ProjectCampaignContentType.Quote:
                    jsonString = GetData(content.Data);
                    break;
                case ProjectCampaignContentType.Video:
                    jsonString = await GetVideo(content, token);
                    break;
                case ProjectCampaignContentType.Button:
                    jsonString = GetData(content.Data);
                    break;
                case ProjectCampaignContentType.Divider:
                    jsonString = GetData(content.Data); 
                    break;
                case ProjectCampaignContentType.List:
                    jsonString = GetData(content.Data); 
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
            }

            return new ProjectCampaignContent(content.Order, contentType, jsonString);
        }

        private async Task<string?> GetImage(ProjectCampaignContentDto content, CancellationToken token)
        {
            var jsonString = GetData(content.Data);

            if (jsonString == null) return null;

            var imageDto = JsonConvert.DeserializeObject<ImageDto>(jsonString);

            if (imageDto != null && imageDto.Image != null)
            {
                if (!string.IsNullOrEmpty(imageDto.Image.Path))
                {
                    imageDto.ImageData = await File.ReadAllBytesAsync(imageDto.Image.Path, token);
                }
            }

            return JsonConvert.SerializeObject(imageDto);
        }

        private async Task<string?> GetVideo(ProjectCampaignContentDto content, CancellationToken token)
        {
            var jsonString = GetData(content.Data);

            if (jsonString == null) return null;

            var videoDto = JsonConvert.DeserializeObject<VideoDto>(jsonString);

            if (videoDto != null && videoDto.Video != null)
            {
                if (!string.IsNullOrEmpty(videoDto.Video.Path))
                {
                   videoDto.VideoData = await File.ReadAllBytesAsync(videoDto.Video.Path, token);
                }
            }

            return JsonConvert.SerializeObject(videoDto);
        }


        private string? GetData(object? data)
        {
            if (data == null) return null;

            string jsonString;

            if (data is JsonElement jsonElement)
            {
                jsonString = jsonElement.GetRawText();
            }
            else if (data is string s)
            {
                jsonString = s;
            }
            else
            {
                jsonString = JsonConvert.SerializeObject(data);
            }

            return jsonString;
        }
    }
}
