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

            #region [1] CORS 사용 등록
            // ========================================================= //
            // [CORS][1][1] 기본: 모두 허용
                 builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAnyOrigin",
                        builder => builder
                        .AllowAnyOrigin() // 정책에서 원본을 허용하는지 확인
                        .AllowAnyMethod() // 정책에서 모든 메더스를 허용하는지 확인
                        .AllowAnyHeader()); // 정책에서 모든 헤더를 허용하는지 확인
                });

                // [CORS][1][2] 참고: 모두 허용
                builder.Services.AddCors(o => o.AddPolicy("AllowAllpolicy", options =>
                {
                    options.AllowAnyOrigin()
                    .AllowAnyMethod() // 정책에서 모든 메더스를 허용하는지 확인
                    .AllowAnyHeader(); // 정책에서 모든 헤더를 허용하는지 확인
                }));

                // [CORS][1][3] 참고: 특정 도메인만 허용
                builder.Services.AddCors(o => o.AddPolicy("AllowSpecific", options =>
                {
                    options.WithOrigins("https://localhost:44356")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE") // 정책에 지정된 method를 추가
                    .WithHeaders("accept", "content-type", "origin", "X-TotalRecordCount"); // 정책에 지정된 header를 추가
                }));
            // ========================================================= //
            #endregion

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region 데이터베이스 정의
            // ========================================================= //
                // 데이터베이스 컨텍스트
                builder.Services.AddEntityFrameworkSqlServer().AddDbContext<NoticeAppDbContext>(options => options.UseSqlServer(@"Server=123.2.156.21,1433;Database=NoticeApp;User Id=sa1;Password=wegg2650;"));

                // 공지사항(NoticeApp) 관련 의존성(종속성) 주입 관련 코드만 따로 모아서 관리
                // INoticeRepositoryAsync.cs Inject: DI Container에 서비스(리포지토리) 등록
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

            #region [CORS][2] 사용 등록

            // [CORS][2] 사용 등록
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