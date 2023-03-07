namespace FoodCorner.Contracts.Email
{
    public class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Hesabin aktivlesdirilmesi";
            public const string NOTIFICATION_MESSAGE = $"Orderinizin Statusu Yenilendi Xais Edirik Yoxlayin";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Sizin activation urliniz : {EmailMessageKeyword.ACTIVATION_URL}";
        }
    }
}
