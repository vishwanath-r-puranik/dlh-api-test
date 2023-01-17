using DLHAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var policyName = "movesPolicy";

// Add services to the container.

// ::  SQL  server database - connection string from appsetting.json file
builder.Services.AddDbContext<DLHDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDbContext<DlhdevAuditContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AuditDbConnection")));

// :: Postgress SQL database - connection string from appsetting.json file
//builder.Services.AddDbContext<DLHDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy(policyName, corsbuilder =>
{
    corsbuilder.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// :: Add content negotiation
 builder.Services.AddMvc().AddXmlSerializerFormatters();

var app = builder.Build();

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<DLHDbContext>();
//    dbContext.Database.EnsureCreated();
//}



app.UseCors(policyName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
