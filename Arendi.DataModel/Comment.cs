namespace Arendi.DataModel
{
    public partial class Comment
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public int IdeaID { get; set; }
        public int UserID { get; set; }
    }
}
