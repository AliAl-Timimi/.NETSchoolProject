namespace Languages.UI.MVC.Models.dto
{
    public class IdeLanguageDto
    {
        public long IdeId { get; set; }
        public long LangId { get; set; }
        public int? PopOrder { get; set; }

        public IdeLanguageDto(long ideId, long langId, int? popOrder)
        {
            IdeId = ideId;
            LangId = langId;
            PopOrder = popOrder;
        }
    }
}