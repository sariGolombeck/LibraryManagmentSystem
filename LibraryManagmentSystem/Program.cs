
//using LibraryManagmentSystem.Core.Repositories;

using LibraryManagmentSystem.Mapping;
using LibraryManagmentSystem.Core.Mapping;
using LibraryManagmentSystem.Core.Repositories;
using LibraryManagmentSystem.Core.Services;
using LibraryManagmentSystem.Data;
using LibraryManagementSystem.Core.Services;
using LibraryManagementSystem.Data.Repositories;
using LibraryManagementSystem.Services;
using LibraryManagmentSystem.Core.Interfaces.Repositories;
using LibraryManagmentSystem.Core.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookBorrowerRepository, BookBorrowerRepository>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookBorrowerService, BookBorrowerService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(MappingPostModelsProfile));

builder.Services.AddDbContext<DataContext>();

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
