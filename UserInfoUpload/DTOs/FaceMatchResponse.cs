namespace UserInfoUpload.DTOs
{
    public class FaceMatchResponse
    {
        public decimal Similarity { get; set; }
        public int StatusCode { get; set; }
        public string TransactionId { get; set; }
        public int ProcessingTimeInMilliSeconds { get; set; }
    }
}
