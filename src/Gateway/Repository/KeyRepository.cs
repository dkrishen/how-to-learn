using Gateway.Data;
using Gateway.Models.Entities;

namespace Gateway.Repository;

public class KeyRepository : RepositoryCrud<Key>, IKeyRepository
{
    public KeyRepository(HowToLearnDbContext context) : base(context) { }
}