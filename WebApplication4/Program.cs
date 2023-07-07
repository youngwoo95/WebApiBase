using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication4.Database;

namespace WebApplication4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region [1] CORS ��� ���
            // ========================================================= //
            // [CORS][1][1] �⺻: ��� ���
                 builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAnyOrigin",
                        builder => builder
                        .AllowAnyOrigin() // ��å���� ������ ����ϴ��� Ȯ��
                        .AllowAnyMethod() // ��å���� ��� �޴����� ����ϴ��� Ȯ��
                        .AllowAnyHeader()); // ��å���� ��� ����� ����ϴ��� Ȯ��
                });

                // [CORS][1][2] ����: ��� ���
                builder.Services.AddCors(o => o.AddPolicy("AllowAllpolicy", options =>
                {
                    options.AllowAnyOrigin()
                    .AllowAnyMethod() // ��å���� ��� �޴����� ����ϴ��� Ȯ��
                    .AllowAnyHeader(); // ��å���� ��� ����� ����ϴ��� Ȯ��
                }));

                // [CORS][1][3] ����: Ư�� �����θ� ���
                builder.Services.AddCors(o => o.AddPolicy("AllowSpecific", options =>
                {
                    options.WithOrigins("https://localhost:44356")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE") // ��å�� ������ method�� �߰�
                    .WithHeaders("accept", "content-type", "origin", "X-TotalRecordCount"); // ��å�� ������ header�� �߰�
                }));
            // ========================================================= //
            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region �����ͺ��̽� ����
            // ========================================================= //
                // �����ͺ��̽� ���ؽ�Ʈ
                builder.Services.AddEntityFrameworkSqlServer().AddDbContext<NoticeAppDbContext>(options => options.UseSqlServer(@"Server=123.2.156.21,1433;Database=NoticeApp;User Id=sa1;Password=wegg2650;"));

                // ��������(NoticeApp) ���� ������(���Ӽ�) ���� ���� �ڵ常 ���� ��Ƽ� ����
                // INoticeRepositoryAsync.cs Inject: DI Container�� ����(�������丮) ���
                builder.Services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();
            // ========================================================= //
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region [CORS][2] ��� ���

            // [CORS][2] ��� ���
            // ========================================================= //
                app.UseCors("AllowAnyOrigin");
            // ========================================================= // 

            #endregion
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}