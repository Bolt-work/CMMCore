using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Core.Repository;

public abstract class RepositorySettings
{
    //public string ConnectionString { get; set; } = null!;
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";

    public string DatabaseName { get; set; } = "Tickergotchi";

    public string CollectionName { get; set; } = null!;
}
