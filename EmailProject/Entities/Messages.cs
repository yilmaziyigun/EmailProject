namespace EmailProject.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; } 
        public string MessageDetail { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsStatus { get; set; }
        public bool IsStarred { get; set; } = false;


    }
}
