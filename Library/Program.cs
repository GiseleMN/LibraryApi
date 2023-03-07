using Library.Contexts;
using Library.Interfaces;
using Library.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<LibraryContext, LibraryContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();

builder.Services.AddControllers();

// Add services cors policy
builder.Services.AddCors(options =>
{

    options.AddPolicy("CorsPolicy", builder =>

    {

        builder.WithOrigins("http://localhost:3000")

        .AllowAnyHeader()

        .AllowAnyMethod();
    });
});


// Add services Jwt Bearer : Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

//Parameters for the token validation
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Validation for requesting
        ValidateIssuer = true,

        //Validation receiving
        ValidateAudience = true,

        //Expiration time to the validation
        ValidateLifetime = true,

        //Encryption form/ validation authentication key
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("library-chave-autenticacao")),

        //Expiration time to the token
        ClockSkew = TimeSpan.FromMinutes(60),

        //Issuer name/ Where is coming from 
        ValidIssuer = "Library",

        //Audience name/  Where is going
        ValidAudience = "Library"
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.MapControllers();

app.Run();

