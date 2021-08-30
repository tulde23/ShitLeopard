namespace ShitLeopard.Common.Documents
{
    public class SeasonDocument : ElasticDocument
    {
        public SeasonDocument()
        {
        }

        public SeasonDocument(ShowDocument showDocument)
        {
            ShowTitle = showDocument.Title;
            ShowDescription = showDocument.Description;
            ShowLastEpisodeId = showDocument.LastEpisodeId;
            ShowId = showDocument.DocumentId;
        }

        public int SeasonNumber { get; set; }

        public string Title { get; set; }
        public string ShowId { get; set; }
        public string ShowTitle { get; set; }
        public string ShowDescription { get; set; }
        public int ShowLastEpisodeId { get; set; }
    }
}