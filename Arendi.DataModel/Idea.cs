namespace Arendi.DataModel
{
    public partial class Idea
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public byte[] Image { get; set; }
    }
}
