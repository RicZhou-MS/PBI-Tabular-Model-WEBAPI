using Microsoft.AnalysisServices.Tabular;
public class PBIConnection
{
    public required string workspaceConnection { get; set; } // "powerbi://api.powerbi.cn/v1.0/myorg/<workspace>";
    public required string tenantId { get; set; } //"xxxxx-c47b-43ce-a3f5-xxxxxx";
    public required string appId { get; set; } //"xxxxx-0bc7-4165-b706-xxxxxx";
    public required string appSecret { get; set; }
    public required string datasetName { get; set; }

    public string GetConnectionString()
    {
        return $"DataSource={workspaceConnection};User ID=app:{appId}@{tenantId};Password={appSecret};";
    }
}

public class DatasetModelShema
{
    public string? datasetName { get; set; }
    public string? modelSchemaJsonString { get; set; }
}

public class DatasetModelImportContext
{
    public required PBIConnection pbiConnection { get; set; }
    public required string modelSchemaJsonString { get; set; }

    public string checkModelColumnAndMeasureStatus(Model databaseModel)
    {
        string checkResult = string.Empty;
        foreach (Table tbl in databaseModel.Tables)
        {

            foreach (Column column in tbl.Columns)
            {
                if (!string.IsNullOrEmpty(column.ErrorMessage))
                {
                    checkResult += "Table:[" + tbl.Name + "] | Coulumn: [" + column.Name + "] | ErrorMessage: [" + column.ErrorMessage + "]\n";
                }
            }
            foreach (var measure in tbl.Measures)
            {
                if (!string.IsNullOrEmpty(measure.ErrorMessage))
                {
                    checkResult += "Table:[" + tbl.Name + "] | Measure: [" + measure.Name + "] | ErrorMessage [" + measure.ErrorMessage + "]\n";
                }
            }

        }
        return checkResult;
    }
}