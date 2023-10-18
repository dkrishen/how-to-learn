namespace Gateway.Data;

public class DbInitializer
{
    public static void Initialize(HowToLearnDbContext context)
    {
        context.Database.EnsureCreated();
    }
}
