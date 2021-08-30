

namespace ShitLeopard.Common.Documents
{
    public class EpisodeDocument : ElasticDocument
    {
        public EpisodeDocument()
        {
        }
        public EpisodeDocument(SeasonDocument seasonDocument, ShowDocument showDocument)
        {
            ShowTitle = showDocument.Title;
            ShowDescription = showDocument.Description;
            ShowLastEpisodeId = showDocument.LastEpisodeId;
            ShowId = showDocument.DocumentId;
            SeasonId = seasonDocument.DocumentId;
            SeasonNumber = seasonDocument.SeasonNumber;
            SeasonTitle = seasonDocument.Title;
        }
        public long EpisodeNumber { get; set; }
        public int? OffsetId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ShowId { get; set; }
        public string ShowTitle { get; set; }
        public string ShowDescription { get; set; }
        public int ShowLastEpisodeId { get; set; }
        public int SeasonNumber { get; set; }
        public string SeasonId { get; set; }
        public string SeasonTitle { get; set; }
        public string Synopsis { get; set; }

    }
}