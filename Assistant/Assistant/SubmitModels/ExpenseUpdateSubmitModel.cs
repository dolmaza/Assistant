namespace Assistant.SubmitModels
{
    public class ExpenseUpdateSubmitModel
    {
        public int? ExpenseId { get; set; }
        public string ExpenseCaption { get; set; }
        public decimal? Amount { get; set; }
    }
}