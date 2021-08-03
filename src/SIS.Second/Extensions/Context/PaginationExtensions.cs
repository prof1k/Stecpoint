namespace SIS.Second.Extensions.Context
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using SIS.Second.Context.Models;
    using SIS.Second.Infrastructure.Exceptions;
    
    /// <summary>
    /// Расширения для пагинации.
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// Применение к IQueryable пагинации.
        /// </summary>
        /// <typeparam name="T">Модель БД.</typeparam>
        /// <typeparam name="TDto">Модель Дто.</typeparam>
        /// <param name="queryable">Объект запроса.</param>
        /// <param name="size">Размер.</param>
        /// <param name="offset">Количество пропускаемых объектов.</param>
        /// <param name="configurationProvider">Провайдер конфигураций автомаппера для проекции.</param>
        /// <returns>Объект <see cref="Pagination{T}"/>.</returns>
        public static async Task<Pagination<TDto>> ToPaginationAsync<T, TDto>(this IQueryable<T> queryable, int size,
            int offset, IConfigurationProvider configurationProvider)
            where T : class
            where TDto : class
        {
            if (offset < 0)
                throw new BusinessException("Количество пропускаемых объектов должно быть больше или равно нулю.");

            if (size <= 0)
                throw new BusinessException("Количество запрашиваемых объектов должно быть больше нуля.");

            var count = await queryable.CountAsync();

            var data = await queryable.Skip(offset).Take(size).ProjectTo<TDto>(configurationProvider).ToListAsync();

            return new Pagination<TDto>
            {
                Entities = data,
                TotalCount = count
            };
        }
    }

}