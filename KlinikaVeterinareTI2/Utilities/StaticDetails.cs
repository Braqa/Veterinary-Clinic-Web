namespace KlinikaVeterinareTI2.Utilities
{
    public static class StaticDetails
    {
        // Roles
        public const string Admin = "Admin";
        public const string Veterinar = "Veterinar";
        public const string User = "User";


        // Gender
        //public enum Gender
        //{
        //    Mashkull,
        //    Femër,
        //    Tjetër
        //}
        public const string Male = "Mashkull";
        public const string Female = "Femer";
        public const string Other = "Tjeter";

        // Messages
        public const string ErrorMessage = "Ka ndodhur një gabim në Sistem, Ju lutem provoni më vonë";
        public const string NotFoundMessage = "Të dhënat nuk u gjetën në databazë";
        public const string DataAdded = "Të dhënat u shtuan me sukses";
        public const string DataModified = "Të dhënat u ndryshuan me sukses";
        public const string DataDeleted = "Të dhënat u fshinë me sukses";
    }
}
