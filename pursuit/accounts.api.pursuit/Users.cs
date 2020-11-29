using System;

namespace accounts.api.pursuit
{
    public class Users
    {
        public static Guid Alice { get; } = Guid.Parse("33164480B55C461691A6EA458C2AC614");
        public static Guid Bob { get; } = Guid.Parse("483B25A568074E8F8CBA23AE18F4086E");
        public static Guid Chris { get; } = Guid.Parse("0860256F8C5A466F879D1644919B2DE8");

        public static string GetUserName(Guid userId)
        {
            foreach (var prop in typeof(Users).GetProperties())
            {
                if ((Guid)prop.GetValue(null, null) == userId)
                    return prop.Name;
            }

            return "";
        }
    }
}
