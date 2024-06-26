using MagniseTask.Data;
using MagniseTask.Services;

var builder = WebApplication.CreateBuilder (args);

builder.Services.AddControllers ();

builder.Services.AddDbContext<InstrumentDbContext> ();
builder.Services.AddHttpClient ();
builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository> ();
builder.Services.AddScoped<IHistoricalDataRequestService, HistoricalDataRequestService> ();
builder.Services.AddScoped<IRealTimeService, RealTimeService> ();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService> ();

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

var app = builder.Build ();

app.UseSwagger ();
app.UseSwaggerUI ();

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

app.Run ();