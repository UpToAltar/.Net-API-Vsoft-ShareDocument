using Vsoft_share_document.BUS;
using Vsoft_share_document.DAO;
using Vsoft_share_document.DatabaseHelper;
using Vsoft_share_document.InterfaceBUS;
using Vsoft_share_document.InterfaceDAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => c.EnableAnnotations()
    );


//Add DI
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IDocumentWatchers_BUS, DocumentWatchers_BUS>();
builder.Services.AddScoped<IDocumentWatchers_DAO, DocumentWatchers_DAO>();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
