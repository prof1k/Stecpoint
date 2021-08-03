namespace SIS.Second.Context.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Модель для возвращения сущностей одной страницы.
    /// </summary>
    /// <typeparam name="T"> Тип сущности для пагинации. </typeparam>
    public class Pagination<T> where T : class
    {
        /// <summary>
        /// Модель для возвращения сущностей одной страницы.
        /// </summary>
        public Pagination()
        {
            Entities = new List<T>();
            TotalCount = 0;
        }

        /// <summary>
        /// Сущности.
        /// </summary>
        public ICollection<T> Entities { get; set; }

        /// <summary>
        /// Общее количество.
        /// </summary>
        public int TotalCount { get; set; }
    }
}