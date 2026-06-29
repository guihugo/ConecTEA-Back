using Conectea.API;
using Conectea.Application;
using Conectea.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registrar serviços
builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Quando implementarmos JWT, descomente essas linhas
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();