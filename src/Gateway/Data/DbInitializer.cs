namespace Gateway.Data;

public class DbInitializer
{
    public static void Initialize(HowToLearnDbContext context)
    {
        if(context.Database.EnsureCreated())
            DbFeeder.Feed(context);
    }
}
