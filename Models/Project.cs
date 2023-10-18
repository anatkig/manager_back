using Azure;
using Azure.Data.Tables;
using System.Text.Json;

public class Project : ITableEntity
{
    public Project()
    {
        // Tasks = new List<ProjectTask>();
    }

    public string Id { get; set; } = string.Empty;
    // Initialized to non-null value
    public string Name { get; set; } = string.Empty; // Initialized to non-null value
    public string Code { get; set; } = string.Empty; // Initialized to non-null value

    // public List<ProjectTask> Tasks { get; set; } // Initialized in the constructor

    public string PartitionKey { get; set; } = string.Empty; // Initialized to non-null value

    public string RowKey { get { return Id; } set { Id = value; } }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }
}