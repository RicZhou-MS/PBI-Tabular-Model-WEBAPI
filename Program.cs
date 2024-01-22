using Microsoft.AnalysisServices.Tabular;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

//The call to AddEndpointsApiExplorer is required only for minimal APIs
builder.Services.AddEndpointsApiExplorer();
//add swagger
builder.Services.AddSwaggerGen();
var app = builder.Build();

//swagger enabled in Dev environment
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/GetDatasetSchemaJsonString", (PBIConnection PBIconn) =>
{

    try
    {
        using Server server = new Server();
        server.Connect(PBIconn.GetConnectionString());
        //var db = server.Databases.GetByName(PBIconn.dbName);
        Model databaseModel = server.Databases.GetByName(PBIconn.datasetName).Model;
        string modeljsonStr = Microsoft.AnalysisServices.Tabular.JsonSerializer.SerializeObject(databaseModel);
        DatasetModelShema datasetModelShema = new DatasetModelShema();
        datasetModelShema.datasetName = PBIconn.datasetName;
        datasetModelShema.modelSchemaJsonString = modeljsonStr;

        //Console.WriteLine(modeljsonStr);

        //var json = JsonConvert.DeserializeObject(modeljsonStr);
        // JObject json = JObject.Parse(modeljsonStr);
        // JsonResult jsonResult = new JsonResult(json);
        return Results.Ok(datasetModelShema);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

});

app.MapPost("/GetDatasetSchemaJson", (PBIConnection PBIconn) =>
{

    try
    {
        using Server server = new Server();
        server.Connect(PBIconn.GetConnectionString());
        //var db = server.Databases.GetByName(PBIconn.dbName);
        Model databaseModel = server.Databases.GetByName(PBIconn.datasetName).Model;
        string modeljsonStr = Microsoft.AnalysisServices.Tabular.JsonSerializer.SerializeObject(databaseModel);
        DatasetModelShema datasetModelShema = new DatasetModelShema();
        datasetModelShema.datasetName = PBIconn.datasetName;
        datasetModelShema.modelSchemaJsonString = modeljsonStr;

        //Console.WriteLine(modeljsonStr);

        //var json = JsonConvert.DeserializeObject(modeljsonStr);
        // JObject json = JObject.Parse(modeljsonStr);
        // JsonResult jsonResult = new JsonResult(json);
        //return Results.Ok(datasetModelShema);
        return Results.Content(modeljsonStr, MediaTypeHeaderValue.Parse("application/json"));
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

});

app.MapPost("/SetDatasetSchemaJsonString", (DatasetModelImportContext importContext) =>
{
    try
    {
        using Server server = new Server();
        server.Connect(importContext.pbiConnection.GetConnectionString());
        var targetDatabase = server.Databases.GetByName(importContext.pbiConnection.datasetName);
        Model newdatabaseModel = Microsoft.AnalysisServices.Tabular.JsonSerializer.DeserializeObject<Model>(importContext.modelSchemaJsonString);

        newdatabaseModel.CopyTo(targetDatabase.Model);
        string errorMessage = importContext.checkModelColumnAndMeasureStatus(targetDatabase.Model);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            return Results.BadRequest(errorMessage);
        }

        targetDatabase.Model.SaveChanges(); //save changes to the database
        // targetDatabase.Model.RequestRefresh(RefreshType.Full);
        // targetDatabase.Model.SaveChanges();
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }

});


app.Run();